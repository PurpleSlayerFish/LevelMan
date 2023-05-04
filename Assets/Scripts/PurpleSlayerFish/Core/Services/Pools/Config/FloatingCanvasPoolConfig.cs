using System;
using UnityEngine;

namespace PurpleSlayerFish.Core.Services.Pools.Config
{
    [CreateAssetMenu(menuName = "ScriptableObjects/FloatingCanvasPoolConfig", fileName = "FloatingCanvasPoolConfig")]
    public class FloatingCanvasPoolConfig : AbstractPoolConfig<FloatingCanvasPoolData>
    {
        [SerializeField] private FloatingCanvasPoolData[] _poolData;
        public override FloatingCanvasPoolData[] GetData => _poolData;
    }

    [Serializable]
    public class FloatingCanvasPoolData : PoolData
    {
    }
}