
using Client.Services.PersistentProgress;
using UnityEngine;

namespace Client.Services.MoneyService
{
    public class MoneyService : IMoneyService
    {
        private IPersistentProgressService _persistentProgressService;

        public MoneyService(IPersistentProgressService progressService)
        {
            _persistentProgressService = progressService;
        }

        public void AddMoney(int amount) => _persistentProgressService.Progress.moneyAmount += amount;

        public void SpendMoney(int amount) => _persistentProgressService.Progress.moneyAmount -= amount;

        public int DisplayMoney() => (int)_persistentProgressService.Progress.moneyAmount;
    }
}
