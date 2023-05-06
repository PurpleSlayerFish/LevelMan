using PurpleSlayerFish.Core.Ui.ElementManager.Elements;
using UnityEngine;

namespace PurpleSlayerFish.Core.Ui.Windows.MainMenuWindow
{
    public class MainMenuWindow : AbstractWindow<MainMenuController>
    {
        [Header("Buttons")]
        [SerializeField] private ExtendedButton _newGameBtn;
        public ExtendedButton NewGameBtn => _newGameBtn;
        [SerializeField] private ExtendedButton _loadGameBtn;
        public ExtendedButton LoadGameBtn => _loadGameBtn;
        [SerializeField] private ExtendedButton _settingBtn;
        public ExtendedButton SettingBtn => _settingBtn;
        [SerializeField] private ExtendedButton authorsBtn;
        public ExtendedButton AuthorsBtn => authorsBtn;
        [SerializeField] private ExtendedButton quitBtn;
        public ExtendedButton QuitBtn => quitBtn;
        
        [Header("Credits")]
        [SerializeField] private GameObject _credits;
        public GameObject Credits => _credits;
        
        [SerializeField] private string _gameSceneName = "GameScene";
        public string GameSceneName => _gameSceneName;
    }
}