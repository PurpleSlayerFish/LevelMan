using PurpleSlayerFish.Game.Behaviours;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Controllers.Impls
{
    public class StalagnateController : IntersectionController<StalagnateBehaviour>
    {
        [Inject] private ObstacleController _obstacleController;

        // public void SpawnStalagmate(string key)
        // {
        //     var stalagnate = _poolProvider.Get<StalagnateBehaviour>();
        //     _intersectors.Add(stalagnate);
        //     stalagnate.transform.position = _scenePoints.Points.Get(key).position;
        //     stalagnate.CombatProcessor.Initialize();
        // }

        // public void Drop(StalagnateBehaviour stalagnate)
        // {
        //     Handle(stalagnate);
        //     _poolProvider.Release(stalagnate);
        // }
        public void Add(StalagnateBehaviour stalagnate) => _intersectors.Add(stalagnate);
            
        public void Handle(StalagnateBehaviour stalagnate)
        {
            _obstacleController.SpawnObstacle(stalagnate.transform.position);
            _intersectors.Remove(stalagnate);
            Object.Destroy(stalagnate.gameObject);
        }
    }
}