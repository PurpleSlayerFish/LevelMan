using System.Collections.Generic;
using PurpleSlayerFish.Core.Services;
using PurpleSlayerFish.Game.Processors.Combat;
using PurpleSlayerFish.Game.Processors.Combat.Impls;
using UnityEngine;

namespace PurpleSlayerFish.Game.Controllers
{
    public abstract class IntersectionController<T> where T : ICombat
    {
        protected List<T> _intersectors = new();
        protected VectorUtils _vectorUtils;
        public bool CheckIntersections(Vector3 position, out AbstractCombatProcessor processor, float distance)
        {
            processor = null;
            for (int i = 0; i < _intersectors.Count; i++)
                if (_vectorUtils.InDistance(_intersectors[i].CombatProcessor.transform.position - position, distance))
                {
                    processor = _intersectors[i].CombatProcessor;
                    return true;
                }
            return false;
        }
    }
}