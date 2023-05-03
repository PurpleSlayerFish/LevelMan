using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Core.Services.Pools.Config;
using UnityEngine;

namespace PurpleSlayerFish.Core.Services.Pools.PoolProvider
{
    public class EffectsPoolProvider :  AbstractPoolProvider<EffectsPoolData>
    {
        protected override string PoolerConfigName => "EffectsPoolConfig";
        protected override string RootName => "[EffectsPoolProvider]";

        protected override AbstractBehaviour CreatePooledItem(Transform parent, EffectsPoolData data)
        {
            var tempBehavior = _assetProvider.Instantiate<EffectBehaviour>(data.BundleName, data.PrefabName, parent);
            tempBehavior.Temporator = new Temporator(data.LifeTimeMilliseconds, () => Release(data.PrefabName, tempBehavior));
            return tempBehavior;
        }
        
        protected override void OnTakeFromPool(AbstractBehaviour view)
        {
            base.OnTakeFromPool(view);
            (view as EffectBehaviour)?.Temporator.Execute();
        }
    }
}