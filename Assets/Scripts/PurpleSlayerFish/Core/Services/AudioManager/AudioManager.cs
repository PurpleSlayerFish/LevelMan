using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Services.DataStorage;
using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using Zenject;

namespace PurpleSlayerFish.Core.Ui.AudioManager
{
    public class AudioManager
    {
        [Inject] private IDataStorage<SettingsData> _settingsStorage;
        [Inject] private IPoolProvider<AudioPoolData> _audioPoolProvider;
        
        private bool _isSoundEnabled => _settingsStorage.Load().IsSoundEnabled;

        public void PlayCloseSound()
        {
            if (!_isSoundEnabled)
                return;
            _audioPoolProvider.Get("CloseAudio");
        }
        
        public void PlayTransitionAudio()
        {
            if (!_isSoundEnabled)
                return;
            _audioPoolProvider.Get("TransitionAudio");
        }
    }
}