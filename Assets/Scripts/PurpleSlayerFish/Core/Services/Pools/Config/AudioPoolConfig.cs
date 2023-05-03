using System;
using UnityEngine;

namespace PurpleSlayerFish.Core.Services.Pools.Config
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AudioPoolConfig", fileName = "AudioPoolConfig")]
    public class AudioPoolConfig : AbstractPoolConfig<AudioPoolData>
    {
        [SerializeField] private AudioPoolData[] _poolData;
        public override AudioPoolData[] GetData => _poolData;
    }
    
    [Serializable]
    public class AudioPoolData : PoolData
    {
    }
}