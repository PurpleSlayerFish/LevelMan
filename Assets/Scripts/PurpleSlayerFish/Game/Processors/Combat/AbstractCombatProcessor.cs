using System;
using System.Threading.Tasks;
using PurpleSlayerFish.Game.Processors.Animation;
using PurpleSlayerFish.Game.Processors.Combat.HealthObject;
using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Combat.Impls
{
    public abstract class AbstractCombatProcessor : MonoBehaviour
    {
        [SerializeField] protected AttackObject.AttackObject _referenceAttackObject;
        [SerializeField] protected HealthObject.HealthObject _referenceHealthObject;
        protected bool _inAttack;
        protected float _attackTimer;
        protected HealthData _healthData;

        public Action OnAfterAttack;
        public Action<int> OnHit;
        public Action OnDeath;
        public bool StopAttack;
        public bool IsDead;
        
        public float HealthPercent => (float)_healthData.CurrentHealth / _healthData.MaxHealth;
        public abstract float IntersectionOffset { get; set; }
        public AnimationProcessor AnimationProcessor { get; set; }
        public AbstractCombatProcessor Target { get; set; }
        
        public virtual void Initialize()
        {
            _healthData = _referenceHealthObject.HealthData;
            IsDead = false;
            AnimationProcessor?.SetDead(false);
            OnDeath = null;
        }

        public virtual void Attack()
        {
            if (StopAttack || IsDead)
                return;
            _inAttack = false;
            OnAfterAttack?.Invoke();
        }

        protected async Task AttackDelay()
        {
            if (StopAttack || IsDead)
                return;
            await Task.Delay(Mathf.RoundToInt(_referenceAttackObject.ActiveDelay * 1000));
        }

        protected async Task ActiveState(Action action)
        {
            if (StopAttack || IsDead)
                return;
            
            _attackTimer = 0f;
            while (_attackTimer < _referenceAttackObject.ActiveDuration)
            {
                if (StopAttack || IsDead)
                    return;
                action.Invoke();
                _attackTimer += Time.deltaTime;
                await Task.Yield();
            }
        }
        protected async Task AttackRemaining()
        {
            if (StopAttack || IsDead)
                return;
            await Task.Delay(Mathf.FloorToInt((_referenceAttackObject.SummaryDuration - _referenceAttackObject.ActiveDuration - _referenceAttackObject.ActiveDelay) * 1000f));
        }
            
        public void Hit(int value)
        {
            ChangeHealth(value);
            OnHit?.Invoke(value);
        }

        protected virtual void Death()
        {
            IsDead = true;
            AnimationProcessor?.SetDead(true);
        }
        
        public void ChangeHealth(int value)
        {
            if (_healthData.CurrentHealth == 0)
                return;
            _healthData.CurrentHealth = Mathf.Clamp(_healthData.CurrentHealth - value, 0, _healthData.MaxHealth);
            if (_healthData.CurrentHealth == 0)
            {
                Death();
                OnDeath?.Invoke();
            }
        }
    }
}