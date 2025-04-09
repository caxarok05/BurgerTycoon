using System;

namespace Client.Data
{
    [Serializable]
    public class TimeBoostUpgradeData
    {
        public int Cost;
        public int UpgradeLevel;
        public const int MaxLevel = 1;
    }
}