using Client.Data;
using Client.Infrastructure.Factory;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.StaticData;
using Client.StaticData;

namespace Client.Logic.EntityUpgrade
{
    public class ChefMaxDishesUpgrade : IUpgradable
    {
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IMoneyService _moneyService;

        public ChefMaxDishesUpgrade(IGameFactory gameFactory, IPersistentProgressService progressService,
            IStaticDataService staticDataService, IMoneyService moneyService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _moneyService = moneyService;
        }

        public bool CanUpgrade()
        {
            return _staticDataService.ForChefMaxDishes(_progressService.Progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel + 1).Cost <= _moneyService.DisplayMoney();
        }

        public void Upgrade()
        {
            _moneyService.SpendMoney(_staticDataService.ForChefMaxDishes(_progressService.Progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel + 1).Cost);
            _progressService.Progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel += 1;
            _gameFactory.UpgradeChefMaxDishes();
        }

        public IUpgradeStaticData GetNewData()
        {
            if (_progressService.Progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel + 1 > ChefMaxDishesUpgradeData.MaxLevel)
            {
                return _staticDataService.ForChefMaxDishes(_progressService.Progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel);
            }
            return _staticDataService.ForChefMaxDishes(_progressService.Progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel + 1);
        }

        public int ReturnRealLevel()
        {
            return _progressService.Progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel;
        }
    }
}
