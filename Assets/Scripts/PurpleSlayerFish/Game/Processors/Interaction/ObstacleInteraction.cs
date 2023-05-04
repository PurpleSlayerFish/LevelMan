using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using PurpleSlayerFish.Game.Behaviours;
using PurpleSlayerFish.Game.Controllers;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.InteractionProcessor
{
    public class ObstacleInteraction : AInteraction
    {
        [Inject] private ObstacleController _obstacleController;
        [SerializeField] private ObstacleBehaviour _obstacleBehaviour;
        [SerializeField] private string _objectKey;
        [SerializeField] private Transform _tooltipFirstPivot;
        [SerializeField] private Transform _tooltipSecondPivot;
        
        public override Transform TooltipFirstPivot => _tooltipFirstPivot;
        public override Transform TooltipSecondPivot => _tooltipSecondPivot;

        public override void Interact(PlayerInteractor interactor)
        {
            interactor.Give(_objectKey);
            _obstacleController.Kill(_obstacleBehaviour, true);
        }
    }
}