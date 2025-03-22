using Client.Logic;
using Client.Infrastructure.Factory;
using Client.Services.PersistentProgress;

namespace Client.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
          _loadingCurtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            InitLocation();
            InitHud();
        }
        private void InitLocation()
        {
            _gameFactory.CreateUpdates();

            _gameFactory.CreateRestaraunt();
            _gameFactory.CreateChefTable();
            _gameFactory.CreateStorageCrate();
            _gameFactory.CreateOrderPlace();
            _gameFactory.CreateNavMesh();
            _gameFactory.UpgradeBurgers();
        }
        private void InitHud()
        {
            _gameFactory.CreateHud();
        }
    }

}