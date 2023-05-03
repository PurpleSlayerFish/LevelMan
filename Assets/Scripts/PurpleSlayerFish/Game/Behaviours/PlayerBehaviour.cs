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
        [Inject] private TooltipProvider _tooltipProvider;
        [Inject] private IInputProvider<InputData> _inputProvider;
        [Inject] private CinemachineVirtualCamera _virtualCamera;
        [Inject] private ObstacleController _obstacleController;
        
        [SerializeField] private AbstractCombatProcessor _combatProcessor;
        
        private Vector3 _direction;
        private MathUtils _mathUtils;
        
        public ManagedNavAgentMovementProcessor MovementProcessor;
        public AnimationProcessor AnimationProcessor;
        public PlayerInteractionProcessor InteractionProcessor;
        public ObstacleBehaviour NearestObstacle;

        public AbstractCombatProcessor CombatProcessor => _combatProcessor;

        public override void Initialize()
        {
            CombatProcessor.Initialize();
            CombatProcessor.AnimationProcessor = AnimationProcessor;

            // Install PlayerInput
            _inputProvider.Data.OnActionMain += ActionPriority;
            _inputProvider.Data.OnActionSecond += () =>
            {
                // _obstacleController.SpawnObstacle(player);
            };
            _inputProvider.Data.OnAttack += () => CombatProcessor.Attack();

            MovementProcessor.playerBehaviour = this;
        }

        private void Update()
        {
            CalculateDirection();
            MovementProcessor.Move(_direction);
            AnimationProcessor.WalkingState(_direction);
            CheckNearestObstacle();
        }

        private void CalculateDirection()
        {
            _direction = new Vector3(_inputProvider.Data.HorizontalAxis, transform.position.y,
                _inputProvider.Data.VerticalAxis).normalized;
            _direction = _mathUtils.Direction3dFromRotate(_virtualCamera.transform.eulerAngles.y, _direction);
        }

        private void CheckNearestObstacle() => NearestObstacle = _obstacleController.CheckNearestObstacle(transform.position);

        private void TryJump()
        {
            if (NearestObstacle == null)
                return;
            MovementProcessor.JumpOverObstacle(NearestObstacle);
        }

        private void ActionPriority()
        {
            TryJump();
        }
    }
}