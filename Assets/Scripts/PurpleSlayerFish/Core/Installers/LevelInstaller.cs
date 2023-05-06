using System.Threading.Tasks;
using DG.Tweening;
using PurpleSlayerFish.Core.Services.AssetProvider;
using PurpleSlayerFish.Core.Services.AudioManager;
using PurpleSlayerFish.Core.Services.Pause;
using PurpleSlayerFish.Core.Services.ScriptableObjects;
using PurpleSlayerFish.Game.Controllers.Impls;
using PurpleSlayerFish.Game.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Core.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private IAssetProvider _assetProvider;
        [Inject] private AudioManager _audioManager;
        [Inject] private IPauseService _pauseService;

        private StalagnateController _stalagnateController;
        private ObstacleController _obstacleController;
        public override void InstallBindings()
        {
            Container.BindInstance(_assetProvider.GetScriptableObject<RatSpawnConfig>()).AsSingle();
            Container.BindInstance(Container.Instantiate<InteractionController>()).AsSingle();
            Container.BindInterfacesAndSelfTo<RatController>().AsSingle();
            _obstacleController = Container.Instantiate<ObstacleController>();
            Container.BindInstance(_obstacleController).AsSingle();
            _obstacleController.Initialize();
            _stalagnateController = Container.Instantiate<StalagnateController>();
            Container.BindInstance(_stalagnateController).AsSingle();
            
            AfterStartPause();
            _audioManager.Play("MusicPrefab");
            PlayerRandomSpeech(new[]
            {
                "Speech 1", 
                "Speech 2", 
                "Speech 3", 
                "Speech 4", 
                "Speech 5", 
                "Speech 6", 
                "Speech 7", 
                "Speech 8", 
                "Speech 9", 
                "Speech 10", 
                "Speech 11", 
                "Speech 12", 
            });
            
            PlayerRandomCaveSound(new[]
            {
                "Cave 1",
                "Cave 2",
                "Cave 3",
                "Cave 4",
                "Cave 5",
                "Cave 6",
                "Cave 7",
                "Cave 8",
                "Cave 9",
            });
        }


        private float _speechTimer;
        private float _caveTimer;
        private async void PlayerRandomSpeech(string[] keys1)
        {
            while (gameObject)
            {
                if (!_pauseService.IsPaused)
                {
                    _speechTimer += Time.deltaTime;
                    if (_speechTimer >= _gameConfig.RandomPhrasesTimeout)
                    {
                        _speechTimer = 0;
                        _audioManager.PlayRandom(keys1);
                    }
                }
                await Task.Yield();
            }
        }
        
        private async void PlayerRandomCaveSound(string[] keys2)
        {
            while (gameObject)
            {
                if (!_pauseService.IsPaused)
                {
                    _caveTimer += Time.deltaTime;
                    if (_caveTimer >= _gameConfig.RandomCaveSoundTimeout)
                    {
                        _caveTimer = 0;
                        _audioManager.PlayRandom(keys2);
                    }
                }
                await Task.Yield();
            }
        }

        private async void AfterStartPause()
        {
            while (_pauseService.IsPaused)
                await Task.Yield();
            _audioManager.Play("FirstSpeech");
        }
    }
}