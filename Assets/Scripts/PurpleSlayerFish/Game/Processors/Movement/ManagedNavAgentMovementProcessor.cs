using DG.Tweening;
using PurpleSlayerFish.Core.Services;
using PurpleSlayerFish.Game.Behaviours;
using UnityEngine;
using UnityEngine.AI;

namespace PurpleSlayerFish.Game.Processors.Movement
{
    public class ManagedNavAgentMovementProcessor : MovementProcessor
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _rotationDuration = 0.2f;

        private VectorUtils _vectorUtils;
        private Vector3 _prevDirection;
        private Tweener _tempTweener;
        
        public NavMeshAgent Agent => _agent; 

        public override void Move(Vector3 direction)
        {
            if (direction == Vector3.up)
                return;
            Rotate(direction);
            _agent.Move(NextPosition(direction));
        }

        private Vector3 NextPosition(Vector3 direction) => direction * _agent.speed * Time.deltaTime;
        public override void Warp(Vector3 position) => _agent.Move(position);

        private void Rotate(Vector3 direction)
        {
            if (_prevDirection != direction)
            {
                _tempTweener.Kill();
                _tempTweener =
                    _agent.transform.DORotateQuaternion(Quaternion.LookRotation(direction), _rotationDuration);
                _prevDirection = direction;
            }
        }
        
        public void JumpOverObstacle(ObstacleBehaviour obstacle)
        {
            _agent.enabled = false;
            
            if ((obstacle.Link0.position - transform.position).sqrMagnitude > (obstacle.Link1.position - transform.position).sqrMagnitude)
                transform.position = obstacle.Link0.position;
            else
                transform.position = obstacle.Link1.position;
            
            _agent.enabled = true;
        }
    }
}
