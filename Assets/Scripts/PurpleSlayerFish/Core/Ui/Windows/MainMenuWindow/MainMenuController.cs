using System.Threading.Tasks;
using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Services.DataStorage;
using PurpleSlayerFish.Core.Services.SceneLoader;
using PurpleSlayerFish.Core.Ui.Container;
using PurpleSlayerFish.Core.Ui.Windows.SettingsWindow;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Core.Ui.Windows.MainMenuWindow
{
    public class MainMenuController : AbstractController<MainMenuWindow>
    {
        [Inject] private IUiContainer _uiContainer;
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private IDataStorage<PlayerData> _dataStorage;
        
        protected override void AfterInitialize()
        {
            _tempPosition2 = _window.Credits.transform.position;
            _window.NewGameBtn.AddOnClick(NewGame);
            _window.LoadGameBtn.AddOnClick(LoadGame);
            _window.AuthorsBtn.AddOnClick(InitAuthors);
            _window.AuthorsBackBtn.AddOnClick(() => _window.Credits.SetActive(false));
            // _window.SettingBtn.AddOnClick(() => _uiContainer.Show<SettingsController>());
            _window.QuitBtn.AddOnClick(BuildQuitDialog);
            _window.LoadGameBtn.Button.interactable = !_dataStorage.CurrentData.IsNew;
        }

        private void NewGame()
        {
            _dataStorage.Clear();
            _sceneLoader.Load(_window.GameSceneName);
        }

        private void LoadGame() => _sceneLoader.Load(_window.GameSceneName);

        private void BuildQuitDialog()
        {
            _uiContainer.BuildDialog()
                .WithLabel("Quit")
                .WithDescription("Are you sure want to quit the game?")
                .WithButton("Yes!", () => Quit())
                .WithButton("No!", () => SetInteractable(true), true)
                .Build()
                .Show();

            SetInteractable(false);

            void Quit()
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                UnityEngine.Application.Quit();
#endif
            }
        }

        private Vector3 _tempPosition;
        private Vector3 _tempPosition2;
        private async void InitAuthors()
        {
            _window.CreditsText.transform.position = _tempPosition2;
            _window.Credits.SetActive(true);
            while (_window.Credits.activeSelf)
            {
                _tempPosition = _window.CreditsText.transform.position;
                _tempPosition.y += _window.ScrollSpeed * Time.deltaTime;
                _window.CreditsText.transform.position = _tempPosition;
                await Task.Yield();
            }
        }
    }
}