using PurpleSlayerFish.Core.Services.Pause;
using PurpleSlayerFish.Core.Services.SceneLoader;
using Zenject;

namespace PurpleSlayerFish.Core.Ui.Windows.PauseWindow
{
    public class LoseController : AbstractController<LoseWindow>
    {
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private IPauseService _pauseService;

        protected override void AfterInitialize()
        {
            OnBeforeShow += () => _pauseService.SetPause(true);
            _window.RestartButton.AddOnClick(Restart);
            _window.MainMenuButton.AddOnClick(Quit);
        }

        private void Restart()
        {
            _pauseService.SetPause(false);
            _sceneLoader.Reload();
            
            SetInteractable(false);
        }
        
        private void Quit()
        {
            _pauseService.SetPause(false);
            _sceneLoader.Load(_window.MainMenuScene);
            SetInteractable(false);
        }
    }
}