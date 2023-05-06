using System.Threading.Tasks;
using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Services.DataStorage;
using PurpleSlayerFish.Core.Services.Pause;
using PurpleSlayerFish.Core.Services.SceneLoader;
using PurpleSlayerFish.Core.Ui.Container;
using Zenject;

namespace PurpleSlayerFish.Core.Ui.Windows.PauseWindow
{
    public class FinishController : AbstractController<FinishWindow>
    {
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private IPauseService _pauseService;

        protected override void AfterInitialize()
        {
            OnBeforeShow += EnableFinish;
            _window.MainMenuButton.AddOnClick(Quit);
        }

        private void Quit()
        {
            _pauseService.SetPause(false);
            _sceneLoader.Load(_window.MainMenuScene);
            
            SetInteractable(false);
        }
        
        private void EnableFinish()
        {
            _pauseService.SetPause(true);
            _window.Finish1Complete.AddOnClick(() => _window.Finish2.SetActive(true));
        }
    }
}