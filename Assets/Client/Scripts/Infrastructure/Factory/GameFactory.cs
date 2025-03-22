using Client.Data;
using Client.Infrastructure.AssetManagement;
using Client.Logic.EntityUpgrade;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.StaticData;
using Client.StaticData;
using Client.UI;
using Client.Units.Cashier;
using Client.Units.Chef;
using Client.Units.Loader;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public List<GameObject> chefTables { get; private set; } = new List<GameObject>();
        public List<GameObject> loaderCrates { get; private set; } = new List<GameObject>();
        public List<GameObject> cashierPoints { get; private set; } = new List<GameObject>();
        public List<Transform> customerPoints { get; private set; } = new List<Transform>();

        private readonly IAssetProvider _assets;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IMoneyService _moneyService;

        private List<Transform> _chefPoints = new List<Transform>();
        private List<Transform> _cratePoints = new List<Transform>();
        private List<Transform> _orderPoints = new List<Transform>();

        private ChefUpgrade _chefUpgrade;
        private LoaderUpgrade _loaderUpgrade;
        private CashierUpgrade _cashierUpgrade;
        private BurgerCostUpgrade _burgerUpgrade;
        private GameObject navMesh;

        private const float BurgerUpgradeCost = 1.3f;

        public GameFactory(IAssetProvider assets, IPersistentProgressService persistentProgress, 
            IMoneyService moneyService, IStaticDataService staticData)
        {
            _assets = assets;
            _persistentProgressService = persistentProgress;
            _moneyService = moneyService;
            _staticDataService = staticData;
        }

        public GameObject CreateRestaraunt()
        {
            var restaraunt = _assets.Instantiate(path: AssetPath.Restaraunt);
            _chefPoints = restaraunt.GetComponent<SpawnPointsData>().chefPoints;
            _cratePoints = restaraunt.GetComponent<SpawnPointsData>().loaderPoints;
            _orderPoints = restaraunt.GetComponent<SpawnPointsData>().orderPoints;
            customerPoints = restaraunt.GetComponent<SpawnPointsData>().customerPoints;
            return restaraunt;
        }

        public void CreateChefTable()
        {
            for (int i = 0; i < _persistentProgressService.Progress.upgradeData.chefUpgrade.UpgradeLevel; i++)
            {
                var table = _assets.Instantiate(AssetPath.ChefTable, _chefPoints[0].transform.position);
                _chefPoints.RemoveAt(0);
                var chef = CreateChef(table.GetComponent<ChefTable>().chefPlace.gameObject);
                table.GetComponent<ChefTable>().AddNewChef(chef.GetComponent<ChefBehaviour>());
                chefTables.Add(table);
            }                  
        }

        public void UpgradeChefs()
        {
            var table = _assets.Instantiate(AssetPath.ChefTable, _chefPoints[0].transform.position);
            _chefPoints.RemoveAt(0);
            var chef = CreateChef(table.GetComponent<ChefTable>().chefPlace.gameObject);
            table.GetComponent<ChefTable>().AddNewChef(chef.GetComponent<ChefBehaviour>());
            chefTables.Add(table);
            RefreshChefs();
            UpdateNavMesh();
        }


        public void CreateStorageCrate()
        {
            for (int i = 0; i < _persistentProgressService.Progress.upgradeData.loaderUpgrade.UpgradeLevel; i++)
            {
                var crate = _assets.Instantiate(AssetPath.StorageCrate, _cratePoints[0].transform.position);
                _cratePoints.RemoveAt(0);
                var loader = CreateLoader(crate.GetComponent<StorageCrate>().loaderPlace.gameObject);
                loaderCrates.Add(crate);

                List<ChefTable> table = new List<ChefTable>();
                foreach (var chef in chefTables)
                {
                    table.Add(chef.GetComponent<ChefTable>());
                }

                crate.GetComponent<StorageCrate>().Construct(table, loader.GetComponent<LoaderBehaviour>());
            }
        }

        public void UpgradeLoaders()
        {
            var crate = _assets.Instantiate(AssetPath.StorageCrate, _cratePoints[0].transform.position);
            _cratePoints.RemoveAt(0);
            var loader = CreateLoader(crate.GetComponent<StorageCrate>().loaderPlace.gameObject);
            loaderCrates.Add(crate);

            List<ChefTable> table = new List<ChefTable>();
            foreach (var chef in chefTables)
            {
                table.Add(chef.GetComponent<ChefTable>());
            }

            crate.GetComponent<StorageCrate>().Construct(table, loader.GetComponent<LoaderBehaviour>());
            UpdateNavMesh();
        }

        public void CreateOrderPlace()
        {
            for (int i = 0; i < _persistentProgressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel; i++)
            {
                var orderPlace = _assets.Instantiate(AssetPath.OrderPlace, _orderPoints[0].transform.position);
                _orderPoints.RemoveAt(0);
                var cashier = CreateCashier(orderPlace.GetComponent<OrderPlace>().cashierPlace.gameObject);
                cashierPoints.Add(orderPlace);

                List<ChefTable> table = new List<ChefTable>();
                foreach (var chef in chefTables)
                {
                    table.Add(chef.GetComponent<ChefTable>());
                }

                orderPlace.GetComponent<OrderPlace>().Construct(table, cashier.GetComponent<CashierBehaviour>(), _moneyService);
            }
        }

        public void UpgradeCashiers()
        {
            var orderPlace = _assets.Instantiate(AssetPath.OrderPlace, _orderPoints[0].transform.position);
            _orderPoints.RemoveAt(0);
            var cashier = CreateCashier(orderPlace.GetComponent<OrderPlace>().cashierPlace.gameObject);
            cashierPoints.Add(orderPlace);

            List<ChefTable> table = new List<ChefTable>();
            foreach (var chef in chefTables)
            {
                table.Add(chef.GetComponent<ChefTable>());
            }

            orderPlace.GetComponent<OrderPlace>().Construct(table, cashier.GetComponent<CashierBehaviour>(), _moneyService);
            UpdateNavMesh();
        }

        public void UpgradeBurgers()
        {
            foreach (var burger in cashierPoints)
            {
                burger.GetComponent<OrderPlace>().BurgerPrice = (int)(burger.GetComponent<OrderPlace>().BurgerPrice * BurgerUpgradeCost); 
            }
        }

        public GameObject CreateRandomCustomer()
        {
            var point = customerPoints[Random.Range(0, customerPoints.Count)];
            var customerPrefabPath = AssetPath.Customers[Random.Range(0, AssetPath.Customers.Count)];
            return _assets.Instantiate(customerPrefabPath, point.transform.position);
        }

        

        public void CreateNavMesh()
        {
            navMesh = _assets.Instantiate(AssetPath.NavMeshSurface);
            UpdateNavMesh();
        }

        private void UpdateNavMesh()
        {
            navMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
        }

        public void CreateHud()
        {
            var hud = _assets.Instantiate(AssetPath.HudPath);
            var content = hud.GetComponent<HudVariables>().content;
            CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.chefUpgrade.UpgradeLevel + 1,"Chef", content);
            CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.loaderUpgrade.UpgradeLevel + 1, "Loader", content);
            CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel + 1, "Cashier", content);
            CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel + 1, "Burger", content);
            _assets.Instantiate(AssetPath.SoonItem, content.transform);
            
        }

        public void CreateUpdates()
        {
            _chefUpgrade = new ChefUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _loaderUpgrade = new LoaderUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _cashierUpgrade = new CashierUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _burgerUpgrade = new BurgerCostUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);

        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
        private GameObject CreateChef(GameObject at)
        {
           return _assets.Instantiate(AssetPath.Chef, at.transform.position);
        }
        private GameObject CreateLoader(GameObject at)
        {
            return _assets.Instantiate(AssetPath.Loader, at.transform.position);
        }
        private GameObject CreateCashier(GameObject at)
        {
            return _assets.Instantiate(AssetPath.Cashier, at.transform.position);
        }
        private void RefreshChefs()
        {
            List<ChefTable> table = new List<ChefTable>();
            foreach (var chef in chefTables)
            {
                table.Add(chef.GetComponent<ChefTable>());
            }
            foreach (var loader in loaderCrates)
            {
                loader.GetComponent<StorageCrate>().UpdateChefTables(table);
            }
            foreach (var cashier in cashierPoints)
            {
                cashier.GetComponent<OrderPlace>().UpdateChefTables(table);
            }
        }

        public void CreateUpgradeItem(int upgradeLevel, string type, GameObject parent)
        {
            UpgradeItemConfigurator item = _assets.Instantiate(AssetPath.UpgradeItem, parent.transform).GetComponent<UpgradeItemConfigurator>();

            IUpgradeStaticData staticData = null;

            switch (type)
            {
                case "Cashier":
                    staticData = _staticDataService.ForCashiers(upgradeLevel);
                    item.Construct(staticData, _cashierUpgrade);
                    break;
                case "Chef":
                    staticData = _staticDataService.ForChefs(upgradeLevel);
                    item.Construct(staticData, _chefUpgrade);
                    break;
                case "Loader":
                    staticData = _staticDataService.ForLoaders(upgradeLevel);
                    item.Construct(staticData, _loaderUpgrade);
                    break;
                case "Burger":
                    staticData = _staticDataService.ForBurgers(upgradeLevel);
                    item.Construct(staticData, _burgerUpgrade);
                    break;
            }
        }
    }

}