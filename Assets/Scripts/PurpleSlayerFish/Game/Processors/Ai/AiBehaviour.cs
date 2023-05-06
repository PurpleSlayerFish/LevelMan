using System;
using System.Drawing.Printing;
using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Core.Services.ScriptableObjects;
using PurpleSlayerFish.Game.Controllers;
using PurpleSlayerFish.Game.Controllers.Impls;
using PurpleSlayerFish.Game.Processors.Ai.States;
using PurpleSlayerFish.Game.Processors.Animation;
using PurpleSlayerFish.Game.Processors.Combat;
using PurpleSlayerFish.Game.Processors.Combat.Impls;
using PurpleSlayerFish.Game.Processors.Movement;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.Ai
{
    public abstract class AiBehaviour : AbstractBehaviour, ICombat
    {
        [Inject] protected GameConfig _gameConfig;
        [Inject] private ObstacleController _obstacleController;
        
        [SerializeField] private AbstractState[] _aiStates;
        [SerializeField] private AiCombatProcessor _combatProcessor;
        public AiNavAgentMovementProcessor MovementProcessor;
        public AnimationProcessor AnimationProcessor;
        public bool BlockAi { get; set; }

        private AbstractState _currentState;
        private AbstractCombatProcessor _outAttackTarget;
        private AbstractCombatProcessor _playerTarget;
        
        public AbstractState CurrentState => _currentState;
        public AbstractCombatProcessor CombatProcessor => _combatProcessor;
        public abstract float IntersectionOffset { get; }

        public void Initialize(AbstractCombatProcessor target)
        {
            _playerTarget = target;
            Initialize();
        }

        public override void Initialize()
        {
            MovementProcessor.Agent.enabled = true;
            _combatProcessor.Target = _playerTarget;
            _combatProcessor.AnimationProcessor = AnimationProcessor;
            _combatProcessor.IntersectionOffset = IntersectionOffset;
            _combatProcessor.Initialize();
            _currentState = _aiStates[0];
            _currentState.Launch(this);
            BlockAi = false;
        }

        private void Update() => TryTargetFollow();

        private void TryTargetFollow()
        {
            if (BlockAi)
                return;
            if (CurrentState.Name != nameof(TargetFollowState))
                return;
            if (MovementProcessor.ReachTarget())
                NextState<AnalysisState>();
            else if (_obstacleController.CheckIntersections(transform.position, out _outAttackTarget, _gameConfig.ObstaclesOffset + IntersectionOffset))
            {
                _combatProcessor.Target = _outAttackTarget;
                NextState<AnalysisState>();
            }
        }

        public void NextState<T>() where T : AbstractState => NextState(typeof(T).Name);
        
        public void NextState(string stateName)
        {
            if (BlockAi)
                return;
            
            _currentState.Finish(this);
            _currentState = null;
            for (int i = 0; i < _aiStates.Length; i++)
                if (_aiStates[i].Name == stateName)
                {
                    _currentState = _aiStates[i];
                    break;
                }
            if (_currentState != null)
                _currentState.Launch(this);
            else
                throw new ArgumentException("There isn't type '" + stateName + "' in AiProcessor.AiStates of given object.");
        }

        public void TargetToPlayer() => CombatProcessor.Target = _playerTarget;
    }
}