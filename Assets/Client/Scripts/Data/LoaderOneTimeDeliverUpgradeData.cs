using System;

namespace Client.Data
{
    [Serializable]
    public class LoaderOneTimeDeliverUpgradeData
    {
        public int Cost;
        public int UpgradeLevel;
        public const int MaxLevel = 2;
    }
}