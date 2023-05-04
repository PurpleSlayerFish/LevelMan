using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Core.Services.Pools.Config;
using UnityEngine;

namespace PurpleSlayerFish.Core.Services.Pools.PoolProvider
{
    public class FloatingCanvasPoolProvider :  AbstractPoolProvider<FloatingCanvasPoolData>
    {
        protected override string PoolerConfigName => "FloatingCanvasPoolConfig";
        protected override string RootName => "[FloatingCanvasPoolProvider]";

        protected override AbstractBehaviour CreatePooledItem(Transform parent, FloatingCanvasPoolData data) => 
            _assetProvider.Instantiate<AbstractBehaviour>(data.BundleName, data.PrefabName, parent);
    }
}