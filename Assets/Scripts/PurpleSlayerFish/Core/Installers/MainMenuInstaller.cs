using PurpleSlayerFish.Core.Ui.Container;
using PurpleSlayerFish.Core.Ui.Windows.MainMenuWindow;
using PurpleSlayerFish.Core.Ui.Windows.ShopWindow;
using Zenject;

namespace PurpleSlayerFish.Core.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [Inject] private IUiContainer _uiContainer;
        
        public override void InstallBindings()
        {
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