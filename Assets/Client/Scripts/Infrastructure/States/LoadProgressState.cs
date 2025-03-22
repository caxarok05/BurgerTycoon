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

            return progress;
        }
    }
}