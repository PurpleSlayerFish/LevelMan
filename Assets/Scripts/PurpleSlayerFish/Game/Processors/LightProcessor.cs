using System;
using PurpleSlayerFish.Core.Services.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors
{
    public class LightProcessor : MonoBehaviour, IInitializable
    {
        [Inject] private GameConfig _gameConfig;

        [SerializeField] private Light _mainPlayerLight;
        [SerializeField] private Light _light1;
        [SerializeField] private Light _light2;
        
        private float _currentLightLevel;
        private float _duration;
        
        public void Initialize()
        {
            _currentLightLevel = _gameConfig.PlayerStartLightLevel;
            _duration = _gameConfig.LightBugDuration;
        }

        private void Update()
        {
            _duration -= Time.deltaTime;
            _duration = Mathf.Max(0, _duration);
            _currentLightLevel = Mathf.Lerp(_gameConfig.PlayerMinLightLevel, _gameConfig.PlayerMaxLightLevel,
                _duration / _gameConfig.LightBugDuration);
            _mainPlayerLight.intensity = _currentLightLevel;
        }

        public void MaximizeLight(float hsvH)
        {
            Color.RGBToHSV(_mainPlayerLight.color, out float hsvH2, out float hsvS, out float hsvV);
            _currentLightLevel = _gameConfig.PlayerMaxLightLevel;
            var newColor = Color.HSVToRGB(hsvH, hsvS, hsvV);
            _mainPlayerLight.color = newColor;
            _light1.color = newColor;
            _light2.color = newColor;
        }
    }
}