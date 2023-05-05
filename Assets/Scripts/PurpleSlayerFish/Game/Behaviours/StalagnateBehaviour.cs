using System;
using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Game.Controllers;
using PurpleSlayerFish.Game.Controllers.Impls;
using PurpleSlayerFish.Game.Processors.Combat;
using PurpleSlayerFish.Game.Processors.Combat.Impls;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Behaviours
{
    public class StalagnateBehaviour : AbstractBehaviour, ICombat
    {
        [Inject] private StalagnateController _stalagnateController;
        
        [SerializeField] private AbstractCombatProcessor _combatProcessor;
        public AbstractCombatProcessor CombatProcessor => _combatProcessor;
        
        public void Awake() => Initialize();

        public override void Initialize()
        {
            _stalagnateController.Add(this);
            CombatProcessor.Initialize();
            CombatProcessor.OnDeath = () => _stalagnateController.Handle(this);
        }
    }
}