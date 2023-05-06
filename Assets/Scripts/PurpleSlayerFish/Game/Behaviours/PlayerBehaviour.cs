using Cinemachine;
using DG.Tweening;
using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Core.Services;
using PurpleSlayerFish.Core.Services.Input;
using PurpleSlayerFish.Core.Services.ScriptableObjects;
using PurpleSlayerFish.Core.Services.Tooltips;
using PurpleSlayerFish.Game.Controllers;
using PurpleSlayerFish.Game.Processors;
using PurpleSlayerFish.Game.Processors.Animation;
using PurpleSlayerFish.Game.Processors.Combat;
using PurpleSlayerFish.Game.Processors.Combat.Impls;
using PurpleSlayerFish.Game.Processors.Interaction.Interactors;
using PurpleSlayerFish.Game.Processors.Movement;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace PurpleSlayerFish.Game.Behaviours
{
    public class PlayerBehaviour : AbstractBehaviour, ICombat
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private IInputProvider<InputData> _inputProvider;
        [Inject] private CinemachineVirtualCamera _virtualCamera;
        
        [SerializeField] private AbstractCombatProcessor _combatProcessor;
        
        private Vector3 _direction;
        private MathUtils _mathUtils;
        
        public ManagedNavAgentMovementProcessor MovementProcessor;
        public AnimationProcessor AnimationProcessor;
        public PlayerInteractor Interactor;
        public LightProcessor LightProcessor;

        public AbstractCombatProcessor CombatProcessor => _combatProcessor;

        public override void Initialize()
        {
            CombatProcessor.Initialize();
            CombatProcessor.AnimationProcessor = AnimationProcessor;
            Interactor.Initialize();
            _inputProvider.Data.OnAttack += () =>
            {
                // transform.rotation
                CombatProcessor.Attack();
                // CombatProcessor.OnAfterAttack;
            };
            LightProcessor.Initialize();
        }

        public void Animate(Transform pivot, int animation, float duration)
        {
            _inputProvider.Data.BlockInput = true;
            MovementProcessor.Agent.enabled = false;
            transform.position = pivot.position;
            transform.rotation = pivot.rotation;
            AnimationProcessor.WalkingState(animation);
            DOVirtual.DelayedCall(duration,StopAnimate);

            void StopAnimate()
            {
                _inputProvider.Data.BlockInput = false;
                MovementProcessor.Agent.enabled = true;
                AnimationProcessor.WalkingState(_gameConfig.PlayerIdleAnimation);
            }
        }

        private void Update()
        {
            CalculateDirection();
            MovementProcessor.Move(_direction);
            SetWalkAnimation();
        }

        private void SetWalkAnimation()
        {
            if (!MovementProcessor.Agent.enabled)
                return;
            
            AnimationProcessor.WalkingState(_direction.x == 0 && _direction.z == 0 
                ? _gameConfig.PlayerIdleAnimation 
                : Interactor.IsBusy ? _gameConfig.PlayerWalkingAnimation : _gameConfig.PlayerRunAnimation);
        }

        private void CalculateDirection()
        {
            _direction = new Vector3(_inputProvider.Data.HorizontalAxis, transform.position.y,
                _inputProvider.Data.VerticalAxis).normalized;
            _direction = _mathUtils.Direction3dFromRotate(_virtualCamera.transform.eulerAngles.y, _direction);
        }
    }
}