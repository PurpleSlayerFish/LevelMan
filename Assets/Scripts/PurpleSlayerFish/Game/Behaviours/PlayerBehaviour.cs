using Cinemachine;
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
            _inputProvider.Data.OnAttack += () => CombatProcessor.Attack();
            LightProcessor.Initialize();
        }

        public void AnimateStir()
        {
            
        }

        private void Update()
        {
            CalculateDirection();
            MovementProcessor.Move(_direction);
            SetWalkAnimation();
        }

        private void SetWalkAnimation()
        {
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