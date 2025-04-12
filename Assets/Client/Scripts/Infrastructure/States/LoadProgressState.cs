using Client.Services.PersistentProgress;
using Client.Services.SaveLoad;
using Client.Data;
using Client.Services.MoneyService;

namespace Client.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadProgress;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadProgress)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadProgress = saveLoadProgress;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, string>("SampleScene");
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress =
              _saveLoadProgress.LoadProgress()
              ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress();

            progress.moneyAmount = 100;

            progress.upgradeData.chefUpgrade.UpgradeLevel = 1;
            progress.upgradeData.loaderUpgrade.UpgradeLevel = 1;
            progress.upgradeData.cashierUpgrade.UpgradeLevel = 1;
            progress.upgradeData.burgerUpgrade.UpgradeLevel = 1;

            progress.upgradeData.timeUpgrade.UpgradeLevel = 0;

            progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel = 0;
            progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel = 0;
            progress.upgradeData.customerSpeedUpgrade.UpgradeLevel = 0;

            progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel = 0;
            progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel = 0;
            progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel = 0;

            progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel = 0;
            progress.upgradeData.chefOneTimeCookedUpgrade.UpgradeLevel = 0;
            
            progress.upgradeData.cashierServingTimeUpgrade.UpgradeLevel = 0;
            progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel = 0;
            progress.upgradeData.loaderIngridentsRechargeUpgrade.UpgradeLevel = 0;

            return progress;
        }
    }
}