using PurpleSlayerFish.Game.Behaviours;
using PurpleSlayerFish.Game.Controllers.Impls;
using PurpleSlayerFish.Game.Processors.Interaction.Interactors;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.Interaction
{
    public class ObstacleInteraction : AInteraction
    {
        [Inject] private ObstacleController _obstacleController;
        [Inject] private InteractionController _interactionController;
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