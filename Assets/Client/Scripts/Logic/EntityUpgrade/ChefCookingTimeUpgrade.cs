using Client.Data;
using Client.Infrastructure.Factory;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.StaticData;
using Client.StaticData;

namespace Client.Logic.EntityUpgrade
{
    public class ChefCookingTimeUpgrade : IUpgradable
    {
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IMoneyService _moneyService;

        public ChefCookingTimeUpgrade(IGameFactory gameFactory, IPersistentProgressService progressService,
            IStaticDataService staticDataService, IMoneyService moneyService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _moneyService = moneyService;
        }

        public bool CanUpgrade()
        {
            return _staticDataService.ForChefCookingTime(_progressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel + 1).Cost <= _moneyService.DisplayMoney();
        }

        public void Upgrade()
        {
            _moneyService.SpendMoney(_staticDataService.ForChefCookingTime(_progressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel + 1).Cost);
            _progressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel += 1;
            _gameFactory.UpgradeChefCookingTime();
        }

        public IUpgradeStaticData GetNewData()
        {
            if (_progressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel + 1 > ChefCookingTimeUpgradeData.MaxLevel)
            {
                return _staticDataService.ForChefCookingTime(_progressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel);
            }
            return _staticDataService.ForChefCookingTime(_progressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel + 1);
        }

        public int ReturnRealLevel()
        {
            return _progressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel;
        }

        public int ReturnMaxLevel()
        {
            return ChefCookingTimeUpgradeData.MaxLevel;
        }

        public int GetUpgradeCost()
        {
            return _staticDataService.ForChefCookingTime(_progressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel + 1).Cost;
        }
    }

}
