using System.Threading.Tasks;
using PurpleSlayerFish.Core.Services.ScriptableObjects;
using PurpleSlayerFish.Core.Ui.Container;
using PurpleSlayerFish.Core.Ui.Windows.GameWindow;
using PurpleSlayerFish.Core.Ui.Windows.PauseWindow;
using PurpleSlayerFish.Game.Controllers.Impls;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.Combat.Impls
{
    public class PlayerCombatProcessor : AbstractCombatProcessor
    { 
        [Inject] private GameConfig _gameConfig;
        [Inject] private SignalBus _signalBus;
        [Inject] private IUiContainer _uiContainer;
        [Inject] private RatController _ratController;
        [Inject] private StalagnateController _stalagnateController;
        
        public Transform AttackPivot;
        private AbstractCombatProcessor _outOverlaped;

        public override float IntersectionOffset
        {
            get => _gameConfig.PlayerAttackOffset;
            set { }
        }

        public override void Initialize()
        {
            base.Initialize();
            OnHit += OnAnimateHit;
            OnHit += _ => _signalBus.Fire(new HealthSubscription{HealthPercent = HealthPercent});
        }

        public override async void Attack()
        {
            if(_inAttack)
                return;
            _inAttack = true;
            _outOverlaped = null;
            AnimationProcessor.ActionState(_gameConfig.PlayerAttackAnimation);
            await AttackDelay();
            await ActiveState(OverlapFirst);
            await AttackRemaining();
            AnimationProcessor.ActionState(_gameConfig.PlayerIdleAnimation);
            base.Attack();
        }

        public void OverlapFirst()
        {
            if (_outOverlaped != null)
                return;
            CheckIntersections();
            if (_outOverlaped != null)
                _outOverlaped.Hit(_referenceAttackObject.Damage);
        }
        
        private void CheckIntersections()
        {
            if (_ratController.CheckIntersections(AttackPivot.position, out _outOverlaped, IntersectionOffset + _gameConfig.RatOffset))
                return;
            if (_stalagnateController.CheckIntersections(AttackPivot.position, out _outOverlaped, IntersectionOffset + _gameConfig.StalagnateOffset))
                return;
        }

        private async void OnAnimateHit(int value)
        {
            if (AnimationProcessor.CurrentActionState != _gameConfig.PlayerIdleAnimation)
                return;
            AnimationProcessor.ActionState(_gameConfig.PlayerHitAnimation);
            await Task.Delay(Mathf.RoundToInt(_gameConfig.PlayerHitDuration * 1000));
            AnimationProcessor.ActionState(_gameConfig.PlayerIdleAnimation);
        }
        
        protected override void Death()
        {
            base.Death();
            _uiContainer.Show<LoseController>();
        }
    }
}