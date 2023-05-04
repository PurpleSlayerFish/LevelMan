using Cinemachine;
using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Global;
using PurpleSlayerFish.Core.Services.AssetProvider;
using PurpleSlayerFish.Core.Services.DataStorage;
using PurpleSlayerFish.Core.Services.EffectsManager;
using PurpleSlayerFish.Core.Services.Input;
using PurpleSlayerFish.Core.Services.PauseService;
using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using PurpleSlayerFish.Core.Services.ScenePoints;
using PurpleSlayerFish.Core.Services.ScriptableObjects.GameConfig;
using PurpleSlayerFish.Core.Services.Tooltips;
using PurpleSlayerFish.Core.Ui.AudioManager;
using PurpleSlayerFish.Core.Ui.Container;
using PurpleSlayerFish.Core.Ui.Windows.GameWindow;
using PurpleSlayerFish.Core.Ui.Windows.PauseWindow;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ScenePoints _scenePoints;
        
        [Inject] private IAssetProvider _assetProvider;
        [Inject] private IUiContainer _uiContainer;
        [Inject] private SignalBus _signalBus;

        private EffectsPoolProvider _effectsPoolProvider;
        private BehaviourPoolProvider _behaviourPoolProvider;
        private AudioPoolProvider _audioPoolProvider;
        private FloatingCanvasPoolProvider _floatingCanvasPoolProvider;

        public override void InstallBindings()
        {
            BindSignals();
            BindInterfaces();
            BindInstances();
            BindPools();
            BindUi();
            BindInput();
        }

        private void BindSignals()
        {
            _signalBus.DeclareSignal<ScoreSubscription>();
        }
        
        private void BindInterfaces()
        {
            Container.BindInterfacesTo<PauseService>().AsSingle();
            Container.BindInterfacesTo<PlayerPrefsStorage<PlayerData>>().AsSingle();
        }

        private void BindInstances()
        {
            Container.BindInstance(_scenePoints);
            Container.BindInstance(_assetProvider.GetScriptableObject<GameConfig>()).AsSingle();
            Container.BindInstance(_assetProvider.Instantiate<Camera>(GameGlobal.CORE_BUNDLE, GameGlobal.CAMERA_PREFAB)).AsSingle();
            Container.BindInstance(_assetProvider.Instantiate<CinemachineVirtualCamera>(GameGlobal.CORE_BUNDLE, GameGlobal.VIRTUAL_CAMERA_PREFAB)).AsSingle();
        }

        private void BindPools()
        {
            BindPoolProvider<EffectsPoolProvider, EffectsPoolData>(out _effectsPoolProvider);
            BindPoolProvider<BehaviourPoolProvider, BehaviourPoolData>(out _behaviourPoolProvider);
            BindPoolProvider<AudioPoolProvider, AudioPoolData>(out _audioPoolProvider);
            BindPoolProvider<FloatingCanvasPoolProvider, FloatingCanvasPoolData>(out _floatingCanvasPoolProvider);
            Container.BindInstance(Container.Instantiate<EffectsManager>()).AsSingle();
            Container.BindInstance(Container.Instantiate<AudioManager>()).AsSingle();
            Container.BindInstance(Container.Instantiate<TooltipManager>()).AsSingle();

            void BindPoolProvider<T1, T2>(out T1 poolProvider) where T1 : IPoolProvider<T2> where T2 : PoolData
            {
                poolProvider = Container.Instantiate<T1>();
                poolProvider.Initialized();
                Container.Bind<IPoolProvider<T2>>().FromInstance(poolProvider).AsSingle();
            }
        }

        private void BindUi()
        {
            _uiContainer.InitializeWindow<GameWindow>();
            _uiContainer.InitializeWindow<PauseWindow>();
            _uiContainer.Show<GameController>();
        }

        private void BindInput()
        {
            Container.Bind<IInputProvider<InputData>>().To<InputProvider>().AsSingle();
#if UNITY_ANDROID
            _uiContainer.InitializeWindow<TouchScreenWindow>();
            var controller = _uiContainer.Get<TouchScreenController>();
            Container.Bind<ITickable>().FromInstance(controller);
            controller.Show();
#else
            Container.BindInstance(_assetProvider.Instantiate<PlayerInputAdapter>(GameGlobal.CORE_BUNDLE, GameGlobal.DESKTOP_INPUT_PREFAB)).AsSingle();
#endif
        }

        private void OnDestroy()
        {
            _effectsPoolProvider.Dispose();
            _behaviourPoolProvider.Dispose();
        }
    }
}
