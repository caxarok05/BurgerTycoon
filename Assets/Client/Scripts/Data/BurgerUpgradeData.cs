using System;

namespace Client.Data
{
    [Serializable]
    public class BurgerUpgradeData
    {
        public int Cost;
        public int BurgerAmount;
        public int UpgradeLevel;
        public const int MaxLevel = 4;
    }
}