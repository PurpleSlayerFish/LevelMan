using Cinemachine;
using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Core.Services;
using PurpleSlayerFish.Core.Services.Input;
using PurpleSlayerFish.Core.Services.Tooltips;
using PurpleSlayerFish.Game.Behaviours.Animation;
using PurpleSlayerFish.Game.Behaviours.Movement;
using PurpleSlayerFish.Game.Controllers;
using PurpleSlayerFish.Game.Processors.Combat;
using PurpleSlayerFish.Game.Processors.Combat.Impls;
using PurpleSlayerFish.Game.Processors.InteractionProcessor;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace PurpleSlayerFish.Game.Behaviours
{
    public class PlayerBehaviour : AbstractBehaviour, ICombat
    {
        [Inject] private TooltipManager _tooltipManager;
        [Inject] private IInputProvider<InputData> _inputProvider;
        [Inject] private CinemachineVirtualCamera _virtualCamera;
        
        [SerializeField] private AbstractCombatProcessor _combatProcessor;
        
        private Vector3 _direction;
        private MathUtils _mathUtils;
        
        public ManagedNavAgentMovementProcessor MovementProcessor;
        public AnimationProcessor AnimationProcessor;
        public PlayerInteractor Interactor;

        public AbstractCombatProcessor CombatProcessor => _combatProcessor;

        public override void Initialize()
        {
            CombatProcessor.Initialize();
            CombatProcessor.AnimationProcessor = AnimationProcessor;
            Interactor.Initialize();
            MovementProcessor.playerBehaviour = this;
            _inputProvider.Data.OnAttack += () => CombatProcessor.Attack();
        }

        private void Update()
        {
            CalculateDirection();
            MovementProcessor.Move(_direction);
            AnimationProcessor.WalkingState(_direction);
        }

        private void CalculateDirection()
        {
            _direction = new Vector3(_inputProvider.Data.HorizontalAxis, transform.position.y,
                _inputProvider.Data.VerticalAxis).normalized;
            _direction = _mathUtils.Direction3dFromRotate(_virtualCamera.transform.eulerAngles.y, _direction);
        }
    }
}