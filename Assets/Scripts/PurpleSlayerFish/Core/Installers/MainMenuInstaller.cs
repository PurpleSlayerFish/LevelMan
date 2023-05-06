using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Services.DataStorage;
using PurpleSlayerFish.Core.Ui.Container;
using PurpleSlayerFish.Core.Ui.Windows.MainMenuWindow;
using PurpleSlayerFish.Core.Ui.Windows.SettingsWindow;
using Zenject;

namespace PurpleSlayerFish.Core.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [Inject] private IUiContainer _uiContainer;
        [Inject] private IDataStorage<PlayerData> _dataStorage;
        
        public override void InstallBindings()
        {
            _dataStorage.LoadCurrent();
            BindInterfaces();
            BindUi();
        }
        
        private void BindInterfaces()
        {
        }
        
        private void BindUi()
        {
            _uiContainer.InitializeWindow<MainMenuWindow>();
            _uiContainer.InitializeWindow<SettingsWindow>();
            _uiContainer.Show<MainMenuController>();
        }
    }
}