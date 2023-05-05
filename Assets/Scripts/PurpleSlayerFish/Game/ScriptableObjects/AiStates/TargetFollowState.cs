using PurpleSlayerFish.Game.Processors.Combat.Impls;
using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Ai.States
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AI/TargetFollowState", fileName = "TargetFollowState")]
    public class TargetFollowState : AbstractState
    {
        [SerializeField] private float _pathRebuildOffset = 1f;
        
        public override void Launch(AiBehaviour behaviour)
        {
            behaviour.MovementProcessor.Agent.enabled = false;
            behaviour.MovementProcessor.Agent.enabled = true;
            behaviour.AnimationProcessor.WalkingState(1);
            behaviour.MovementProcessor.Target = (behaviour.CombatProcessor as AiCombatProcessor).Target.transform;
            behaviour.MovementProcessor.FollowThreshold = _pathRebuildOffset;
            behaviour.MovementProcessor.Move();
        }

        public override void Finish(AiBehaviour behaviour)
        {
            behaviour.AnimationProcessor.WalkingState(0);
            behaviour.MovementProcessor.Target = null;
            behaviour.MovementProcessor.FollowThreshold = 0;
        }
    }
}