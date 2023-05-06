using System;
using System.Collections.Generic;
using PurpleSlayerFish.Core.Services.Pause;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace PurpleSlayerFish.Core.Services.Input
{
    public class PlayerInputAdapter : MonoBehaviour
    {
        [Inject] private IInputProvider<InputData> _inputProvider;
        [Inject] private IPauseService _pauseService;

        [SerializeField] private PlayerInput _playerInput;

        private List<InputSubscriber> _subscribers = new();

        private void Start()
        {
            _playerInput.actions["Escape"].performed += OnEscape;
            Subscribe(new InputSubscriber("HorizontalAxis", OnHorizontal, OnHorizontal));
            Subscribe(new InputSubscriber("VerticalAxis", OnVertical, OnVertical));
            Subscribe(new InputSubscriber("ActionMain", OnActionMain, null));
            Subscribe(new InputSubscriber("ActionSecond", OnActionSecond, null));
            Subscribe(new InputSubscriber("Attack", OnAttack, null));
        }

        private void OnDestroy()
        {
            _playerInput.actions["Escape"].performed -= OnEscape;
            Unsubscribe();
        }

        private void Subscribe(InputSubscriber sub)
        {
            _subscribers.Add(sub);
            if (sub.PerformedAction != null)
                _playerInput.actions[sub.Name].performed += sub.PerformedAction;
            if (sub.CanceledAction != null)
                _playerInput.actions[sub.Name].canceled += sub.PerformedAction;
        }

        private void Unsubscribe()
        {
            for (int i = 0; i < _subscribers.Count; i++)
            {
                if (_subscribers[i].PerformedAction != null)
                    _playerInput.actions[_subscribers[i].Name].performed -= _subscribers[i].PerformedAction;
                if (_subscribers[i].CanceledAction != null)
                    _playerInput.actions[_subscribers[i].Name].canceled -= _subscribers[i].PerformedAction;
                _subscribers.Remove(_subscribers[i]);
            }
        }

        private bool _condition => _pauseService.IsPaused || _inputProvider.Data.BlockInput; 
        
        // todo придумать что-то на экшенах и предикантах
        private void OnHorizontal(InputAction.CallbackContext ctx)
        {
            if (_condition)
                return;
            _inputProvider.Data.HorizontalAxis = ctx.ReadValue<float>();
        }
        private void OnVertical(InputAction.CallbackContext ctx)
        {
            if (_condition)
                return;
            _inputProvider.Data.VerticalAxis = ctx.ReadValue<float>();
        }
        private void OnActionMain(InputAction.CallbackContext ctx)
        {
            if (_condition)
                return;
            _inputProvider.Data.OnActionMain?.Invoke();
        }
        private void OnActionSecond(InputAction.CallbackContext ctx)
        {
            if (_condition)
                return;
            _inputProvider.Data.OnActionSecond?.Invoke();
        }
        private void OnAttack(InputAction.CallbackContext ctx)
        {
            if (_condition)
                return;
            _inputProvider.Data.OnAttack?.Invoke();
        }

        private void OnEscape(InputAction.CallbackContext ctx) => _inputProvider.Data.OnEscape?.Invoke();

        private struct InputSubscriber
        {
            public string Name;
            public Action<InputAction.CallbackContext> PerformedAction;
            public Action<InputAction.CallbackContext> CanceledAction;

            public InputSubscriber(string name, Action<InputAction.CallbackContext> performedAction, Action<InputAction.CallbackContext> canceledAction)
            {
                Name = name;
                PerformedAction = performedAction;
                CanceledAction = canceledAction;
            }
        }
    }
}