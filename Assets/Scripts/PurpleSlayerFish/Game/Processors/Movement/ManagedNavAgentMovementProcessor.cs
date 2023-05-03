using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using PurpleSlayerFish.Core.Services;
using UnityEngine;
using UnityEngine.AI;

namespace PurpleSlayerFish.Game.Behaviours.Movement
{
    public class ManagedNavAgentMovementProcessor : MovementProcessor
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _rotationDuration = 0.2f;

        private VectorUtils _vectorUtils;
        private Vector3 _prevDirection;
        private Tweener _tempTweener;

        public PlayerBehaviour playerBehaviour;

        public override void Move(Vector3 direction)
        {
            if (direction == Vector3.up)
                return;
            Rotate(direction);
            // if (CrossObstacle(direction*5))
            //     return;
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

        // private bool CrossObstacle(Vector3 direction) =>
        //     playerBehaviour.NearestObstacle != null
        //     && _vectorUtils.IsPointInsideBox(transform.position + NextPosition(direction)
        //         , playerBehaviour.NearestObstacle.Stop0.position, playerBehaviour.NearestObstacle.Stop1.position);
    }
}
