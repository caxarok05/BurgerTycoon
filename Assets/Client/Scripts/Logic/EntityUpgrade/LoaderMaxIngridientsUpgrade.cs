using Client.Data;
using Client.Infrastructure.Factory;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.StaticData;
using Client.StaticData;

namespace Client.Logic.EntityUpgrade
{
    public class LoaderMaxIngridientsUpgrade : IUpgradable
    {
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IMoneyService _moneyService;

        public LoaderMaxIngridientsUpgrade(IGameFactory gameFactory, IPersistentProgressService progressService,
            IStaticDataService staticDataService, IMoneyService moneyService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _moneyService = moneyService;
        }

        public bool CanUpgrade()
        {
            return _staticDataService.ForLoaderMaxIngridients(_progressService.Progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel + 1).Cost <= _moneyService.DisplayMoney();
        }

        public void Upgrade()
        {
            _moneyService.SpendMoney(_staticDataService.ForLoaderMaxIngridients(_progressService.Progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel + 1).Cost);
            _progressService.Progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel += 1;
            _gameFactory.UpgradeLoaderMaxIngridients();
        }

        public IUpgradeStaticData GetNewData()
        {
            if (_progressService.Progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel + 1 > LoaderMaxIngridientsUpgradeData.MaxLevel)
            {
                return _staticDataService.ForLoaderMaxIngridients(_progressService.Progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel);
            }
            return _staticDataService.ForLoaderMaxIngridients(_progressService.Progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel + 1);
        }

        public int ReturnRealLevel()
        {
            return _progressService.Progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel;
        }
    }
}
