using PurpleSlayerFish.Game.Processors.Ai;
using Zenject;

namespace PurpleSlayerFish.Game.Behaviours
{
    public class RatBehaviour : AiBehaviour
    {
        public override float IntersectionOffset => _gameConfig.RatOffset;
    }
}