using PurpleSlayerFish.Game.Behaviours;
using PurpleSlayerFish.Game.Controllers.Impls;
using PurpleSlayerFish.Game.Processors.Interaction.Interactors;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.Interaction
{
    public class LightBugInteraction : AInteraction
    {
        [Inject] private InteractionController _interactionController;
        [SerializeField] private string _objectKey;
        [SerializeField] private Transform _tooltipFirstPivot;
        [SerializeField] private Transform _tooltipSecondPivot;
        [SerializeField] private Light _light;
        
        public override Transform TooltipFirstPivot => _tooltipFirstPivot;
        public override Transform TooltipSecondPivot => _tooltipSecondPivot;
        public override bool AllowInteraction(PlayerInteractor interactor) => true;

        private void Awake()
        {
            _interactionController.Interactions.Add(this);
        }

        public override void Interact(PlayerInteractor interactor)
        {
            Color.RGBToHSV(_light.color, out float hsvH, out float hsvS, out float hsvV);
            interactor.Player.LightProcessor.MaximizeLight(hsvH);
            _interactionController.Interactions.Remove(this);
            Destroy(gameObject);
        }
    }
}