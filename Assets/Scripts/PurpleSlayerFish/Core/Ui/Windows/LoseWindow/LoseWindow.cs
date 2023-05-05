using PurpleSlayerFish.Core.Ui.ElementManager.Elements;
using UnityEngine;

namespace PurpleSlayerFish.Core.Ui.Windows.PauseWindow
{
    public class LoseWindow : AbstractWindow<LoseController>
    {
        [Header("Quit")]
        [SerializeField] private string _mainMenuScene = "MainMenuScene";
        public string MainMenuScene => _mainMenuScene;
        [SerializeField] private ExtendedButton _restartButton;
        public ExtendedButton RestartButton => _restartButton;
        [SerializeField] private ExtendedButton _mainMenuButton;
        public ExtendedButton MainMenuButton => _mainMenuButton;
    }
}