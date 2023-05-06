using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Core.Services.Pools.Config;
using UnityEngine;
using AudioBehaviour = PurpleSlayerFish.Core.Behaviours.AudioBehaviour;

namespace PurpleSlayerFish.Core.Services.Pools.PoolProvider
{
    public class AudioPoolProvider : AbstractPoolProvider<AudioPoolData>
    {
        protected override string PoolerConfigName => "AudioPoolConfig";
        protected override string RootName => "[AudioPoolProvider]";

        protected override AbstractBehaviour CreatePooledItem(Transform parent, AudioPoolData data)
        {
            var tempBehavior = _assetProvider.Instantiate<AudioBehaviour>(data.BundleName, data.PrefabName, parent);
            tempBehavior.Temporator = new Temporator(tempBehavior.ClipDuration, () => Release(data.PrefabName, tempBehavior));
            return tempBehavior;
        }
        
        protected override void OnTakeFromPool(AbstractBehaviour view)
        {
            base.OnTakeFromPool(view);
            (view as AudioBehaviour)?.Temporator.Execute();
        }
    }
}