using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Services;
using PurpleSlayerFish.Core.Services.DataStorage;
using PurpleSlayerFish.Core.Services.Pause;
using PurpleSlayerFish.Core.Services.ScriptableObjects;
using PurpleSlayerFish.Core.Ui.ElementManager.Elements;
using TMPro;
using Zenject;

namespace PurpleSlayerFish.Core.Ui.Windows.GameWindow
{
    public class GameController : AbstractController<GameWindow>
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private IDataStorage<PlayerData> _dataStorage;
        [Inject] private IPauseService _pauseService;

        private StringUtils _stringUtils = new();
        private MathUtils _mathUtils = new();

        protected override void AfterInitialize()
        {
            if (_dataStorage.CurrentData.IsNew)
                EnableTutorial();
            _signalBus.Subscribe<HealthSubscription>(UpdateHealth);
            _signalBus.Subscribe<ScoreSubscription>(UpdateScore);
        }

        private void UpdateHealth(HealthSubscription value) => _window.Bar.FillAmount(value.HealthPercent);
        private void UpdateScore(ScoreSubscription value) => UpdateText(_window.Score, value.Score.ToString());
        private void UpdateText(TMP_Text tmpText, string value) => tmpText.text = value;

        private void EnableTutorial()
        {
            _pauseService.SetPause(true);
            _window.Tutorial1.SetActive(true);
            _window.Tutorial1Complete.AddOnClick(EnableTutorial2);
            _window.Tutorial2Complete.AddOnClick(DisableTutorial);

            void EnableTutorial2()
            {
                _window.Tutorial1.SetActive(false);
                _window.Tutorial2.SetActive(true);
            }
            
            void DisableTutorial()
            {
                _window.Tutorial2.SetActive(false);
                _pauseService.SetPause(false);
            }
        }
        
    }

    public struct ScoreSubscription
    {
        public int Score;
    }
    
    public struct HealthSubscription
    {
        public float HealthPercent;
    }
}