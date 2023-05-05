using Cinemachine;
using PurpleSlayerFish.Core.Global;
using PurpleSlayerFish.Core.Services.AssetProvider;
using PurpleSlayerFish.Core.Services.EffectsManager;
using PurpleSlayerFish.Core.Services.Input;
using PurpleSlayerFish.Core.Services.Pause;
using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using PurpleSlayerFish.Core.Services.ScenePoints;
using PurpleSlayerFish.Core.Ui.Windows.GameWindow;
using PurpleSlayerFish.Game.Behaviours;
using PurpleSlayerFish.Game.Controllers;
using PurpleSlayerFish.Game.Controllers.Impls;
using Zenject;

namespace PurpleSlayerFish.Core.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [Inject] private IAssetProvider _assetProvider;
        [Inject] private IInputProvider<InputData> _inputProvider;
        [Inject] private IPauseService _pauseService;
        [Inject] private CinemachineVirtualCamera _virtualCamera;
        [Inject] private SignalBus _signalBus;
        [Inject] private ScenePoints _scenePoints;
        
        [Inject] private RatController _ratController;
        [Inject] private ObstacleController _obstacleController;

        private const string CAMERA_START = "CAMERA_OFFSET";
        private const string PLAYER_START = "PLAYER_START";

        public override void InstallBindings()
        {
            _inputProvider.Data.OnEscape += () =>
            {
                // todo проверку и закрытие других окон
                _pauseService.SetPause(!_pauseService.IsPaused);
                _pauseService.UpdatePauseWindow();
            };
            
            var player = _assetProvider.Instantiate<PlayerBehaviour>(GameGlobal.PREFABS_BUNDLE);
            
            // Install Virtual Camera
            _virtualCamera.transform.position = _scenePoints.Points.Get(CAMERA_START).position;
            _virtualCamera.transform.rotation = _scenePoints.Points.Get(CAMERA_START).rotation;
            _virtualCamera.Follow = player.transform;
            player.MovementProcessor.Warp(_scenePoints.Points.Get(PLAYER_START).position);
            player.Initialize();
            
            _signalBus.Fire(new ScoreSubscription{Score = 100});
            _signalBus.Fire(new HealthSubscription{HealthPercent = player.CombatProcessor.HealthPercent});
            _ratController.Initialize(player);
        }
    }
}