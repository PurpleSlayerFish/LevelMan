using UnityEngine;

namespace PurpleSlayerFish.Core.Services.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameConfig", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject//, IGameConfig
    {
        [Header("Player Options")]
        [SerializeField] private float _playerAttackOffset = 0.4f;
        public float PlayerAttackOffset => _playerAttackOffset;
        [SerializeField] private float _playerHitDuration = 0.4f;
        public float PlayerHitDuration => _playerHitDuration;
        [SerializeField] private float _playerInterationOffset = 0.6f;
        public float PlayerInterationOffset => _playerInterationOffset;
        [SerializeField] private float _playerNormalSpeed = 3.4f;
        public float PlayerNormalSpeed => _playerNormalSpeed;
        [SerializeField] private float _playerBusySpeed = 1.7f;
        public float PlayerBusySpeed => _playerBusySpeed;
        [SerializeField] private int _shroomsHeal = 5;
        public int ShroomsHeal => _shroomsHeal;
        
        
        [Header("Player Base Animations")]
        [SerializeField] private int _playerIdleAnimation = 0;
        public int PlayerIdleAnimation => _playerIdleAnimation;
        [SerializeField] private int _playerWalkingAnimation = 1;
        public int PlayerWalkingAnimation => _playerWalkingAnimation;
        [SerializeField] private int _playerJumpAnimation = 2;
        public int PlayerRunAnimation => _playerJumpAnimation;
        [SerializeField] private int _playerStirAnimation = 20;
        public int PlayerStirAnimation => _playerStirAnimation;
        [SerializeField] private float _playerStirAnimationDuration = 3f;
        public float PlayerStirAnimationDuration => _playerStirAnimationDuration;
        [SerializeField] private int _playerColumnAnimation = 21;
        public int PlayerColumnAnimation => _playerColumnAnimation;
        [SerializeField] private float _playerColumnDuration = 3f;
        public float PlayerColumnDuration => _playerColumnDuration;
        
        [Header("Player Action Animations")]
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
        [SerializeField] private float _ratDespawnTime = 3f;
        public float RatDespawnTime => _ratDespawnTime;

        [Header("Rats Options")]
        [SerializeField] private float _ratDespawnDuration = 0.7f;
        public float RatDespawnDuration => _ratDespawnDuration;
        [SerializeField] private float _ratOffset = 0.28f;
        public float RatOffset => _ratOffset;
        [SerializeField] private float _ratSpawnToPlayerOffset = 15f;
        public float RatSpawnToPlayerOffset => _ratSpawnToPlayerOffset;

        [Header("Column Options")]
        [SerializeField] private int _startBrickCount = 1;
        public int StartBrickCount => _startBrickCount;
        [SerializeField] private int _totalBrickCount = 7;
        public int TotalBrickCount => _totalBrickCount;
        [SerializeField] private float _brickHeight = 1f;
        public float BrickHeight => _brickHeight;
        
        
        [Header("Light Options")]
        [SerializeField] private float _playerStartLightLevel = 0.6f;
        public float PlayerStartLightLevel => _playerStartLightLevel;
        [SerializeField] private float _playerMinLightLevel = 0.6f;
        public float PlayerMinLightLevel => _playerMinLightLevel;
        [SerializeField] private float _playerMaxLightLevel = 2.5f;
        public float PlayerMaxLightLevel => _playerMaxLightLevel;
        [SerializeField] private float _lightBugDuration = 120f;
        public float LightBugDuration => _lightBugDuration;
        
        
        [Header("Sound Options")]
        [SerializeField] private float _randomPhrasesTimeout = 66f;
        public float RandomPhrasesTimeout => _randomPhrasesTimeout;
        [SerializeField] private float _randomCaveSoundTimeout = 46f;
        public float RandomCaveSoundTimeout => _randomCaveSoundTimeout;
    }
}