﻿using System;
using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Core.Services.ScriptableObjects.GameConfig;
using PurpleSlayerFish.Game.Behaviours.Movement;
using PurpleSlayerFish.Game.Processors.Combat;
using PurpleSlayerFish.Game.Processors.Combat.Impls;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace PurpleSlayerFish.Game.Behaviours
{
    public class ObstacleBehaviour : AbstractBehaviour, ICombat
    {
        [Inject] private GameConfig _gameConfig;
        
        [SerializeField] private AbstractCombatProcessor _combatProcessor;
        public AbstractCombatProcessor CombatProcessor => _combatProcessor;
        
        public MovementProcessor MovementProcessor;
        public Transform Pivot0;
        public Transform Pivot1;
        public Transform Link0;
        public Transform Link1;
        
        public override void Initialize()
        {
            CombatProcessor.Initialize();
            CombatProcessor.IntersectionOffset = _gameConfig.ObstaclesOffset;
        }
    }
}