using System.Collections.Generic;
using DG.Tweening;
using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Services.DataStorage;
using PurpleSlayerFish.Core.Services.Pause;
using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using PurpleSlayerFish.Core.Services.ScenePoints;
using PurpleSlayerFish.Core.Services.ScriptableObjects;
using PurpleSlayerFish.Game.Behaviours;
using PurpleSlayerFish.Game.Processors.Ai.States;
using PurpleSlayerFish.Game.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Controllers.Impls
{
    public class RatController : IntersectionController<RatBehaviour>, ITickable
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private ScenePoints _scenePoints;
        [Inject] private RatSpawnConfig _spawnConfig;
        [Inject] private IPoolProvider<BehaviourPoolData> _poolProvider;
        [Inject] private IPauseService _pauseService;
        
        [Inject] private IDataStorage<PlayerData> _dataStorage;

        private PlayerBehaviour _player;
        private List<Transform> _spawnPoints;
        private List<float> _spawnPointsTimer;
        
        private const string SPAWN_POINT_PREFIX = "RAT_SPAWNER_POINT_";

        public void Initialize(PlayerBehaviour player)
        {
            _player = player;
            SetSpawnPoints();

            if (_dataStorage.CurrentData.Bricks < 2)
            {
                TrySpawnRat(_scenePoints.Points.Get("RAT_TUTORIAL_1").position);
                TrySpawnRat(_scenePoints.Points.Get("RAT_TUTORIAL_2").position);
            }
        }

        public void TrySpawnRat(Vector3 position)
        {
            if (_intersectors.Count > _spawnConfig.RatSpawnInfo[_dataStorage.CurrentData.Bricks].MaxRatsCount)
                return;
            var rat = _poolProvider.Get<RatBehaviour>();
            _intersectors.Add(rat);
            rat.MovementProcessor.Agent.Warp(position);
            rat.Initialize(_player.CombatProcessor);
            rat.CombatProcessor.OnDeath += () => KillRat(rat);
        }

        public void KillRat(RatBehaviour rat)
        {
            rat.NextState<DeadState>();
            rat.transform.DOMoveY(-1, _gameConfig.RatDespawnDuration)
                .SetRelative()
                .SetDelay(_gameConfig.RatDespawnTime)
                .OnComplete(() =>
                {
                    _intersectors.Remove(rat);
                    _poolProvider.Release(rat);
                });
        }

        public void Tick()
        {
            if (_pauseService.IsPaused)
                return;
            if (_spawnConfig.RatSpawnInfo[_dataStorage.CurrentData.Bricks].RatsPerPoint < 1)
                return;

            for (int i = 0; i < _spawnPointsTimer.Count; i++)
            {
                _spawnPointsTimer[i] += Time.deltaTime;
                if (_spawnPointsTimer[i] >= _spawnConfig.RatSpawnInfo[_dataStorage.CurrentData.Bricks].SpawnCooldown 
                    && !_vectorUtils.InDistance(_player.transform.position - _spawnPoints[i].position, _gameConfig.RatSpawnToPlayerOffset))
                {
                    for (int j = 0; j < _spawnConfig.RatSpawnInfo[_dataStorage.CurrentData.Bricks].RatsPerPoint; j++)
                        TrySpawnRat(_spawnPoints[i].position);
                    _spawnPointsTimer[i] = 0;
                }
            }
        }


        public void SetSpawnPoints()
        {
            _spawnPoints = new();
            foreach (KeyValuePair<string, Transform> kvp in _scenePoints.Points.Dictionary)
                if (kvp.Key.StartsWith(SPAWN_POINT_PREFIX))
                    _spawnPoints.Add(kvp.Value);
            _spawnPointsTimer = new List<float>(_spawnPoints.Count);
            for (int i = 0; i < _spawnPointsTimer.Capacity; i++)
                _spawnPointsTimer.Add(0);
        }
    }
}