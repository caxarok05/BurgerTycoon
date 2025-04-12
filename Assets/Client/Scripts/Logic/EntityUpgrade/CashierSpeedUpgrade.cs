using Client.Data;
using Client.Infrastructure.Factory;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.StaticData;
using Client.StaticData;

namespace Client.Logic.EntityUpgrade
{
    public class CashierSpeedUpgrade : IUpgradable
    {
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IMoneyService _moneyService;

        public CashierSpeedUpgrade(IGameFactory gameFactory, IPersistentProgressService progressService,
            IStaticDataService staticDataService, IMoneyService moneyService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _moneyService = moneyService;
        }

        public bool CanUpgrade()
        {
            return _staticDataService.ForCashierSpeed(_progressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel + 1).Cost <= _moneyService.DisplayMoney();
        }

        public void Upgrade()
        {
            _gameFactory.UpgradeCashierSpeed();
            _moneyService.SpendMoney(_staticDataService.ForCashierSpeed(_progressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel + 1).Cost);
            _progressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel += 1;
        }

        public IUpgradeStaticData GetNewData()
        {
            if (_progressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel + 1 > CashierSpeedUpgradeData.MaxLevel)
            {
                return _staticDataService.ForCashierSpeed(_progressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel);
            }
            return _staticDataService.ForCashierSpeed(_progressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel + 1);
        }

        public int ReturnRealLevel()
        {
            return _progressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel;
        }

        public int ReturnMaxLevel()
        {
            return CashierSpeedUpgradeData.MaxLevel;
        }

        public int GetUpgradeCost()
        {
            return _staticDataService.ForCashierSpeed(_progressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel + 1).Cost;
        }
    }
}
