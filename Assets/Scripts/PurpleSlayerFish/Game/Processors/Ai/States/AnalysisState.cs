using DG.Tweening;
using PurpleSlayerFish.Core.Services;
using PurpleSlayerFish.Game.Processors.Ai.States;
using PurpleSlayerFish.Game.Processors.Combat;
using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Ai
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AI/AnalysisState", fileName = "AnalysisState")]
    public class AnalysisState : AbstractState
    {
        [SerializeField] private float _duration = 0.2f;
        
        private VectorUtils _vectorUtils = new ();
        private Vector3 _deltaPosition;
        private string _nextState;
        
        public override void Launch(AiBehaviour behaviour)
        {
            DOVirtual.DelayedCall(_duration, Analysis);

            void Analysis()
            {
                if (behaviour.BlockAi)
                    return;
                
                if (behaviour.CombatProcessor.Target.IsDead)
                    behaviour.TargetToPlayer();
                        
                _deltaPosition = behaviour.CombatProcessor.Target.transform.position - behaviour.transform.position;
                // _nextState = _vectorUtils.InDistance(_deltaPosition, behaviour.MovementProcessor.Agent.stoppingDistance + 0.1f) 
                
                _nextState = _vectorUtils.InDistance(_deltaPosition, behaviour.CombatProcessor.Target.IntersectionOffset 
                                                                     + behaviour.CombatProcessor.IntersectionOffset) 
                    ? nameof(AttackState) 
                    : nameof(TargetFollowState);
                behaviour.NextState(_nextState);
            }
        }
    }
}