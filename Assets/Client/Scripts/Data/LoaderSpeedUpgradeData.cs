using System;

namespace Client.Data
{
    [Serializable]
    public class LoaderSpeedUpgradeData
    {
        public int Cost;
        public int UpgradeLevel;
        public const int MaxLevel = 4;
    }
}