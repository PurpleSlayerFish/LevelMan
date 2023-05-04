using PurpleSlayerFish.Game.Controllers;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.InteractionProcessor
{
    public class PotInteraction : AInteraction
    {
        [Inject] private InteractionController _interactionController;
        [SerializeField] private string _objectKey;
        [SerializeField] private Transform _tooltipFirstPivot;
        [SerializeField] private Transform _tooltipSecondPivot;
        
        public override Transform TooltipFirstPivot => _tooltipFirstPivot;
        public override Transform TooltipSecondPivot => _tooltipSecondPivot;

        private void Awake()
        {
            _interactionController.AddPot(this);
        }

        public override void Interact(PlayerInteractor interactor)
        {
            interactor.Give(_objectKey);
            _interactionController.HidePot();
        }
    }
}