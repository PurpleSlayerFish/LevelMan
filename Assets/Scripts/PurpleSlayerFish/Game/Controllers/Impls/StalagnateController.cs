using System.Collections.Generic;
using PurpleSlayerFish.Core.Services;
using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using PurpleSlayerFish.Core.Services.ScenePoints;
using PurpleSlayerFish.Core.Services.ScriptableObjects.GameConfig;
using PurpleSlayerFish.Game.Behaviours;
using PurpleSlayerFish.Game.Processors.Combat;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace PurpleSlayerFish.Game.Controllers
{
    public class StalagnateController : IntersectionController<StalagnateBehaviour>
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private ObstacleController _obstacleController;
        [Inject] private IPoolProvider<BehaviourPoolData> _poolProvider;
        [Inject] private ScenePoints _scenePoints;

        public void SpawnStalagmate()
        {
            var stalagnate = _poolProvider.Get<StalagnateBehaviour>();
            _intersectors.Add(stalagnate);
            stalagnate.transform.position = _scenePoints.Points.Get("STALAGMATE_1").position;
            stalagnate.CombatProcessor.Initialize();
            stalagnate.CombatProcessor.OnDeath = () => Drop(stalagnate);
        }

        public void Drop(StalagnateBehaviour stalagnate)
        {
            _obstacleController.SpawnObstacle(stalagnate.transform.position);
            _intersectors.Remove(stalagnate);
            _poolProvider.Release(stalagnate);
        }
    }
}