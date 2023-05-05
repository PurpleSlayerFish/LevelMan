using System;
using UnityEngine;

namespace PurpleSlayerFish.Game.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Game/RatSpawnConfig", fileName = "RatSpawnConfig")]
    public class RatSpawnConfig : ScriptableObject
    {
        public RatSpawnInfo[] RatSpawnInfo;
    }

    [Serializable]
    public struct RatSpawnInfo
    {
        public int RatsPerPoint;
        public float SpawnCooldown;
        public int MaxRatsCount;
    }
}