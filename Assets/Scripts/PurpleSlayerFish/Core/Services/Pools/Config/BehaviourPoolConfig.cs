using System;
using UnityEngine;

namespace PurpleSlayerFish.Core.Services.Pools.Config
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BehaviourPoolConfig", fileName = "BehaviourPoolConfig")]
    public class BehaviourPoolConfig : AbstractPoolConfig<BehaviourPoolData>
    {
        [SerializeField] private BehaviourPoolData[] _poolData;
        public override BehaviourPoolData[] GetData => _poolData;
    }
    
    [Serializable]
    public class BehaviourPoolData : PoolData
    {
    }
}