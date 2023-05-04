using PurpleSlayerFish.Game;
using UnityEngine;

namespace PurpleSlayerFish.Core.Behaviours
{
    public abstract class AbstractBehaviour : MonoBehaviour, IInitializable
    {
        public abstract void Initialize();
    }
}