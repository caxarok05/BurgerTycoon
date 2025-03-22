using System;

namespace Client.Data
{
    [Serializable]
    public class LoaderUpgradeData
    {
        public int Cost;
        public int LoaderAmount;
        public int UpgradeLevel;
        public const int MaxLevel = 6;
    }

}