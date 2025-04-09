using System;

namespace Client.Data
{
    [Serializable]
    public class ChefOneTimeCookingUpgradeData
    {
        public int Cost;
        public int UpgradeLevel;
        public const int MaxLevel = 2;
    }
}