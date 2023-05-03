using PurpleSlayerFish.Game.Processors.Combat.Impls;

namespace PurpleSlayerFish.Game.Processors.Combat
{
    public interface ICombat
    {
        AbstractCombatProcessor CombatProcessor { get; }
    }
}