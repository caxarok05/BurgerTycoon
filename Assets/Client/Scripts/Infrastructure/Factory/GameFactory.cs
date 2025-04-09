using Client.Data;
using Client.Infrastructure.AssetManagement;
using Client.Logic.EntityUpgrade;
using Client.Services.MoneyService;
using Client.Services.PersistentProgress;
using Client.Services.StaticData;
using Client.Services.TimeService;
using Client.StaticData;
using Client.UI;
using Client.Units;
using Client.Units.Cashier;
using Client.Units.Chef;
using Client.Units.Loader;
using System.Collections.Generic;
using System.Linq;
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
        public List<GameObject> customers { get; private set; } = new List<GameObject>();

        private readonly IAssetProvider _assets;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IMoneyService _moneyService;
        private readonly ITimeController _timeService;
        public List<Transform> customerPoints { get; private set; } = new List<Transform>();

        private List<Transform> _chefPoints = new List<Transform>();
        private List<Transform> _cratePoints = new List<Transform>();
        private List<Transform> _orderPoints = new List<Transform>();

        private ChefUpgrade _chefUpgrade;
        private LoaderUpgrade _loaderUpgrade;
        private CashierUpgrade _cashierUpgrade;
        private BurgerCostUpgrade _burgerUpgrade;

        private TimeBoostUpgrade _timeUpgrade;

        private CashierSpeedUpgrade _cashierSpeedUpgrade;
        private LoaderSpeedUpgrade _loaderSpeedUpgrade;
        private CustomerSpeedUpgrade _customerSpeedUpgrade;

        private LoaderMaxIngridientsUpgrade _loaderMaxIngridientsUpgrade;
        private ChefMaxIngridientsUpgrade _chefMaxIngridientsUpgrade;
        private ChefMaxDishesUpgrade _chefMaxDishesUpgrade;

        private LoaderOneTimeDeliveryUpgrade _loaderOneTimeDeliverUpgrade;
        private ChefOneTimeCookedUpgrade _chefOneTimeCookedUpgrade;

        private ChefCookingTimeUpgrade _chefCookingTimeUpgrade;
        private CashierServingTimeUpgrade _cashierServingTimeUpgrade;

        private GameObject _navMesh;
        private GameObject _hud;

        private const float BurgerUpgradeCost = 1.1f;

        public GameFactory(IAssetProvider assets, IPersistentProgressService persistentProgress, 
            IMoneyService moneyService, IStaticDataService staticData, ITimeController timeController)
        {
            _assets = assets;
            _persistentProgressService = persistentProgress;
            _moneyService = moneyService;
            _staticDataService = staticData;
            _timeService = timeController;
        }

        public GameObject CreateRestaraunt()
        {
            var restaraunt = _assets.Instantiate(path: AssetPath.Restaraunt);
            _chefPoints = restaraunt.GetComponent<SpawnPointsData>().chefPoints;
            _cratePoints = restaraunt.GetComponent<SpawnPointsData>().loaderPoints;
            _orderPoints = restaraunt.GetComponent<SpawnPointsData>().orderPoints;
            customerPoints = restaraunt.GetComponent<SpawnPointsData>().customerPoints;
            
            _moneyService.AddMoney(10000);

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
                table.GetComponent<ChefTable>().UpgradeMaxIngridients(_staticDataService.ForChefMaxIngridients(_persistentProgressService.Progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel).maxIngridients);
                table.GetComponent<ChefTable>().UpgradeMaxCooked(_staticDataService.ForChefMaxDishes(_persistentProgressService.Progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel).maxDishes);
                table.GetComponent<ChefTable>().chef.UpgradeOneTimeCooked(_staticDataService.ForChefOneTimeCooked(_persistentProgressService.Progress.upgradeData.chefOneTimeCookedUpgrade.UpgradeLevel).oneTimeCooked);
                table.GetComponent<ChefTable>().chef.UpgradeCookingTime(_staticDataService.ForChefCookingTime(_persistentProgressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel).cookingTime);

                chefTables.Add(table);
            }                  
        }

        public void UpgradeChefs()
        {
            var table = _assets.Instantiate(AssetPath.ChefTable, _chefPoints[0].transform.position);
            _chefPoints.RemoveAt(0);
            var chef = CreateChef(table.GetComponent<ChefTable>().chefPlace.gameObject);
            table.GetComponent<ChefTable>().AddNewChef(chef.GetComponent<ChefBehaviour>());
            table.GetComponent<ChefTable>().UpgradeMaxIngridients(_staticDataService.ForChefMaxIngridients(_persistentProgressService.Progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel).maxIngridients);
            table.GetComponent<ChefTable>().UpgradeMaxCooked(_staticDataService.ForChefMaxDishes(_persistentProgressService.Progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel).maxDishes);
            table.GetComponent<ChefTable>().chef.UpgradeOneTimeCooked(_staticDataService.ForChefOneTimeCooked(_persistentProgressService.Progress.upgradeData.chefOneTimeCookedUpgrade.UpgradeLevel).oneTimeCooked);
            table.GetComponent<ChefTable>().chef.UpgradeCookingTime(_staticDataService.ForChefCookingTime(_persistentProgressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel).cookingTime);

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
                crate.GetComponent<StorageCrate>().UpdateloaderSpeed(_staticDataService.ForLoaderSpeed(_persistentProgressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel).loaderSpeed);
                crate.GetComponent<StorageCrate>().UpgradeMaxIngridients(_staticDataService.ForLoaderMaxIngridients(_persistentProgressService.Progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel).maxIngridients);
                crate.GetComponent<StorageCrate>().loader.UpgradeOneTimedeliver(_staticDataService.ForLoaderOneTimeDeliver(_persistentProgressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel).oneTimeDeliver);
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
            crate.GetComponent<StorageCrate>().UpdateloaderSpeed(_staticDataService.ForLoaderSpeed(_persistentProgressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel).loaderSpeed);
            crate.GetComponent<StorageCrate>().UpgradeMaxIngridients(_staticDataService.ForLoaderMaxIngridients(_persistentProgressService.Progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel).maxIngridients);
            crate.GetComponent<StorageCrate>().loader.UpgradeOneTimedeliver(_staticDataService.ForLoaderOneTimeDeliver(_persistentProgressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel).oneTimeDeliver);
            UpdateNavMesh();
        }

        public void CreateOrderPlace()
        {
            for (int i = 0; i < _persistentProgressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel; i++)
            {
                var orderPlace = _assets.Instantiate(AssetPath.OrderPlace, _orderPoints[0].transform.position);

                orderPlace.GetComponent<CustomersSpawner>().ChangeSpeed(_staticDataService.ForCustomerSpeed(_persistentProgressService.Progress.upgradeData.customerSpeedUpgrade.UpgradeLevel).customerSpeed);
                
                _orderPoints.RemoveAt(0);
                var cashier = CreateCashier(orderPlace.GetComponent<OrderPlace>().cashierPlace.gameObject);
                cashierPoints.Add(orderPlace);

                List<ChefTable> table = new List<ChefTable>();
                foreach (var chef in chefTables)
                {
                    table.Add(chef.GetComponent<ChefTable>());
                }

                orderPlace.GetComponent<OrderPlace>().Construct(table, cashier.GetComponent<CashierBehaviour>(), _moneyService);
                orderPlace.GetComponent<OrderPlace>().UpdateCashierSpeed(_staticDataService.ForCashierSpeed(_persistentProgressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel).cashierSpeed);
                orderPlace.GetComponent<OrderPlace>().cashier.putDish.UpgradeServingTime(_staticDataService.ForCashierServingTime(_persistentProgressService.Progress.upgradeData.cashirServingTimeUpgrade.UpgradeLevel).servingTime);
            }
        }

        public void UpgradeCashiers()
        {
            var orderPlace = _assets.Instantiate(AssetPath.OrderPlace, _orderPoints[0].transform.position);

            orderPlace.GetComponent<CustomersSpawner>().ChangeSpeed(_staticDataService.ForCustomerSpeed(_persistentProgressService.Progress.upgradeData.customerSpeedUpgrade.UpgradeLevel).customerSpeed);
            
            _orderPoints.RemoveAt(0);
            var cashier = CreateCashier(orderPlace.GetComponent<OrderPlace>().cashierPlace.gameObject);
            cashierPoints.Add(orderPlace);

            List<ChefTable> table = new List<ChefTable>();
            foreach (var chef in chefTables)
            {
                table.Add(chef.GetComponent<ChefTable>());
            }

            orderPlace.GetComponent<OrderPlace>().Construct(table, cashier.GetComponent<CashierBehaviour>(), _moneyService);
            orderPlace.GetComponent<OrderPlace>().UpdateCashierSpeed(_staticDataService.ForCashierSpeed(_persistentProgressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel).cashierSpeed);
            orderPlace.GetComponent<OrderPlace>().cashier.putDish.UpgradeServingTime(_staticDataService.ForCashierServingTime(_persistentProgressService.Progress.upgradeData.cashirServingTimeUpgrade.UpgradeLevel).servingTime);

            UpdateNavMesh();
        }
        
        public void UpgradeBurgers()
        {
            foreach (var burger in cashierPoints)
            {
                for (int i = 0; i < _persistentProgressService.Progress.upgradeData.burgerUpgrade.UpgradeLevel; i++)
                {
                    burger.GetComponent<OrderPlace>().BurgerPrice = (int)(burger.GetComponent<OrderPlace>().BurgerPrice * BurgerUpgradeCost); 
                }
            }
        }

        public void UpgradeTimeBooster()
        {
            _hud.GetComponent<HudVariables>().toggleBooster.gameObject.SetActive(true);
        }

        public void UpgradeCashierSpeed()
        {
            foreach (var cashier in cashierPoints)
            {
                cashier.GetComponent<OrderPlace>().UpdateCashierSpeed(_staticDataService.ForCashierSpeed(_persistentProgressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel).cashierSpeed);
            }
        }
        public void UpgradeLoaderSpeed()
        {
            foreach (var loader in loaderCrates)
            {
                loader.GetComponent<StorageCrate>().UpdateloaderSpeed(_staticDataService.ForLoaderSpeed(_persistentProgressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel).loaderSpeed);
            }
        }

        public void UpgradeCustomerSpeed()
        {
            foreach (var cashier in cashierPoints)
            {
                cashier.GetComponent<CustomersSpawner>().ChangeSpeed(_staticDataService.ForCustomerSpeed(_persistentProgressService.Progress.upgradeData.customerSpeedUpgrade.UpgradeLevel).customerSpeed);
            }
            foreach (var customer in customers)
            {
                customer.GetComponent<Unit>().agent.speed = _staticDataService.ForCustomerSpeed(_persistentProgressService.Progress.upgradeData.customerSpeedUpgrade.UpgradeLevel).customerSpeed;
            }
        }

        public void UpgradeLoaderMaxIngridients()
        {
            foreach(var loader in loaderCrates)
            {
                loader.GetComponent<StorageCrate>().UpgradeMaxIngridients(_staticDataService.ForLoaderMaxIngridients(_persistentProgressService.Progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel).maxIngridients);
            }
        }

        public void UpgradeChefMaxIngridients()
        {
            foreach( var chef in chefTables)
            {
                chef.GetComponent<ChefTable>().UpgradeMaxIngridients(_staticDataService.ForChefMaxIngridients(_persistentProgressService.Progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel).maxIngridients);
            }
        }

        public void UpgradeChefMaxDishes()
        {
            foreach (var chef in chefTables)
            {
                chef.GetComponent<ChefTable>().UpgradeMaxCooked(_staticDataService.ForChefMaxDishes(_persistentProgressService.Progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel).maxDishes);
            }
        }

        public void UpgradeLoaderOneTimeDeliver()
        {
            foreach (var crate in loaderCrates)
            {
                crate.GetComponent<StorageCrate>().loader.UpgradeOneTimedeliver(_staticDataService.ForLoaderOneTimeDeliver(_persistentProgressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel).oneTimeDeliver);
            }
        }

        public void UpgradeChefOneTimeCooked()
        {
            foreach (var table in chefTables)
            {
                table.GetComponent<ChefTable>().chef.UpgradeOneTimeCooked(_staticDataService.ForChefOneTimeCooked(_persistentProgressService.Progress.upgradeData.chefOneTimeCookedUpgrade.UpgradeLevel).oneTimeCooked);
            }

        }
        public void UpgradeChefCookingTime()
        {
            foreach (var table in chefTables)
            {
                table.GetComponent<ChefTable>().chef.UpgradeCookingTime(_staticDataService.ForChefCookingTime(_persistentProgressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel).cookingTime);
            }
        }
        public void UpgradeCashierServingTime()
        {
            foreach (var point in cashierPoints)
            {
                point.GetComponent<OrderPlace>().cashier.putDish.UpgradeServingTime(_staticDataService.ForCashierServingTime(_persistentProgressService.Progress.upgradeData.cashirServingTimeUpgrade.UpgradeLevel).servingTime);
            }
        }

        public GameObject CreateRandomCustomer()
        {
            var point = customerPoints[Random.Range(0, customerPoints.Count)];
            var customerPrefabPath = AssetPath.Customers[Random.Range(0, AssetPath.Customers.Count)];
            GameObject customer = _assets.Instantiate(customerPrefabPath, point.transform.position);
            customers.Add(customer);
            return customer;
        }

        

        public void CreateNavMesh()
        {
            _navMesh = _assets.Instantiate(AssetPath.NavMeshSurface);
            UpdateNavMesh();
        }

        private void UpdateNavMesh()
        {
            _navMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
        }

        public void CreateHud()
        {
            _hud = _assets.Instantiate(AssetPath.HudPath);
            var content = _hud.GetComponent<HudVariables>().content;

            _hud.GetComponent<HudVariables>().adBooster.Construct(_timeService);
            _hud.GetComponent<HudVariables>().toggleBooster.Construct(_timeService);

            if (_persistentProgressService.Progress.upgradeData.chefUpgrade.UpgradeLevel < ChefUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.chefUpgrade.UpgradeLevel + 1, "Chef", content);
            else
                CreateUpgradeItem(ChefUpgradeData.MaxLevel, "Chef", content);

            if (_persistentProgressService.Progress.upgradeData.loaderUpgrade.UpgradeLevel < LoaderUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.loaderUpgrade.UpgradeLevel + 1, "Loader", content);
            else
                CreateUpgradeItem(LoaderUpgradeData.MaxLevel, "Loader", content);

            if (_persistentProgressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel < CashierUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.cashierUpgrade.UpgradeLevel + 1, "Cashier", content);
            else
                CreateUpgradeItem(CashierUpgradeData.MaxLevel, "Cashier", content);

            if (_persistentProgressService.Progress.upgradeData.burgerUpgrade.UpgradeLevel < BurgerUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.burgerUpgrade.UpgradeLevel + 1, "Burger", content);
            else
                CreateUpgradeItem(BurgerUpgradeData.MaxLevel, "Burger", content);

            if (_persistentProgressService.Progress.upgradeData.timeUpgrade.UpgradeLevel < TimeBoostUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.timeUpgrade.UpgradeLevel + 1, "TimeBooster", content);
            else
                CreateUpgradeItem(TimeBoostUpgradeData.MaxLevel, "TimeBooster", content);

            if(_persistentProgressService.Progress.upgradeData.timeUpgrade.UpgradeLevel >= TimeBoostUpgradeData.MaxLevel)
                UpgradeTimeBooster();


            if (_persistentProgressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel < CashierSpeedUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.cashierSpeedUpgrade.UpgradeLevel + 1, "CashierSpeed", content);
            else
                CreateUpgradeItem(CashierSpeedUpgradeData.MaxLevel, "CashierSpeed", content);

            if (_persistentProgressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel < LoaderSpeedUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.loaderSpeedUpgrade.UpgradeLevel + 1, "LoaderSpeed", content);
            else
                CreateUpgradeItem(LoaderSpeedUpgradeData.MaxLevel, "LoaderSpeed", content);

            if (_persistentProgressService.Progress.upgradeData.customerSpeedUpgrade.UpgradeLevel < CustomerSpeedUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.customerSpeedUpgrade.UpgradeLevel + 1, "CustomerSpeed", content);
            else
                CreateUpgradeItem(CustomerSpeedUpgradeData.MaxLevel, "CustomerSpeed", content);


            if (_persistentProgressService.Progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel < LoaderMaxIngridientsUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.loaderMaxIngridientsUpgrade.UpgradeLevel + 1, "LoaderMaxIngridients", content);
            else
                CreateUpgradeItem(LoaderMaxIngridientsUpgradeData.MaxLevel, "LoaderMaxIngridients", content);

            if (_persistentProgressService.Progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel < ChefMaxIngridientsUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.chefMaxIngridientsUpgrade.UpgradeLevel + 1, "ChefMaxIngridients", content);
            else
                CreateUpgradeItem(ChefMaxIngridientsUpgradeData.MaxLevel, "ChefMaxIngridients", content);

            if (_persistentProgressService.Progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel < ChefMaxDishesUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.chefMaxDishesUpgrade.UpgradeLevel + 1, "ChefMaxDishes", content);
            else
                CreateUpgradeItem(ChefMaxDishesUpgradeData.MaxLevel, "ChefMaxDishes", content);


            if (_persistentProgressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel < LoaderOneTimeDeliverUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.LoaderOneTimeDeliverUpgrade.UpgradeLevel + 1, "LoaderOneTimeDeliver", content);
            else
                CreateUpgradeItem(LoaderOneTimeDeliverUpgradeData.MaxLevel, "LoaderOneTimeDeliver", content);

            if (_persistentProgressService.Progress.upgradeData.chefOneTimeCookedUpgrade.UpgradeLevel < ChefOneTimeCookingUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.chefOneTimeCookedUpgrade.UpgradeLevel + 1, "ChefOneTimeCooked", content);
            else
                CreateUpgradeItem(ChefOneTimeCookingUpgradeData.MaxLevel, "ChefOneTimeCooked", content);


            if (_persistentProgressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel < ChefCookingTimeUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.chefCookingTimeUpgrade.UpgradeLevel + 1, "ChefCookingTime", content);
            else
                CreateUpgradeItem(ChefCookingTimeUpgradeData.MaxLevel, "ChefCookingTime", content);

            if (_persistentProgressService.Progress.upgradeData.cashirServingTimeUpgrade.UpgradeLevel < CashierServingTimeUpgradeData.MaxLevel)
                CreateUpgradeItem(_persistentProgressService.Progress.upgradeData.cashirServingTimeUpgrade.UpgradeLevel + 1, "CashierServingTime", content);
            else
                CreateUpgradeItem(CashierServingTimeUpgradeData.MaxLevel, "CashierServingTime", content);

            _assets.Instantiate(AssetPath.SoonItem, content.transform);
            
        }

        public void CreateUpdates()
        {
            _chefUpgrade = new ChefUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _loaderUpgrade = new LoaderUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _cashierUpgrade = new CashierUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _burgerUpgrade = new BurgerCostUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _timeUpgrade = new TimeBoostUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _cashierSpeedUpgrade = new CashierSpeedUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _loaderSpeedUpgrade = new LoaderSpeedUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _customerSpeedUpgrade = new CustomerSpeedUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _loaderMaxIngridientsUpgrade = new LoaderMaxIngridientsUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _chefMaxIngridientsUpgrade = new ChefMaxIngridientsUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _chefMaxDishesUpgrade = new ChefMaxDishesUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _loaderOneTimeDeliverUpgrade = new LoaderOneTimeDeliveryUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _chefOneTimeCookedUpgrade = new ChefOneTimeCookedUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _chefCookingTimeUpgrade = new ChefCookingTimeUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);
            _cashierServingTimeUpgrade = new CashierServingTimeUpgrade(this, _persistentProgressService, _staticDataService, _moneyService);

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
                case "TimeBooster":
                    staticData = _staticDataService.ForTimeBooster(upgradeLevel);
                    item.Construct(staticData, _timeUpgrade);
                    break;
                case "CashierSpeed":
                    staticData = _staticDataService.ForCashierSpeed(upgradeLevel);
                    item.Construct(staticData, _cashierSpeedUpgrade);
                    break;
                case "LoaderSpeed":
                    staticData = _staticDataService.ForLoaderSpeed(upgradeLevel);
                    item.Construct(staticData, _loaderSpeedUpgrade);
                    break;
                case "CustomerSpeed":
                    staticData = _staticDataService.ForCustomerSpeed(upgradeLevel);
                    item.Construct(staticData, _customerSpeedUpgrade);
                    break;
                case "LoaderMaxIngridients":
                    staticData = _staticDataService.ForLoaderMaxIngridients(upgradeLevel);
                    item.Construct(staticData, _loaderMaxIngridientsUpgrade);
                    break;
                case "ChefMaxIngridients":
                    staticData = _staticDataService.ForChefMaxIngridients(upgradeLevel);
                    item.Construct(staticData, _chefMaxIngridientsUpgrade);
                    break;
                case "ChefMaxDishes":
                    staticData = _staticDataService.ForChefMaxDishes(upgradeLevel);
                    item.Construct(staticData, _chefMaxDishesUpgrade);
                    break;
                case "LoaderOneTimeDeliver":
                    staticData = _staticDataService.ForLoaderOneTimeDeliver(upgradeLevel);
                    item.Construct(staticData, _loaderOneTimeDeliverUpgrade);
                    break;
                case "ChefOneTimeCooked":
                    staticData = _staticDataService.ForChefOneTimeCooked(upgradeLevel);
                    item.Construct(staticData, _chefOneTimeCookedUpgrade);
                    break;
                case "ChefCookingTime":
                    staticData = _staticDataService.ForChefCookingTime(upgradeLevel);
                    item.Construct(staticData, _chefCookingTimeUpgrade);
                    break;
                case "CashierServingTime":
                    staticData = _staticDataService.ForCashierServingTime(upgradeLevel);
                    item.Construct(staticData, _cashierServingTimeUpgrade);
                    break;
            }
        }
    }

}