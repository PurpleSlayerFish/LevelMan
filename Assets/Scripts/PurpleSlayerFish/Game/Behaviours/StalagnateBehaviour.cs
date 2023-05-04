using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Game.Controllers;
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
        
        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}