using System;

namespace PurpleSlayerFish.Core.Data
{
    [Serializable]
    public class SettingsData : IStorableData
    {
        public bool IsNew { get; set; } = true;
        public bool IsSoundEnabled;
    }
}