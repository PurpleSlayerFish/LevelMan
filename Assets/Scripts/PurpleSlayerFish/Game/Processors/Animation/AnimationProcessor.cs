using PurpleSlayerFish.Core.Behaviours;
using UnityEngine;

namespace PurpleSlayerFish.Game.Behaviours.Animation
{
    public class AnimationProcessor : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private readonly int _walkingState = Animator.StringToHash("WALKING_STATE");
        private readonly int _actionState = Animator.StringToHash("ACTION_STATE");
        private readonly int _deadState = Animator.StringToHash("DEAD_STATE");
        private int _nextWalkingState;
        private int _currentWalkingState;
        private int _currentActionState;
        private bool _isDead;

        // public int CurrentWalkingState => _currentWalkingState;
        // public int CurrentActionState => _currentActionState;

        public void WalkingState(Vector3 direction)
        {
            _nextWalkingState = direction.x == 0 && direction.z == 0 ? 0 : 1;
            if (_currentWalkingState == _nextWalkingState)
                return;
            WalkingState(_nextWalkingState);
        }
        
        public void WalkingState(int value)
        {
            _currentWalkingState = value;
            _animator.SetInteger(_walkingState, _currentWalkingState);
        }

        public void ActionState(int value)
        {
            _currentActionState = value;
            _animator.SetInteger(_actionState, value);
            
            // Debug.Log(gameObject.name + ": " + value);
            // Debug.Log(StackTraceUtility.ExtractStackTrace());
        }

        public void SetDead(bool value) => _animator.SetBool(_deadState, value);
    }
}