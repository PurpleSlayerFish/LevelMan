using PurpleSlayerFish.Core.Services.Input;
using PurpleSlayerFish.Core.Services.ScriptableObjects;
using PurpleSlayerFish.Core.Ui.Windows.GameWindow;
using PurpleSlayerFish.Game.Controllers.Impls;
using PurpleSlayerFish.Game.Processors.Interaction.Interactors;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.Interaction
{
    public class PotInteraction : AInteraction
    {
        [Inject] private InteractionController _interactionController;
        [Inject] private GameConfig _gameConfig;
        [Inject] private SignalBus _signalBus;
        [Inject] private IInputProvider<InputData> _inputProvider;
        [SerializeField] private string _objectKey;
        [SerializeField] private Transform _tooltipFirstPivot;
        [SerializeField] private Transform _tooltipSecondPivot;

        [SerializeField] private GameObject _stirPivot;
        [SerializeField] private GameObject _shrooms;
        [SerializeField] private GameObject _sand;
        [SerializeField] private GameObject _soop;
        [SerializeField] private GameObject _stalagnate;
        [SerializeField] private GameObject _brick;
        
        private bool _hasShrooms;
        private bool _hasSand;
        private bool _hasSoop;
        private bool _hasStalagnate;
        private bool _hasBrick;
        
        public override Transform TooltipFirstPivot => _tooltipFirstPivot;
        public override Transform TooltipSecondPivot => _tooltipSecondPivot;

        public override bool AllowInteraction(PlayerInteractor interactor)
        {
            if (_inputProvider.Data.BlockInput)
                return false;
            
            if (!interactor.IsBusy && (!_hasSand && !_hasStalagnate || _hasSand && _hasStalagnate))
                return true;

            switch (interactor.BusyKey)
            {
                case PlayerInteractor.SHROOM_KEY:
                    return _isEmpty || _hasSoop;
                case PlayerInteractor.SAND_KEY:
                    return _isEmpty || _hasStalagnate;
                case PlayerInteractor.STALAGNATE_KEY:
                    return _isEmpty || _hasSand;
                default:
                    return false;
            }
        }

        private bool _isEmpty => !(_hasShrooms || _hasSand || _hasSoop || _hasStalagnate || _hasBrick);

        private void Awake()
        {
            _interactionController.AddPot(this);
        }

        public override void Interact(PlayerInteractor interactor)
        {
            if (_isEmpty)
                HandleEmpty(interactor);
            else if (_hasShrooms)
            {
                HandleShrooms(interactor);
            }
            else if (_hasSoop)
            {
                HandleSoop(interactor);
            }
            else if (_hasSand && _hasStalagnate)
                HandleSandAndStalagnate(interactor);
            else if (_hasSand)
            {
                HandleSand(interactor);
            }
            else if (_hasStalagnate)
            {
                HandleStalagnate(interactor);
            }
            else if (_hasBrick)
            {
                HandleBrick(interactor);
            }
        }

        private void HandleEmpty(PlayerInteractor interactor)
        {
            if (interactor.IsBusy)
            {
                WhatDo(interactor.BusyKey, true);
                interactor.SetEmpty();
            }
            else
            {
                interactor.Give(_objectKey);
                _interactionController.HidePot();
            }
        }
        
        private void HandleShrooms(PlayerInteractor interactor)
        {
            WhatDo(PlayerInteractor.SHROOM_KEY, false);
            WhatDo(PlayerInteractor.SOOP_KEY, true);
            interactor.Player.Animate(_stirPivot.transform, _gameConfig.PlayerStirAnimation, _gameConfig.PlayerStirAnimationDuration);
        }
        
        private void HandleSoop(PlayerInteractor interactor)
        {
            WhatDo(PlayerInteractor.SOOP_KEY, false);
            interactor.Player.CombatProcessor.ChangeHealth(-_gameConfig.ShroomsHeal);
            _signalBus.Fire(new HealthSubscription{HealthPercent = interactor.Player.CombatProcessor.HealthPercent});
        }
        
        private void HandleSand(PlayerInteractor interactor)
        {
            if (interactor.BusyKey == PlayerInteractor.STALAGNATE_KEY)
            {
                WhatDo(PlayerInteractor.STALAGNATE_KEY, true);
                interactor.SetEmpty();
            }
        }
        
        
        private void HandleStalagnate(PlayerInteractor interactor)
        {
            if (interactor.BusyKey == PlayerInteractor.SAND_KEY)
            {
                WhatDo(PlayerInteractor.SAND_KEY, true);
                interactor.SetEmpty();
            }
        }
        
        private void HandleSandAndStalagnate(PlayerInteractor interactor)
        {
            WhatDo(PlayerInteractor.SAND_KEY, false);
            WhatDo(PlayerInteractor.STALAGNATE_KEY, false);
            WhatDo(PlayerInteractor.BRICK_KEY, true);
            interactor.Player.Animate(_stirPivot.transform, _gameConfig.PlayerStirAnimation, _gameConfig.PlayerStirAnimationDuration);
        }
        
        private void HandleBrick(PlayerInteractor interactor)
        {
            WhatDo(PlayerInteractor.BRICK_KEY, false);
            interactor.Give(PlayerInteractor.BRICK_KEY);
        }

        private void WhatDo(string key, bool value)
        {
            switch (key)
            {
                case PlayerInteractor.SHROOM_KEY:
                    _shrooms.SetActive(value);
                    _hasShrooms = value;
                    break;
                case PlayerInteractor.SAND_KEY:
                    _sand.SetActive(value);
                    _hasSand = value;
                    break;
                case PlayerInteractor.STALAGNATE_KEY:
                    _stalagnate.SetActive(value);
                    _hasStalagnate = value;
                    break;
                case PlayerInteractor.BRICK_KEY:
                    _brick.SetActive(value);
                    _hasBrick = value;
                    break;
                case PlayerInteractor.SOOP_KEY:
                    _soop.SetActive(value);
                    _hasSoop = value;
                    break;
            }
        }
    }
}