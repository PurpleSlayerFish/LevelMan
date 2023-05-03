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
        private AudioBehaviour _tempBehavior;

        protected override AbstractBehaviour CreatePooledItem(Transform parent, AudioPoolData data)
        {
            _tempBehavior = _assetProvider.Instantiate<AudioBehaviour>(data.BundleName, data.PrefabName, parent);
            _tempBehavior.Temporator = new Temporator(_tempBehavior.ClipDuation, () => Release(data.PrefabName, _tempBehavior));
            return _tempBehavior;
        }
        
        protected override void OnTakeFromPool(AbstractBehaviour view)
        {
            base.OnTakeFromPool(view);
            (view as AudioBehaviour)?.Temporator.Execute();
        }
    }
}