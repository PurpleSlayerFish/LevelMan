using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace PurpleSlayerFish.Core.Services.Input
{
    public class PlayerInputAdapter : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        
        [Inject] private IInputProvider<InputData> _inputProvider;

        // todo sub/unsub
        private void Start()
        {
            var actions = _playerInput.actions;
            actions["HorizontalAxis"].performed += OnHorizontal;
            actions["HorizontalAxis"].canceled += OnHorizontal;
            actions["VerticalAxis"].performed += OnVertical;
            actions["VerticalAxis"].canceled += OnVertical;
            actions["Escape"].performed += OnEscape;
            actions["ActionMain"].performed += OnActionMain;
            actions["ActionSecond"].performed += OnActionSecond;
            actions["Attack"].performed += OnAttack;
        }

        private void OnDestroy()
        {
            var actions = _playerInput.actions;
            actions["HorizontalAxis"].performed -= OnHorizontal;
            actions["HorizontalAxis"].canceled -= OnHorizontal;
            actions["VerticalAxis"].performed -= OnVertical;
            actions["VerticalAxis"].canceled -= OnVertical;
            actions["Escape"].performed -= OnEscape;
            actions["ActionMain"].performed -= OnActionMain;
            actions["ActionSecond"].performed -= OnActionSecond;
            actions["Attack"].performed -= OnAttack;
        }

        private void OnHorizontal(InputAction.CallbackContext ctx) => _inputProvider.Data.HorizontalAxis = ctx.ReadValue<float>();
        private void OnVertical(InputAction.CallbackContext ctx) => _inputProvider.Data.VerticalAxis = ctx.ReadValue<float>();
        private void OnEscape(InputAction.CallbackContext ctx) => _inputProvider.Data.OnEscape?.Invoke();
        private void OnActionMain(InputAction.CallbackContext ctx) => _inputProvider.Data.OnActionMain?.Invoke();
        private void OnActionSecond(InputAction.CallbackContext ctx) => _inputProvider.Data.OnActionSecond?.Invoke();
        private void OnAttack(InputAction.CallbackContext ctx) => _inputProvider.Data.OnAttack?.Invoke();
    }
}