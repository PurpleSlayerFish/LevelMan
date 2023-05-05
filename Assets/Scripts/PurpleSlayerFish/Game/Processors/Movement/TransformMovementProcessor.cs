using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Movement
{
    public class TransformMovementProcessor : MovementProcessor
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _speed = 40f;
        
        public override void Move(Vector3 direction) => _transform.position += direction * Time.deltaTime * _speed;
        public override void Warp(Vector3 position) =>_transform.position = position;
    }
}