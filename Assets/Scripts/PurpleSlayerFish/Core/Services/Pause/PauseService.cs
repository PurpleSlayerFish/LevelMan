using PurpleSlayerFish.Core.Ui.Container;
using PurpleSlayerFish.Core.Ui.Windows.PauseWindow;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Core.Services.Pause
{
    public class PauseService : IPauseService
    {
        [Inject] private IUiContainer _uiContainer;

        private float _cashedTimeScale = 1;
        
        public bool IsPaused { get; set; }
        
        public void SetPause(bool value)
        {
            if (IsPaused == value)
                return;
            if (value)
                _cashedTimeScale = Time.timeScale;
            Time.timeScale = value ? 0 : _cashedTimeScale;
            IsPaused = value;
        }

        public void UpdatePauseWindow()
        {
            if (IsPaused)
                _uiContainer.Show<PauseController>();
            else
                _uiContainer.Hide<PauseController>();
        }
        
    }
}