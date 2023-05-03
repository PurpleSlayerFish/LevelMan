using UnityEngine;
using UnityEngine.UI;

namespace PurpleSlayerFish.Core.Ui.Windows.GameWindow
{
    public class TouchScreenWindow : AbstractWindow<TouchScreenController>
    {
        [Header("Controls")]
        [SerializeField] private UltimateJoystick _joystick;
        public UltimateJoystick Joystick => _joystick;
        [SerializeField] private Button _actionButton;
        public Button ActionButton => _actionButton;
        [SerializeField] private Button _pauseButton;
        public Button PauseButton => _pauseButton;
    }
}