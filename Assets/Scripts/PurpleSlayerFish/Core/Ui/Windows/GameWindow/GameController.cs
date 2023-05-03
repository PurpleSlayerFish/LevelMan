using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Services;
using PurpleSlayerFish.Core.Services.DataStorage;
using TMPro;
using Zenject;

namespace PurpleSlayerFish.Core.Ui.Windows.GameWindow
{
    public class GameController : AbstractController<GameWindow>
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private IDataStorage<PlayerData> _dataStorage;

        private StringUtils _stringUtils = new();
        private MathUtils _mathUtils = new();

        protected override void AfterInitialize() => _signalBus.Subscribe<ScoreSubscription>(UpdateScore);
        private void UpdateScore(ScoreSubscription value) => UpdateText(_window.Score, value.Score.ToString());
        private void UpdateText(TMP_Text tmpText, string value) => tmpText.text = value;
    }

    public struct ScoreSubscription
    {
        public int Score;
    }
}