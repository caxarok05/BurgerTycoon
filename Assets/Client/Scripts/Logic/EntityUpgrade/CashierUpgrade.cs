using Client.Data;
using Client.Infrastructure.Factory;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.StaticData;
using Client.StaticData;

namespace Client.Logic.EntityUpgrade
{
    public class CashierUpgrade : IUpgradable
    {
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IMoneyService _moneyService;

        public CashierUpgrade(IGameFactory gameFactory, IPersistentProgressService progressService,
            IStaticDataService staticDataService, IMoneyService moneyService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _moneyService = moneyService;
        }

        public bool CanUpgrade()
        {
            return _staticDataService.ForCashiers(_progressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel + 1).Cost <= _moneyService.DisplayMoney();
        }

        public void Upgrade()
        {
            _gameFactory.UpgradeCashiers();
            _moneyService.SpendMoney(_staticDataService.ForCashiers(_progressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel + 1).Cost);
            _progressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel += 1;
        }

        public IUpgradeStaticData GetNewData()
        {
            if (_progressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel + 1 > CashierUpgradeData.MaxLevel)
            {
                return _staticDataService.ForCashiers(_progressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel);
            }
            return _staticDataService.ForCashiers(_progressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel + 1);
        }

        public int ReturnRealLevel()
        {
            return _progressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel;
        }

        public int ReturnMaxLevel()
        {
            return CashierUpgradeData.MaxLevel;
        }

        public int GetUpgradeCost()
        {
            return _staticDataService.ForCashiers(_progressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel + 1).Cost;
        }
    }
}
