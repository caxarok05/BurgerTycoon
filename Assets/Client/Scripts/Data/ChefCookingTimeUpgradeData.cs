using System;

namespace Client.Data
{
    [Serializable]
    public class ChefCookingTimeUpgradeData
    {
        public int Cost;
        public int UpgradeLevel;
        public const int MaxLevel = 3;
    }
}