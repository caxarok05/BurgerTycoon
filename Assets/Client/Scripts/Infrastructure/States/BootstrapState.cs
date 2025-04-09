using Client.Infrastructure.AssetManagement;
using Client.Infrastructure.Factory;
using Client.Services;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.Randomizer;
using Client.Services.SaveLoad;
using Client.Services.StaticData;
using Client.Services.TimeService;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "SampleScene";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter() =>
          _sceneLoader.Load(Initial, onLoaded: EnterLoadProgress);

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            RegisterStaticDataService();
            _services.RegisterSingle<IRandomService>(new RandomService());
            _services.RegisterSingle<ITimeController>(new TimeController());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IMoneyService>(new MoneyService(_services.Single<IPersistentProgressService>()));

            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(), 
                _services.Single<IPersistentProgressService>(), 
                _services.Single<IMoneyService>(), 
                _services.Single<IStaticDataService>(),
                _services.Single<ITimeController>())
            );

            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
        }

        private void EnterLoadProgress() =>
          _stateMachine.Enter<LoadProgressState>();
    }
}

