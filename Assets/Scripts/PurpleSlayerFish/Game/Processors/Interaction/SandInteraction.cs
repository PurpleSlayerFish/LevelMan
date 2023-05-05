using PurpleSlayerFish.Game.Behaviours;
using PurpleSlayerFish.Game.Controllers.Impls;
using PurpleSlayerFish.Game.Processors.Interaction.Interactors;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.Interaction
{
    public class SandInteraction : AInteraction
    {
        [Inject] private InteractionController _interactionController;
        [SerializeField] private InteractionBehavior _behavior;
        [SerializeField] private string _objectKey;
        [SerializeField] private Transform _tooltipFirstPivot;
        [SerializeField] private Transform _tooltipSecondPivot;
        public bool IsDrop;
        
        public override Transform TooltipFirstPivot => _tooltipFirstPivot;
        public override Transform TooltipSecondPivot => _tooltipSecondPivot;

        private void Awake()
        {
            if (IsDrop)
                return;
            
            _interactionController.Interactions.Add(this);
        }

        public override void Interact(PlayerInteractor interactor)
        {
            interactor.Give(_objectKey);
            if (IsDrop)
                _interactionController.Kill(_behavior, _objectKey);
            else
            {
                _interactionController.Interactions.Remove(this);
                Destroy(gameObject);
            }
        }
    }
}