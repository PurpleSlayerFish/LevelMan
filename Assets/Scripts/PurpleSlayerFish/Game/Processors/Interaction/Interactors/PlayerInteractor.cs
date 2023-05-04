using System;
using System.Diagnostics.CodeAnalysis;
using PurpleSlayerFish.Core.Services.Input;
using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using PurpleSlayerFish.Core.Services.ScriptableObjects.GameConfig;
using PurpleSlayerFish.Core.Services.Tooltips;
using PurpleSlayerFish.Game.Behaviours;
using PurpleSlayerFish.Game.Controllers;
using PurpleSlayerFish.Game.Processors.Combat;
using SerializableDictionary.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.InteractionProcessor
{
    public class PlayerInteractor : MonoBehaviour, IInitializable
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private IInputProvider<InputData> _inputProvider;
        [Inject] private TooltipManager _tooltipManager;
        [Inject] private ObstacleController _obstacleController;
        [Inject] private InteractionController _interactionController;
        
        public Transform PlaceToPot;
        [SerializeField] private SerializableDictionary<string, GameObject> _backPack;

        private string _busyKey;
        private ObstacleBehaviour _nearestObstacle;
        private AInteraction _nearestInteraction;
        
        public PlayerBehaviour Player;
        private bool _isBusy;
        
        private const string OBSTACLE_KEY = "StalagnateBehaviour";
        
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
            _nearestInteraction = _interactionController.CheckIntersections(transform.position, _gameConfig.PlayerAttackOffset + _gameConfig.InteractionOffset);
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

        private bool CanInteract() => _nearestInteraction != null && (!_isBusy/* || (_isBusy && _nearestInteraction.AllowIfBusy)*/);

        public void Give([NotNull] string key)
        {
            if (_busyKey != null)
                _backPack.Get(_busyKey).SetActive(false);
            _busyKey = key;
            _backPack.Get(_busyKey).SetActive(true);
            _isBusy = true;
        }
        
        public void Drop()
        {
            if (_busyKey == OBSTACLE_KEY)
                _obstacleController.SpawnObstacle((Player.CombatProcessor as PlayerCombatProcessor).AttackPivot.position);
            else
                _interactionController.Spawn((Player.CombatProcessor as PlayerCombatProcessor).AttackPivot.position, _busyKey);
            
            _backPack.Get(_busyKey).SetActive(false);
            _busyKey = null;
            _isBusy = false;
        }
    }
}