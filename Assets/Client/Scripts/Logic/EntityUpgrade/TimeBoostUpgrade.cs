using Client.Data;
using Client.Infrastructure.Factory;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.StaticData;
using Client.StaticData;

namespace Client.Logic.EntityUpgrade
{
    public class TimeBoostUpgrade : IUpgradable
    {
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IMoneyService _moneyService;

        public TimeBoostUpgrade(IGameFactory gameFactory, IPersistentProgressService progressService,
            IStaticDataService staticDataService, IMoneyService moneyService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _moneyService = moneyService;
        }

        public bool CanUpgrade()
        {
            return _staticDataService.ForTimeBooster(_progressService.Progress.upgradeData.timeUpgrade.UpgradeLevel + 1).Cost <= _moneyService.DisplayMoney();
        }

        public void Upgrade()
        {
             _gameFactory.UpgradeTimeBooster();
            _moneyService.SpendMoney(_staticDataService.ForTimeBooster(_progressService.Progress.upgradeData.timeUpgrade.UpgradeLevel + 1).Cost);
            _progressService.Progress.upgradeData.timeUpgrade.UpgradeLevel += 1;
        }

        public IUpgradeStaticData GetNewData()
        {
            if (_progressService.Progress.upgradeData.timeUpgrade.UpgradeLevel + 1 > TimeBoostUpgradeData.MaxLevel)
            {
                return _staticDataService.ForTimeBooster(_progressService.Progress.upgradeData.timeUpgrade.UpgradeLevel);
            }
            return _staticDataService.ForTimeBooster(_progressService.Progress.upgradeData.timeUpgrade.UpgradeLevel + 1);
        }

        public int ReturnRealLevel()
        {
            return _progressService.Progress.upgradeData.timeUpgrade.UpgradeLevel;
        }

        public int ReturnMaxLevel()
        {
            return TimeBoostUpgradeData.MaxLevel;
        }

        public int GetUpgradeCost()
        {
            return _staticDataService.ForTimeBooster(_progressService.Progress.upgradeData.timeUpgrade.UpgradeLevel + 1).Cost;
        }
    }

}
