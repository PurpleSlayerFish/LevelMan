using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Services.DataStorage;
using PurpleSlayerFish.Core.Services.Pause;
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
        [Inject] private IDataStorage<PlayerData> _dataStorage;

        protected override void AfterInitialize()
        {
            _window.PlayButton.AddOnClick(DisablePause);
            _window.QuitButton.AddOnClick(Quit);
        }

        private void DisablePause()
        {
            _pauseService.SetPause(false);
            _pauseService.UpdatePauseWindow();
        }

        private void Quit()
        {
            _uiContainer.BuildDialog()
                .WithLabel("Quit?")
                .WithDescription("Go to the main menu?")
                .WithButton("Yes!", () =>
                {
                    _dataStorage.SaveCurrent();
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