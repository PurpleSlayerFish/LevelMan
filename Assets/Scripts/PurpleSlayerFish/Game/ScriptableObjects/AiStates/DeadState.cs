using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Ai.States
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AI/DeadState", fileName = "DeadState")]
    public class DeadState : AbstractState
    {
        public override void Launch(AiBehaviour behaviour)
        {
            behaviour.BlockAi = true;
            behaviour.CombatProcessor.StopAttack = true;
            behaviour.MovementProcessor.Agent.enabled = false;
        }
    }
}