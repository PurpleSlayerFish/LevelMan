using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Services.AssetProvider;
using PurpleSlayerFish.Core.Services.DataStorage;
using PurpleSlayerFish.Core.Ui.Container;
using PurpleSlayerFish.Core.Ui.ElementManager;
using Zenject;

namespace PurpleSlayerFish.Core.Installers
{
    public class GeneralBindInstaller : MonoInstaller
    {
        private AssetBundleProvider _assetProvider;
        private UiContainer _uiContainer;
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            _assetProvider = Container.Instantiate<AssetBundleProvider>();
            _assetProvider.Install(Container);
            Container.Bind<IAssetProvider>().FromInstance(_assetProvider).AsSingle();
            Container.BindInterfacesTo<UiElementManager>().AsSingle();
            _uiContainer = Container.Instantiate<UiContainer>();
            _uiContainer.Install(Container);
            Container.Bind<IUiContainer>().FromInstance(_uiContainer).AsSingle();
            Container.Bind<IDataStorage<SettingsData>>().To<PlayerPrefsStorage<SettingsData>>().AsSingle();
        }
        
        private void OnDestroy()
        {
            _uiContainer.Dispose();
            _assetProvider.Dispose();
        }
    }
}