using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Core.Services.Pools.Config;
using UnityEngine;

namespace PurpleSlayerFish.Core.Services.Pools.PoolProvider
{
    public class BehaviourPoolProvider : AbstractPoolProvider<BehaviourPoolData>
    {
        protected override string PoolerConfigName => "BehaviourPoolConfig";
        protected override string RootName => "[BehaviourPoolProvider]";

        protected override AbstractBehaviour CreatePooledItem(Transform parent, BehaviourPoolData data) 
            => _assetProvider.Instantiate<AbstractBehaviour>(data.BundleName, data.PrefabName, parent);
    }
}