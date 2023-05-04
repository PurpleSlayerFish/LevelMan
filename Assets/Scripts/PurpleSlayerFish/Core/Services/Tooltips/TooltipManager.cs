using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using PurpleSlayerFish.Game.Utils;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Core.Services.Tooltips
{
    public class TooltipManager
    {
        [Inject] private IPoolProvider<FloatingCanvasPoolData> _tooltipPool;

        private Transform _interactionPivot;
        private AbstractBehaviour _interactionTooltip;
        private Transform _secondActionPivot;
        private AbstractBehaviour _secondActionTooltip;
        
        public void InteractionTooltip(Transform transform = null) => 
            Tooltip(ref transform, ref _interactionPivot, ref _interactionTooltip, "InteractionTooltip");
        
        public void SecondActionTooltip(Transform transform = null) => 
            Tooltip(ref transform, ref _secondActionPivot, ref _secondActionTooltip, "SecondActionTooltip");

        private void Tooltip(ref Transform incoming, ref Transform pivot, ref AbstractBehaviour behaviour, string key)
        {
            if (incoming == null)
            {
                if (behaviour == null)
                    return;
                _tooltipPool.Release(key, behaviour);
                behaviour = null;
                pivot = null;
                return;
            }

            if (behaviour == null)
            {
                behaviour = _tooltipPool.Get(key);
                pivot = incoming;
            }
            else if (incoming != pivot)
                pivot = incoming;

            behaviour.transform.position = incoming.position;
        }
    }
}