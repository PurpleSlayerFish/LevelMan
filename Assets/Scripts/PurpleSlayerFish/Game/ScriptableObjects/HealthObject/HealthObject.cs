using System;
using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Combat.HealthObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Combat/HealthObject", fileName = "HealthObject")]
    public class HealthObject : ScriptableObject
    {
        [SerializeField] private HealthData _healthData;
        public HealthData HealthData => _healthData;
    }

    [Serializable]
    public struct HealthData
    {
        public int MaxHealth;
        public int CurrentHealth;
    }
}