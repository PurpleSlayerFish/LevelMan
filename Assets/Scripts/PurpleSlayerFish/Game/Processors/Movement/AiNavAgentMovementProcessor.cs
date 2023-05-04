using System;
using DG.Tweening;
using PurpleSlayerFish.Core.Services;
using PurpleSlayerFish.Game.Controllers;
using PurpleSlayerFish.Game.Processors.Ai;
using PurpleSlayerFish.Game.Processors.Ai.States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Zenject;

namespace PurpleSlayerFish.Game.Behaviours.Movement
{
    public class AiNavAgentMovementProcessor : MovementProcessor
    { 
        [SerializeField] private float _rotationDuration = 0.2f;
        public NavMeshAgent Agent;

        private VectorUtils _vectorUtils = new ();
        private Vector3 _direction;
        private Vector3 _prevDirection;
        private Quaternion _nextRotation;
        private Tweener _tempTweener;
        private bool _pathPending;
        
        public Transform Target { get; set; }
        public float FollowThreshold { get; set; }

        private void Update()
        {
            FollowTarget();
            Rotate();
        }

        public override void Move(Vector3 direction)
        {
            Agent.SetDestination(Target.position);
            _pathPending = true;
        }

        public void Move() => Move(Target.position);
        public override void Warp(Vector3 position) => Agent.Move(position);

        private void FollowTarget()
        {
            if (Target == null)
                return;
            
            if (!_pathPending && !_vectorUtils.InDistance(Agent.destination -  Target.position, FollowThreshold))
                Move();
            else if (_pathPending && Agent.remainingDistance <= Agent.stoppingDistance)
                _pathPending = false;
        }

        public bool ReachTarget() => Target == null || _vectorUtils.InDistance(Agent.transform.position - Agent.destination, Agent.stoppingDistance);

        public void Rotate()
        {
            if (Agent.hasPath && Agent.path.corners.Length > 0)
                _direction = Agent.path.corners[Mathf.Min(1, Agent.path.corners.Length - 1)] - transform.position;
            else if (Target != null)
                _direction = Target.position - transform.position;

            if (_prevDirection != _direction)
            {
                _tempTweener.Kill();
                _tempTweener = Agent.transform.DORotateQuaternion(Quaternion.LookRotation(_direction), _rotationDuration);
                _prevDirection = _direction;
            }
        }
    }
}