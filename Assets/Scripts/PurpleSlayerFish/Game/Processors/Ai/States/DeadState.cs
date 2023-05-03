using PurpleSlayerFish.Game.Processors.Ai.States;
using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Ai
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AI/DeadState", fileName = "DeadState")]
    public class DeadState : AbstractState
    {
        public override void Launch(AiBehaviour behaviour)
        {
            behaviour.BlockAi = true;
            behaviour.CombatProcessor.StopAttack = true;
        }
    }
}