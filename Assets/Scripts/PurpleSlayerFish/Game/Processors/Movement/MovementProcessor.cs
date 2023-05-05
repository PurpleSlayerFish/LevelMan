using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Movement
{
    public abstract class MovementProcessor : MonoBehaviour
    {
        public abstract void Move(Vector3 direction);

        public abstract void Warp(Vector3 position);
    }
}