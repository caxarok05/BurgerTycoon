using System;

namespace Client.Data
{
    [Serializable]
    public class CashierUpgradeData
    {
        public int Cost;
        public int CashierAmount;
        public int UpgradeLevel;
        public const int MaxLevel = 4;
    }
}