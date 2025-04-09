using Client.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Client.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string ChefsDataPath = "Static Data/Chef";
        private const string LoadersDataPath = "Static Data/Loader";
        private const string CashierDataPath = "Static Data/Cashier";
        private const string BurgerDataPath = "Static Data/Burger";
        private const string TimeBoosterDataPath = "Static Data/TimeBooster";

        private const string CashierSpeedDataPath = "Static Data/Speed/CashierSpeed";
        private const string LoaderSpeedDataPath = "Static Data/Speed/LoaderSpeed";
        private const string CustomerSpeedDataPath = "Static Data/Speed/CustomerSpeed";

        private const string LoaderMaxIngridientsDataPath = "Static Data/IncreaseIngridients/LoaderMaxIngridients";
        private const string ChefMaxIngridientsDataPath = "Static Data/IncreaseIngridients/ChefMaxIngridients";
        private const string ChefMaxDishesDataPath = "Static Data/IncreaseIngridients/ChefMaxDishes";

        private const string ChefOneTimeCookedDataPath = "Static Data/OneTimeProcess/ChefOneTimeCooked";
        private const string LoaderOneTimeDeliverDataPath = "Static Data/OneTimeProcess/LoaderOneTimeDeliver";

        private const string CashierServingTimeDataPath = "Static Data/TimeReduction/CashierServingTime";
        private const string ChefCookingTimeDataPath = "Static Data/TimeReduction/ChefCookingTime";

        private const string LevelsDataPath = "Static Data/Levels";

        private Dictionary<int, ChefStaticData> _chefs;
        private Dictionary<int, LoaderStaticData> _loader;
        private Dictionary<int, CashierStaticData> _cashier;
        private Dictionary<int, BurgerCostStaticData> _burger;
        private Dictionary<int, TimeBoosterStaticData> _timeBooster;

        private Dictionary<int, CashierSpeedStaticData> _cashierSpeed;
        private Dictionary<int, LoaderSpeedStaticData> _loaderSpeed;
        private Dictionary<int, CustomerSpeedStaticData> _customerSpeed;

        private Dictionary<int, LoaderMaxIngridiensStaticData> _loaderMaxIngridients;
        private Dictionary<int, ChefMaxIngridiensStaticData> _chefMaxIngridients;
        private Dictionary<int, ChefMaxDishesStaticData> _chefMaxDishes;

        private Dictionary<int, ChefOneTimeCookedStaticData> _chefOneTimeCooked;
        private Dictionary<int, LoaderOneTimeDeliverStaticData> _loaderOneTimeDeliver;

        private Dictionary<int, CashierServingTimeStaticData> _cashierServingTime;
        private Dictionary<int, ChefCookingTimeStaticData> _chefCookingTime;


        public void Load()
        {
            _chefs = Resources
              .LoadAll<ChefStaticData>(ChefsDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _loader = Resources
              .LoadAll<LoaderStaticData>(LoadersDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _cashier = Resources
              .LoadAll<CashierStaticData>(CashierDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _burger = Resources
              .LoadAll<BurgerCostStaticData>(BurgerDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);
            
            _timeBooster = Resources
              .LoadAll<TimeBoosterStaticData>(TimeBoosterDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _cashierSpeed = Resources
              .LoadAll<CashierSpeedStaticData>(CashierSpeedDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _loaderSpeed = Resources
              .LoadAll<LoaderSpeedStaticData>(LoaderSpeedDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _customerSpeed = Resources
              .LoadAll<CustomerSpeedStaticData>(CustomerSpeedDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _loaderMaxIngridients = Resources
              .LoadAll<LoaderMaxIngridiensStaticData>(LoaderMaxIngridientsDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _chefMaxIngridients = Resources
              .LoadAll<ChefMaxIngridiensStaticData>(ChefMaxIngridientsDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _chefMaxDishes = Resources
              .LoadAll<ChefMaxDishesStaticData>(ChefMaxDishesDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _chefOneTimeCooked = Resources
              .LoadAll<ChefOneTimeCookedStaticData>(ChefOneTimeCookedDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _loaderOneTimeDeliver = Resources
              .LoadAll<LoaderOneTimeDeliverStaticData>(LoaderOneTimeDeliverDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _cashierServingTime = Resources
              .LoadAll<CashierServingTimeStaticData>(CashierServingTimeDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);

            _chefCookingTime = Resources
              .LoadAll<ChefCookingTimeStaticData>(ChefCookingTimeDataPath)
              .ToDictionary(x => x.UpgradeLevel, x => x);
        }

        public ChefStaticData ForChefs(int upgradeLevel) =>
          _chefs.TryGetValue(upgradeLevel, out ChefStaticData staticData)
            ? staticData
            : null;

        public LoaderStaticData ForLoaders(int upgradeLevel) =>
          _loader.TryGetValue(upgradeLevel, out LoaderStaticData staticData)
            ? staticData
            : null;

        public CashierStaticData ForCashiers(int upgradeLevel) =>
          _cashier.TryGetValue(upgradeLevel, out CashierStaticData staticData)
            ? staticData
            : null;

        public BurgerCostStaticData ForBurgers(int upgradeLevel) =>
          _burger.TryGetValue(upgradeLevel, out BurgerCostStaticData staticData)
            ? staticData
            : null;
        public TimeBoosterStaticData ForTimeBooster(int upgradeLevel) =>
          _timeBooster.TryGetValue(upgradeLevel, out TimeBoosterStaticData staticData)
            ? staticData
            : null;
        public CashierSpeedStaticData ForCashierSpeed(int upgradeLevel) =>
          _cashierSpeed.TryGetValue(upgradeLevel, out CashierSpeedStaticData staticData)
            ? staticData
            : null;
        public LoaderSpeedStaticData ForLoaderSpeed(int upgradeLevel) =>
          _loaderSpeed.TryGetValue(upgradeLevel, out LoaderSpeedStaticData staticData)
            ? staticData
            : null;
        public CustomerSpeedStaticData ForCustomerSpeed(int upgradeLevel) =>
          _customerSpeed.TryGetValue(upgradeLevel, out CustomerSpeedStaticData staticData)
            ? staticData
            : null;

        public LoaderMaxIngridiensStaticData ForLoaderMaxIngridients(int upgradeLevel) =>
          _loaderMaxIngridients.TryGetValue(upgradeLevel, out LoaderMaxIngridiensStaticData staticData)
            ? staticData
            : null;

        public ChefMaxIngridiensStaticData ForChefMaxIngridients(int upgradeLevel) =>
          _chefMaxIngridients.TryGetValue(upgradeLevel, out ChefMaxIngridiensStaticData staticData)
            ? staticData
            : null;

        public ChefMaxDishesStaticData ForChefMaxDishes(int upgradeLevel) =>
          _chefMaxDishes.TryGetValue(upgradeLevel, out ChefMaxDishesStaticData staticData)
            ? staticData
            : null;

        public ChefOneTimeCookedStaticData ForChefOneTimeCooked(int upgradeLevel) =>
          _chefOneTimeCooked.TryGetValue(upgradeLevel, out ChefOneTimeCookedStaticData staticData)
            ? staticData
            : null;

        public LoaderOneTimeDeliverStaticData ForLoaderOneTimeDeliver(int upgradeLevel) =>
          _loaderOneTimeDeliver.TryGetValue(upgradeLevel, out LoaderOneTimeDeliverStaticData staticData)
            ? staticData
            : null;

        public CashierServingTimeStaticData ForCashierServingTime(int upgradeLevel) =>
          _cashierServingTime.TryGetValue(upgradeLevel, out CashierServingTimeStaticData staticData)
            ? staticData
            : null;

        public ChefCookingTimeStaticData ForChefCookingTime(int upgradeLevel) =>
          _chefCookingTime.TryGetValue(upgradeLevel, out ChefCookingTimeStaticData staticData)
            ? staticData
            : null;

    }
}