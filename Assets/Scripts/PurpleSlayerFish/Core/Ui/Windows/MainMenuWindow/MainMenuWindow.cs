using PurpleSlayerFish.Core.Ui.ElementManager.Elements;
using TMPro;
using UnityEngine;

namespace PurpleSlayerFish.Core.Ui.Windows.MainMenuWindow
{
    public class MainMenuWindow : AbstractWindow<MainMenuController>
    {
        [Header("General")]
        [SerializeField] private string _gameSceneName = "GameScene";
        public string GameSceneName => _gameSceneName;
        
        [Header("Buttons")]
        [SerializeField] private ExtendedButton _newGameBtn;
        public ExtendedButton NewGameBtn => _newGameBtn;
        [SerializeField] private ExtendedButton _loadGameBtn;
        public ExtendedButton LoadGameBtn => _loadGameBtn;
        [SerializeField] private ExtendedButton _settingBtn;
        public ExtendedButton SettingBtn => _settingBtn;
        [SerializeField] private ExtendedButton quitBtn;
        public ExtendedButton QuitBtn => quitBtn;
        
        [Header("Credits")]
        [SerializeField] private GameObject _credits;
        public GameObject Credits => _credits;
        [SerializeField] private TMP_Text _creditsText;
        public TMP_Text CreditsText => _creditsText;
        [SerializeField] private float _scrollSpeed = 1f;
        public float ScrollSpeed => _scrollSpeed;
        [SerializeField] private ExtendedButton _authorsBtn;
        public ExtendedButton AuthorsBtn => _authorsBtn;
        [SerializeField] private ExtendedButton _authorsBackBtn;
        public ExtendedButton AuthorsBackBtn => _authorsBackBtn;
        
    }
}