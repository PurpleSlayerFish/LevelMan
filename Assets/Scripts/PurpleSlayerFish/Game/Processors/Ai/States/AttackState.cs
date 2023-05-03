using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Ai.States
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AI/AttackState", fileName = "AttackState")]
    public class AttackState : AbstractState
    {
        public override void Launch(AiBehaviour behaviour)
        {
            behaviour.CombatProcessor.Attack();
            behaviour.CombatProcessor.OnAfterAttack = () => behaviour.NextState<AnalysisState>();
        }
    }
}