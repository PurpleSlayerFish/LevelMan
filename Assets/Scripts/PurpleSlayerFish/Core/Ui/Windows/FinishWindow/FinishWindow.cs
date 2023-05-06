using PurpleSlayerFish.Core.Ui.ElementManager.Elements;
using UnityEngine;

namespace PurpleSlayerFish.Core.Ui.Windows.PauseWindow
{
    public class FinishWindow : AbstractWindow<FinishController>
    {
        [Header("Quit")]
        [SerializeField] private string _mainMenuScene = "MainMenuScene";
        public string MainMenuScene => _mainMenuScene;
        [SerializeField] private ExtendedButton _mainMenuButton;
        public ExtendedButton MainMenuButton => _mainMenuButton;
        
        public ExtendedButton Finish1Complete;
        public GameObject Finish2;
    }
}