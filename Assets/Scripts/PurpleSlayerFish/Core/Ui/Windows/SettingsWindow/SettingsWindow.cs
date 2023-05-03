using PurpleSlayerFish.Core.Ui.ElementManager.Elements;
using UnityEngine;

namespace PurpleSlayerFish.Core.Ui.Windows.ShopWindow
{
    public class SettingsWindow : AbstractWindow<SettingsController>
    {
        [Header("Settings Boxes")] 
        [SerializeField] private ExternalCheckBox _soundCheck;
        public ExternalCheckBox SoundCheck => _soundCheck;
        [Header("Exit")] 
        [SerializeField] private ExtendedButton _exitButton;
        public ExtendedButton ExitButton => _exitButton;
    }
}