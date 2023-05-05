using PurpleSlayerFish.Core.Services;

namespace PurpleSlayerFish.Game.Processors.Combat.Impls
{
    public class AiCombatProcessor : AbstractCombatProcessor
    {
        private VectorUtils _vectorUtils;
        private bool _isTargetHitted;
        
        public override float IntersectionOffset { get; set; }

        public override async void Attack()
        {
            if(Target == null)
                return;
            if(_inAttack)
                return;
            _isTargetHitted = false;
            _inAttack = true;
            AnimationProcessor.ActionState(1);
            await AttackDelay();
            await ActiveState(OverlapFirst);
            await AttackRemaining();
            AnimationProcessor.ActionState(0);
            base.Attack();
        }

        public void OverlapFirst()
        {
            if (_isTargetHitted)
                return;
            if (_vectorUtils.InDistance(Target.transform.position - transform.position,
                IntersectionOffset + Target.IntersectionOffset))
            {
                _isTargetHitted = true;
                Target.Hit(_referenceAttackObject.Damage);
            }
        }
    }
}