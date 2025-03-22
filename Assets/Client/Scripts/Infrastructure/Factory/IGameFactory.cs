using Client.Services;
using Client.Services.PersistentProgress;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        List<GameObject> chefTables { get; }
        List<GameObject> loaderCrates { get; }
        List<GameObject> cashierPoints { get; }
        List<Transform> customerPoints { get; }

        void Cleanup();

        GameObject CreateRestaraunt();

        void CreateChefTable();
        void CreateStorageCrate();
        void CreateOrderPlace();
        GameObject CreateRandomCustomer();

        void CreateHud();
        void CreateNavMesh();
        void Register(ISavedProgressReader progressReader);

        void CreateUpdates();

        void UpgradeChefs();
        void UpgradeLoaders();
        void UpgradeCashiers();
        void UpgradeBurgers();
    }
}