using Client.Data;
using Client.Infrastructure.Factory;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.StaticData;
using Client.StaticData;

namespace Client.Logic.EntityUpgrade
{
    public class ChefMaxIngridientsUpgrade : IUpgradable
    {
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IMoneyService _moneyService;

        public ChefMaxIngridientsUpgrade(IGameFactory gameFactory, IPersistentProgressService progressService,
            IStaticDataService staticDataService, IMoneyService moneyService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _moneyService = moneyService;
        }

        public bool CanUpgrade()
        {
            return _staticDataService.ForChefMaxIngridients(_progressService.Progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel + 1).Cost <= _moneyService.DisplayMoney();
        }

        public void Upgrade()
        {
            _moneyService.SpendMoney(_staticDataService.ForChefMaxIngridients(_progressService.Progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel + 1).Cost);
            _progressService.Progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel += 1;
            _gameFactory.UpgradeChefMaxIngridients();
        }

        public IUpgradeStaticData GetNewData()
        {
            if (_progressService.Progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel + 1 > ChefMaxIngridientsUpgradeData.MaxLevel)
            {
                return _staticDataService.ForChefMaxIngridients(_progressService.Progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel);
            }
            return _staticDataService.ForChefMaxIngridients(_progressService.Progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel + 1);
        }

        public int ReturnRealLevel()
        {
            return _progressService.Progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel;
        }
    }
}
