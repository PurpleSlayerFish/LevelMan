using System.Collections.Generic;
using DG.Tweening;
using PurpleSlayerFish.Core.Services;
using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using PurpleSlayerFish.Core.Services.ScenePoints;
using PurpleSlayerFish.Core.Services.ScriptableObjects.GameConfig;
using PurpleSlayerFish.Game.Behaviours;
using PurpleSlayerFish.Game.Processors.Ai;
using PurpleSlayerFish.Game.Processors.Combat;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Controllers
{
    public class RatController : IntersectionController<RatBehaviour>
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private ScenePoints _scenePoints;
        [Inject] private IPoolProvider<BehaviourPoolData> _poolProvider;

        private PlayerBehaviour _player;

        public void Initialize(PlayerBehaviour player)
        {
            _player = player;
            // SpawnRat();
        }

        public void SpawnRat()
        {
            var rat = _poolProvider.Get<RatBehaviour>();
            _intersectors.Add(rat);
            rat.MovementProcessor.Agent.Warp(_scenePoints.Points.Get("DEFAULT_RAT_SPAWN").position);
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
    }
}