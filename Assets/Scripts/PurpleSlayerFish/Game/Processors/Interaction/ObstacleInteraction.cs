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
        public bool IsDrop;
        
        public override Transform TooltipFirstPivot => _tooltipFirstPivot;
        public override Transform TooltipSecondPivot => _tooltipSecondPivot;

        public void Awake()
        {
            if (IsDrop)
                return;
            
            _interactionController.Interactions.Add(this);
            _obstacleController.Interactions.Add(_obstacleBehaviour);
            _obstacleBehaviour.Initialize();
            _obstacleBehaviour.CombatProcessor.OnDeath += () => _obstacleController.Kill(_obstacleBehaviour);
        }

        public override void Interact(PlayerInteractor interactor)
        {
            interactor.Give(_objectKey);
            if (IsDrop)
                _obstacleController.Kill(_obstacleBehaviour, true);
            else
            {
                _interactionController.Interactions.Remove(this);
                _obstacleController.Interactions.Remove(_obstacleBehaviour);
                Destroy(gameObject);
            }
        }
    }
}