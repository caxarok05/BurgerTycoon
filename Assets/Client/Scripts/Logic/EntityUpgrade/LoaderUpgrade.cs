using Client.Data;
using Client.Infrastructure.Factory;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.StaticData;
using Client.StaticData;

namespace Client.Logic.EntityUpgrade
{
    public class LoaderUpgrade : IUpgradable
    {
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IMoneyService _moneyService;

        public LoaderUpgrade(IGameFactory gameFactory, IPersistentProgressService progressService,
            IStaticDataService staticDataService, IMoneyService moneyService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _moneyService = moneyService;
        }

        public bool CanUpgrade()
        {
            return _staticDataService.ForLoaders(_progressService.Progress.upgradeData.loaderUpgrade.UpgradeLevel + 1).Cost <= _moneyService.DisplayMoney();
        }

        public void Upgrade()
        {
            _gameFactory.UpgradeLoaders();
            _moneyService.SpendMoney(_staticDataService.ForLoaders(_progressService.Progress.upgradeData.loaderUpgrade.UpgradeLevel + 1).Cost);
            _progressService.Progress.upgradeData.loaderUpgrade.UpgradeLevel += 1;
        }

        public IUpgradeStaticData GetNewData()
        {
            if (_progressService.Progress.upgradeData.loaderUpgrade.UpgradeLevel + 1 > LoaderUpgradeData.MaxLevel)
            {
                return _staticDataService.ForLoaders(_progressService.Progress.upgradeData.loaderUpgrade.UpgradeLevel);
            }
            return _staticDataService.ForLoaders(_progressService.Progress.upgradeData.loaderUpgrade.UpgradeLevel + 1);
        }

        public int ReturnRealLevel()
        {
            return _progressService.Progress.upgradeData.loaderUpgrade.UpgradeLevel;
        }

        public int ReturnMaxLevel()
        {
            return LoaderUpgradeData.MaxLevel;
        }

        public int GetUpgradeCost()
        {
            return _staticDataService.ForLoaders(_progressService.Progress.upgradeData.loaderUpgrade.UpgradeLevel + 1).Cost;
        }
    }
}
