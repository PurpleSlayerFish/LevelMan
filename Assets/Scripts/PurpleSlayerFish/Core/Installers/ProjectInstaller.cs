using PurpleSlayerFish.Core.Global;
using PurpleSlayerFish.Core.Services;
using PurpleSlayerFish.Core.Services.AssetProvider;
using PurpleSlayerFish.Core.Services.SceneLoader;
using PurpleSlayerFish.Core.Ui.Container;
using PurpleSlayerFish.Core.Ui.Windows.LoadingWindow;
using Zenject;

namespace PurpleSlayerFish.Core.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        private AssetBundleProvider _assetProvider;
        private UiProvider _uiProvider;
        
        public override void InstallBindings()
        {
            _assetProvider = Container.Instantiate<AssetBundleProvider>();
            _assetProvider.Install(Container);
            _uiProvider = _assetProvider.Instantiate<UiProvider>(UiGlobal.CORE_BUNDLE, UiGlobal.ROOT_PREFAB);
            DontDestroyOnLoad(_uiProvider);
            Container.BindInstance(_uiProvider).AsSingle();
            Container.BindInstance(_assetProvider
                .Instantiate<LoadingWindow>(GameGlobal.PROJECT_BUNDLE, _uiProvider.transform)
                .Initialize<LoadingController>(Container));
            Container.BindInterfacesTo<AsyncSceneLoader>().AsSingle();

            Container.BindInstance(Container.Instantiate<MathUtils>()).AsSingle();
            Container.BindInstance(Container.Instantiate<StringUtils>()).AsSingle();
        }

        private void OnDestroy()
        {
            Destroy(_uiProvider.gameObject);
            _assetProvider.Dispose();
        }
    }
}