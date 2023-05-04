using UnityEngine;

namespace PurpleSlayerFish.Core.Services.Tooltips
{
    public interface IHasWorldTooltip
    {
        Transform TooltipFirstPivot { get; }
        Transform TooltipSecondPivot { get; }
    }
}