using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Animation
{
    public class AnimationProcessor : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private readonly int _walkingState = Animator.StringToHash("WALKING_STATE");
        private readonly int _actionState = Animator.StringToHash("ACTION_STATE");
        private readonly int _deadState = Animator.StringToHash("DEAD_STATE");
        private int _currentWalkingState;
        private int _currentActionState;
        private bool _isDead;

        public int CurrentWalkingState => _currentWalkingState;
        public int CurrentActionState => _currentActionState;
        
        public void WalkingState(int value)
        {
            if (_currentWalkingState == value)
                return;
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