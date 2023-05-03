using PurpleSlayerFish.Core.Services.PauseService;
using PurpleSlayerFish.Core.Services.SceneLoader;
using PurpleSlayerFish.Core.Ui.Container;
using Zenject;

namespace PurpleSlayerFish.Core.Ui.Windows.PauseWindow
{
    public class PauseController : AbstractController<PauseWindow>
    {
        [Inject] private IUiContainer _uiContainer;
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private IPauseService _pauseService;

        protected override void AfterInitialize()
        {
            _window.PlayButton.AddOnClick(() => _pauseService.SetPause(false));
            _window.QuitButton.AddOnClick(Quit);
        }

        private void Quit()
        {
            _uiContainer.BuildDialog()
                .WithLabel("Quit?")
                .WithDescription("Go to the main menu?")
                .WithButton("Yes!", () =>
                {
                    _pauseService.SetPause(false);
                    _sceneLoader.Load(_window.MainMenuScene);
                })
                .WithButton("No!", () => SetInteractable(true), true)
                .Build()
                .Show();
            
            SetInteractable(false);
        }
    }
}