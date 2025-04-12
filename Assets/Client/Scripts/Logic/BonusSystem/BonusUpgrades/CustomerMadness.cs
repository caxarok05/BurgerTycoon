using Client.Infrastructure.Factory;
using Client.Services.StaticData;
using Client.Units;
using System.Collections;
using UnityEngine;

namespace Client.Logic.BonusSystem.BonusUpgrades
{
    public class CustomerMadness : Bonus
    {
        private const float _timeDuration = 30;
        private const float _speedMultiplier = 2f;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly MonoBehaviour _coroutineHandler;
        public CustomerMadness(IStaticDataService staticDataService, IGameFactory gameFactory, MonoBehaviour coroutineHandler)
        {
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
            _coroutineHandler = coroutineHandler;

            var data = staticDataService.ForBonus(BonusType.CustomerMadness);
            Construct(data.title, data.description, data.icon, data.rarity);
        }

        public override void UseBonus()
        {
            _coroutineHandler.StartCoroutine(ResetBonusAfterDuration());

            foreach (var cashier in _gameFactory.cashierPoints)
            {
                cashier.GetComponent<CustomersSpawner>().maxCustomers = (int)(cashier.GetComponent<CustomersSpawner>().maxCustomers * _speedMultiplier);
                cashier.GetComponent<CustomersSpawner>().BonusSpeed(_speedMultiplier);
                cashier.GetComponent<CustomersSpawner>().minTimeDelay /= _speedMultiplier;
                cashier.GetComponent<CustomersSpawner>().maxTimeDelay /= _speedMultiplier;
            }
            foreach (var customer in _gameFactory.customers)
            {
                customer.GetComponent<Unit>().agent.speed *= _speedMultiplier;
            }
        }

        private IEnumerator ResetBonusAfterDuration()
        {
            yield return new WaitForSeconds(_timeDuration);

            foreach (var cashier in _gameFactory.cashierPoints)
            {
                var spawner = cashier.GetComponent<CustomersSpawner>();
                spawner.BonusSpeed(1/_speedMultiplier);
                spawner.maxCustomers = (int)(spawner.maxCustomers / _speedMultiplier);
                spawner.minTimeDelay *= _speedMultiplier;
                spawner.maxTimeDelay *= _speedMultiplier;
            }
            foreach (var customer in _gameFactory.customers)
            {
                customer.GetComponent<Unit>().agent.speed /= _speedMultiplier;
            }
        }
    }
}