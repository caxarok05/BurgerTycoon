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
        public TimeBoostUpgradeData timeUpgrade;

        public LoaderSpeedUpgradeData loaderSpeedUpgrade;
        public CashierSpeedUpgradeData cashierSpeedUpgrade;
        public CustomerSpeedUpgradeData customerSpeedUpgrade;

        public LoaderMaxIngridientsUpgradeData loaderMaxIngridientsUpgrade;
        public ChefMaxIngridientsUpgradeData chefMaxIngridientsUpgrade;
        public ChefMaxDishesUpgradeData chefMaxDishesUpgrade;

        public ChefOneTimeCookingUpgradeData chefOneTimeCookedUpgrade;
        public LoaderOneTimeDeliverUpgradeData LoaderOneTimeDeliverUpgrade;

        public ChefCookingTimeUpgradeData chefCookingTimeUpgrade;
        public CashierServingTimeUpgradeData cashierServingTimeUpgrade;
        public LoaderIngridientsRechargeUpgradeData loaderIngridentsRechargeUpgrade;

        public UpgradeData()
        {
            chefUpgrade = new ChefUpgradeData();
            loaderUpgrade = new LoaderUpgradeData();
            cashierUpgrade = new CashierUpgradeData();
            burgerUpgrade = new BurgerUpgradeData();

            timeUpgrade = new TimeBoostUpgradeData();

            loaderSpeedUpgrade = new LoaderSpeedUpgradeData();
            cashierSpeedUpgrade = new CashierSpeedUpgradeData();
            customerSpeedUpgrade = new CustomerSpeedUpgradeData();

            loaderMaxIngridientsUpgrade = new LoaderMaxIngridientsUpgradeData();
            chefMaxIngridientsUpgrade = new ChefMaxIngridientsUpgradeData();
            chefMaxDishesUpgrade = new ChefMaxDishesUpgradeData();

            chefOneTimeCookedUpgrade = new ChefOneTimeCookingUpgradeData();
            LoaderOneTimeDeliverUpgrade = new LoaderOneTimeDeliverUpgradeData();

            chefCookingTimeUpgrade = new ChefCookingTimeUpgradeData();
            cashierServingTimeUpgrade = new CashierServingTimeUpgradeData();
            loaderIngridentsRechargeUpgrade = new LoaderIngridientsRechargeUpgradeData();
        }
    }
}