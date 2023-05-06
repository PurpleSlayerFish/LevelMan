using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Services.DataStorage;
using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Core.Services.AudioManager
{
    public class AudioManager
    {
        [Inject] private IDataStorage<SettingsData> _settingsStorage;
        [Inject] private IPoolProvider<AudioPoolData> _audioPoolProvider;
        
        private bool _isSoundEnabled => _settingsStorage.Load().IsSoundEnabled;

        public void PlayRandom(string[] keys)
        {
            if (!_isSoundEnabled)
                return;
            
            _audioPoolProvider.Get(keys[Random.Range(0, keys.Length)]);
        }
        
        public void Play(string key)
        {
            if (!_isSoundEnabled)
                return;
            _audioPoolProvider.Get(key);
        }
    }
}