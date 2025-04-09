using System;

namespace Client.Data
{
    [Serializable]
    public class CashierSpeedUpgradeData
    {
        public int Cost;
        public int UpgradeLevel;
        public const int MaxLevel = 4;
    }
}