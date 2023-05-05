using JetBrains.Annotations;
using UnityEngine;

namespace PurpleSlayerFish.Game.Processors.Ai.States
{
    public abstract class AbstractState : ScriptableObject
    {
        public abstract void Launch(AiBehaviour behaviour);

        public virtual void Finish(AiBehaviour behaviour)
        {
            
        }

        [NotNull] public string Name => GetType().Name;
    }
}