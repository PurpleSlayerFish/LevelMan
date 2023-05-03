using UnityEngine;

namespace PurpleSlayerFish.Game.Behaviours.Movement
{
    public abstract class MovementProcessor : MonoBehaviour
    {
        public abstract void Move(Vector3 direction);

        public abstract void Warp(Vector3 position);
    }
}