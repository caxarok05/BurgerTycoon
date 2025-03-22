using System;

namespace Client.Data
{
    [Serializable]
    public class ChefUpgradeData
    {     
        public int Cost;
        public int ChefAmount;
        public int UpgradeLevel;
        public const int MaxLevel = 8;
    }

}