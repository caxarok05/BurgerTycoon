using System;

namespace Client.Data
{
    [Serializable]
    public class UpgradeData
    {
        public ChefUpgradeData chefUpgrade;
        public LoaderUpgradeData loaderUpgrade;
        public CashierUpgradeData cashierUpgrade;
        public BurgerUpgradeData burgerUpgrade;

        public UpgradeData()
        {
            chefUpgrade = new ChefUpgradeData();
            loaderUpgrade = new LoaderUpgradeData();
            cashierUpgrade = new CashierUpgradeData();
            burgerUpgrade = new BurgerUpgradeData();
        }
    }

}