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

        private const string LevelsDataPath = "Static Data/Levels";

        private Dictionary<int, ChefStaticData> _chefs;
        private Dictionary<int, LoaderStaticData> _loader;
        private Dictionary<int, CashierStaticData> _cashier;
        private Dictionary<int, BurgerCostStaticData> _burger;


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
    }
}