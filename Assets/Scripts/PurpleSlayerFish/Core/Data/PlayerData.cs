using System;

namespace PurpleSlayerFish.Core.Data
{
    [Serializable]
    public class PlayerData : IStorableData
    {
        public bool IsNew { get; set; } = true;

        public int Bricks;
    }
}