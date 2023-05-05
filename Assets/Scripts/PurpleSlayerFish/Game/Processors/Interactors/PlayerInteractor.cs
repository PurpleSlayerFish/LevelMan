using System.Diagnostics.CodeAnalysis;
using PurpleSlayerFish.Core.Services.Input;
using PurpleSlayerFish.Core.Services.ScriptableObjects;
using PurpleSlayerFish.Core.Services.Tooltips;
using PurpleSlayerFish.Game.Behaviours;
using PurpleSlayerFish.Game.Controllers.Impls;
using PurpleSlayerFish.Game.Processors.Combat.Impls;
using SerializableDictionary.Scripts;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.Interaction.Interactors
{
    public class PlayerInteractor : MonoBehaviour, IInitializable
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private IInputProvider<InputData> _inputProvider;
        [Inject] private TooltipManager _tooltipManager;
        [Inject] private ObstacleController _obstacleController;
        [Inject] private InteractionController _interactionController;
        
        [SerializeField] private SerializableDictionary<string, GameObject> _backPack;

        private string _busyKey;
        private ObstacleBehaviour _nearestObstacle;
        private AInteraction _nearestInteraction;
        
        public PlayerBehaviour Player;
        private bool _isBusy;
        
        public const string SHROOM_KEY = "ShroomBehaviour";
        public const string SAND_KEY = "SandBehaviour";
        public const string STALAGNATE_KEY = "StalagnateBehaviour";
        public const string BRICK_KEY = "BrickBehaviour";
        public const string SOOP_KEY = "SoopBehaviour";
        
        public bool IsBusy => _isBusy;
        public string BusyKey => _busyKey;
        
        public void Initialize()
        {
            _inputProvider.Data.OnActionMain += TryInteract;
            _inputProvider.Data.OnActionSecond += TryJump;
        }

        private void Update()
        {
            CheckNearestObstacle();
            CheckNearestInteractions();
        }
        
        private void CheckNearestObstacle()
        {
            _nearestObstacle = _obstacleController.CheckNearestObstacle(transform.position);
            _tooltipManager.SecondActionTooltip(_nearestObstacle == null ? null : _nearestObstacle.ObstacleInteraction.TooltipSecondPivot);
        }

        private void CheckNearestInteractions()
        {
            _nearestInteraction = _interactionController.CheckIntersections(transform.position, _gameConfig.PlayerInterationOffset);
            _tooltipManager.InteractionTooltip((_nearestInteraction == null || !CanInteract()) ? null : _nearestInteraction.TooltipFirstPivot);
        }
            
        private void TryJump()
        {
            if (_nearestObstacle == null)
                return;
            Player.MovementProcessor.JumpOverObstacle(_nearestObstacle);
        }

        private void TryInteract()
        {
            if (CanInteract())
                _nearestInteraction.Interact(this);
            else if (_isBusy)
                Drop();
        }

        private bool CanInteract() => _nearestInteraction != null && _nearestInteraction.AllowInteraction(this);

        public void Give([NotNull] string key)
        {
            if (_busyKey != null)
                _backPack.Get(_busyKey).SetActive(false);
            _busyKey = key;
            _backPack.Get(_busyKey).SetActive(true);
            SetBusy(true);
        }
        
        public void Drop()
        {
            if (_busyKey == STALAGNATE_KEY)
                _obstacleController.SpawnObstacle((Player.CombatProcessor as PlayerCombatProcessor).AttackPivot.position);
            else
                _interactionController.Spawn((Player.CombatProcessor as PlayerCombatProcessor).AttackPivot.position, _busyKey);
            SetEmpty();
        }

        public void SetEmpty()
        {
            _backPack.Get(_busyKey).SetActive(false);
            _busyKey = null;
            SetBusy(false);
        }

        public void SetBusy(bool value)
        {
            _isBusy = value;
            Player.MovementProcessor.Agent.speed =
                _isBusy ? _gameConfig.PlayerBusySpeed : _gameConfig.PlayerNormalSpeed;
        }
    }
}