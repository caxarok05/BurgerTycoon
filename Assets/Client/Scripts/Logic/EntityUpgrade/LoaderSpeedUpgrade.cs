using Client.Data;
using Client.Infrastructure.Factory;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.StaticData;
using Client.StaticData;

namespace Client.Logic.EntityUpgrade
{
    public class LoaderSpeedUpgrade : IUpgradable
    {
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IMoneyService _moneyService;

        public LoaderSpeedUpgrade(IGameFactory gameFactory, IPersistentProgressService progressService,
            IStaticDataService staticDataService, IMoneyService moneyService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _moneyService = moneyService;
        }

        public bool CanUpgrade()
        {
            return _staticDataService.ForLoaderSpeed(_progressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel + 1).Cost <= _moneyService.DisplayMoney();
        }

        public void Upgrade()
        {
            _gameFactory.UpgradeLoaderSpeed();
            _moneyService.SpendMoney(_staticDataService.ForLoaderSpeed(_progressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel + 1).Cost);
            _progressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel += 1;
        }

        public IUpgradeStaticData GetNewData()
        {
            if (_progressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel + 1 > LoaderSpeedUpgradeData.MaxLevel)
            {
                return _staticDataService.ForLoaderSpeed(_progressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel);
            }
            return _staticDataService.ForLoaderSpeed(_progressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel + 1);
        }

        public int ReturnRealLevel()
        {
            return _progressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel;
        }

        public int ReturnMaxLevel()
        {
            return LoaderSpeedUpgradeData.MaxLevel;
        }

        public int GetUpgradeCost()
        {
            return _staticDataService.ForLoaderSpeed(_progressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel + 1).Cost;
        }
    }
}
