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
        List<GameObject> customers { get; }

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
        void UpgradeTimeBooster();
        void UpgradeCustomerSpeed();
        void UpgradeLoaderSpeed();
        void UpgradeCashierSpeed();
        void UpgradeLoaderMaxIngridients();
        void UpgradeChefMaxDishes();
        void UpgradeChefMaxIngridients();
        void UpgradeLoaderOneTimeDeliver();
        void UpgradeChefOneTimeCooked();
        void UpgradeCashierServingTime();
        void UpgradeChefCookingTime();
    }
}