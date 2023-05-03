using UnityEngine;

namespace PurpleSlayerFish.Core.Services.ScriptableObjects.GameConfig
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameConfig", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject//, IGameConfig
    {
        [Header("Player Options")]
        [SerializeField] private float _playerAttackOffset = 0.4f;
        public float PlayerAttackOffset => _playerAttackOffset;
        
        [SerializeField] private float _playerHitDuration = 0.4f;
        public float PlayerHitDuration => _playerHitDuration;
        
        [Header("Player Animations")]
        [SerializeField] private int _playerIdleAnimation = 0;
        public int PlayerIdleAnimation => _playerIdleAnimation;
        [SerializeField] private int _playerWalkingAnimation = 1;
        public int PlayerWalkingAnimation => _playerWalkingAnimation;
        [SerializeField] private int _playerJumpAnimation = 2;
        public int PlayerJumpAnimation => _playerJumpAnimation;
        [SerializeField] private int _playerSillyAnimation = 20;
        public int PlayerSillyAnimation => _playerSillyAnimation;
        
        [SerializeField] private int _playerAttackAnimation = 10;
        public int PlayerAttackAnimation => _playerAttackAnimation;
        [SerializeField] private int _playerTakeAnimation = 20;
        public int PlayerTakeAnimation => _playerTakeAnimation;
        [SerializeField] private int _playerHitAnimation = 30;
        public int PlayerHitAnimation => _playerHitAnimation;
        
        
        [Header("Stalagnates Options")]
        [SerializeField] private float _option1 = 0.55f;
        public float Option1 => _option1;
        [SerializeField] private float _stalagnateOffset = 0.75f;
        public float StalagnateOffset => _stalagnateOffset;

        [Header("Obstacles Options")]
        [SerializeField] private float _obstaclesOffset = 0.75f;
        public float ObstaclesOffset => _obstaclesOffset;
        [SerializeField] private float _obstaclesWidth = 4f;
        public float ObstaclesWidth => _obstaclesWidth;
        [SerializeField] private float _obstacleDespawnDuration = 0.7f;
        public float ObstacleDespawnDuration => _obstacleDespawnDuration;

        [Header("Rats Options")]
        [SerializeField] private float _ratDespawnTime = 3f;
        public float RatDespawnTime => _ratDespawnTime;
        [SerializeField] private float _ratDespawnDuration = 0.7f;
        public float RatDespawnDuration => _ratDespawnDuration;
        [SerializeField] private float _ratOffset = 0.28f;
        public float RatOffset => _ratOffset;
    }
}