using DG.Tweening;
using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using PurpleSlayerFish.Core.Services.ScriptableObjects.GameConfig;
using PurpleSlayerFish.Game.Behaviours;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace PurpleSlayerFish.Game.Controllers
{
    public class ObstacleController : IntersectionController<ObstacleBehaviour>
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private IPoolProvider<BehaviourPoolData> _poolProvider;
        [Inject] private InteractionController _interactionController;

        private float _navMeshOffset = NavMesh.GetSettingsByID(0).voxelSize;

        public ObstacleBehaviour CheckNearestObstacle(Vector3 position)
        {
            for (int i = 0; i < _intersectors.Count; i++)
                if (_vectorUtils.IsPointInsideBox(position, _intersectors[i].Pivot0.position,
                    _intersectors[i].Pivot1.position))
                    return _intersectors[i];
            return null;
        }
        
        public void SpawnObstacle(Vector3 dropPosition)
        {
            var obstacle = _poolProvider.Get<ObstacleBehaviour>();
            _intersectors.Add(obstacle);
            _interactionController.Interactions.Add(obstacle.ObstacleInteraction);
            Drop(dropPosition, out Vector3 position, out float rotation);
            obstacle.MovementProcessor.Warp(position);
            obstacle.transform.eulerAngles = Vector3.up * rotation;
            obstacle.Initialize();
            obstacle.CombatProcessor.OnDeath += () => Kill(obstacle);
        }
        
        public void Kill(ObstacleBehaviour obstacle, bool instantly = false)
        {
            if (instantly)
            {
                _interactionController.Interactions.Remove(obstacle.ObstacleInteraction);
                _intersectors.Remove(obstacle);
                _poolProvider.Release(obstacle);
            }
            else
                obstacle.transform.DOMoveY(-1.5f, _gameConfig.ObstacleDespawnDuration)
                    .SetRelative()
                    .SetDelay(_gameConfig.RatDespawnTime)
                    .OnComplete(() =>
                    {
                        _intersectors.Remove(obstacle);
                        _poolProvider.Release(obstacle);
                    });
        }
        
        private void Drop(Vector3 origin, out Vector3 position, out float rotation)
        {
            position = Vector3.zero;
            rotation = 0;
            if (NavMesh.SamplePosition(origin, out var hit, 1.0f, NavMesh.AllAreas))
            {
                NavMesh.FindClosestEdge(hit.position, out hit, NavMesh.AllAreas);
                position = hit.position + hit.normal * _gameConfig.ObstaclesWidth / 2;
                rotation = Vector3.SignedAngle(position - hit.position, Vector3.right, Vector3.up);
                position -= Vector3.up * _navMeshOffset;
            }
        }
    }
}