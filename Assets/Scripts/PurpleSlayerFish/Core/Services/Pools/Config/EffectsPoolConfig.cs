using System;
using UnityEngine;

namespace PurpleSlayerFish.Core.Services.Pools.Config
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EffectsPoolConfig", fileName = "EffectsPoolConfig")]
    public class EffectsPoolConfig : AbstractPoolConfig<EffectsPoolData>
    {
        [SerializeField] private EffectsPoolData[] _poolData;
        public override EffectsPoolData[] GetData => _poolData;
    }

    [Serializable]
    public class EffectsPoolData : PoolData
    {
        [SerializeField] private int _lifeTimeMilliseconds;
        public int LifeTimeMilliseconds => _lifeTimeMilliseconds;
    }
}