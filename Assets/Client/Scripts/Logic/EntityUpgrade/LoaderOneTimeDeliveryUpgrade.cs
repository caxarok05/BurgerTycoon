using Client.Data;
using Client.Infrastructure.Factory;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.StaticData;
using Client.StaticData;

namespace Client.Logic.EntityUpgrade
{
    public class LoaderOneTimeDeliveryUpgrade : IUpgradable
    {
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IMoneyService _moneyService;

        public LoaderOneTimeDeliveryUpgrade(IGameFactory gameFactory, IPersistentProgressService progressService,
            IStaticDataService staticDataService, IMoneyService moneyService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _moneyService = moneyService;
        }

        public bool CanUpgrade()
        {
            return _staticDataService.ForLoaderOneTimeDeliver(_progressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel + 1).Cost <= _moneyService.DisplayMoney();
        }

        public void Upgrade()
        {
            _moneyService.SpendMoney(_staticDataService.ForLoaderOneTimeDeliver(_progressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel + 1).Cost);
            _progressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel += 1;
            _gameFactory.UpgradeLoaderOneTimeDeliver();
        }

        public IUpgradeStaticData GetNewData()
        {
            if (_progressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel + 1 > LoaderOneTimeDeliverUpgradeData.MaxLevel)
            {
                return _staticDataService.ForLoaderOneTimeDeliver(_progressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel);
            }
            return _staticDataService.ForLoaderOneTimeDeliver(_progressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel + 1);
        }

        public int ReturnRealLevel()
        {
            return _progressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel;
        }

        public int ReturnMaxLevel()
        {
            return LoaderOneTimeDeliverUpgradeData.MaxLevel;
        }

        public int GetUpgradeCost()
        {
            return _staticDataService.ForLoaderOneTimeDeliver(_progressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel + 1).Cost;
        }
    }
}
