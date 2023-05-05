using PurpleSlayerFish.Core.Services.Tooltips;
using PurpleSlayerFish.Game.Processors.Interaction.Interactors;
using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Interaction
{
    public abstract class AInteraction : MonoBehaviour, IHasWorldTooltip
    {
        [SerializeField] protected float _offset = 0.5f;
        public float Offset => _offset * transform.lossyScale.magnitude;
        public virtual bool AllowInteraction(PlayerInteractor interactor) => !interactor.IsBusy;

        public abstract Transform TooltipFirstPivot { get; }
        public abstract Transform TooltipSecondPivot { get; }
        public abstract void Interact(PlayerInteractor interactor);
    }
}