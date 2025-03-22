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
    }
}