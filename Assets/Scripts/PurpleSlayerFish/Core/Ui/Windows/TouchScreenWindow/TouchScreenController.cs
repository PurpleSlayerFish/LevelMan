using PurpleSlayerFish.Core.Services.Input;
using Zenject;

namespace PurpleSlayerFish.Core.Ui.Windows.TouchScreenWindow
{
    public class TouchScreenController : AbstractController<TouchScreenWindow>, ITickable
    {
        [Inject] private IInputProvider<InputData> _inputProvider;
        
        protected override void AfterInitialize()
        {
            InstallJoystickPosition();
            _window.ActionButton.onClick.AddListener(() => _inputProvider.Data.OnActionMain.Invoke());
            _window.PauseButton.onClick.AddListener(() => _inputProvider.Data.OnEscape.Invoke());
        }

        public void Tick()
        {
            _inputProvider.Data.HorizontalAxis = _window.Joystick.HorizontalAxis;
            _inputProvider.Data.VerticalAxis = _window.Joystick.VerticalAxis;
        }
        
        private void InstallJoystickPosition()
        {
            var joystick = _window.Joystick;
            var pos = joystick.joystick.anchoredPosition;
            joystick.joystickBase.anchoredPosition = pos;
            joystick.highlightBase.rectTransform.anchoredPosition = pos;
            joystick.tensionAccentDown.rectTransform.anchoredPosition = pos;
            joystick.tensionAccentLeft.rectTransform.anchoredPosition = pos;
            joystick.tensionAccentRight.rectTransform.anchoredPosition = pos;
            joystick.tensionAccentUp.rectTransform.anchoredPosition = pos;
        }
    }
}