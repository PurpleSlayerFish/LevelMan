using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Combat.AttackObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Combat/AttackObject", fileName = "AttackObject")]
    public class AttackObject : ScriptableObject
    {
        [Header("Damage options")]
        [SerializeField] private int _damage = 1;
        public int Damage => _damage;
        
        [Header("Attack options")]
        [SerializeField] private float _summaryDuration = 1f;
        public float SummaryDuration => _summaryDuration;
        [SerializeField] private float _activeDuration = 0.5f;
        public float ActiveDuration => _activeDuration;
        [SerializeField] private float _activeDelay = 0.25f;
        public float ActiveDelay => _activeDelay;
    }
}