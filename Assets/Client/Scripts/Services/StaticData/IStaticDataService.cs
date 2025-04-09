using Client.Services.StaticData;
using Client.StaticData;

namespace Client.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        ChefStaticData ForChefs(int upgradeLevel);
        LoaderStaticData ForLoaders(int upgradeLevel);
        CashierStaticData ForCashiers(int upgradeLevel);
        BurgerCostStaticData ForBurgers(int upgradeLevel);
        TimeBoosterStaticData ForTimeBooster(int upgradeLevel);
        CashierSpeedStaticData ForCashierSpeed(int upgradeLevel);
        LoaderSpeedStaticData ForLoaderSpeed(int upgradeLevel);
        CustomerSpeedStaticData ForCustomerSpeed(int upgradeLevel);
        LoaderMaxIngridiensStaticData ForLoaderMaxIngridients(int upgradeLevel);
        ChefMaxIngridiensStaticData ForChefMaxIngridients(int upgradeLevel);
        ChefMaxDishesStaticData ForChefMaxDishes(int upgradeLevel);
        ChefOneTimeCookedStaticData ForChefOneTimeCooked(int upgradeLevel);
        LoaderOneTimeDeliverStaticData ForLoaderOneTimeDeliver(int upgradeLevel);
        CashierServingTimeStaticData ForCashierServingTime(int upgradeLevel);
        ChefCookingTimeStaticData ForChefCookingTime(int upgradeLevel);
    }
}