using System;
using PurpleSlayerFish.Core.Services.Tooltips;
using PurpleSlayerFish.Game.Controllers;
using PurpleSlayerFish.Game.Processors.Combat;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.InteractionProcessor
{
    public abstract class AInteraction : MonoBehaviour, IHasWorldTooltip
    {
        // public virtual bool AllowIfBusy => false;

        public abstract Transform TooltipFirstPivot { get; }
        public abstract Transform TooltipSecondPivot { get; }
        public abstract void Interact(PlayerInteractor interactor);
    }
}