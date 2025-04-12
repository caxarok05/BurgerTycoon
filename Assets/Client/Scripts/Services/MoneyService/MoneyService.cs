
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

        public void AddMoney(int amount)
        {
            _persistentProgressService.Progress.moneyAmount += amount;
            if (_persistentProgressService.Progress.moneyAmount > _persistentProgressService.Progress.maxMoneyAmount)
            {
                _persistentProgressService.Progress.maxMoneyAmount = _persistentProgressService.Progress.moneyAmount;
            }
        }

        public void SpendMoney(int amount) => _persistentProgressService.Progress.moneyAmount -= amount;

        public int DisplayMoney() => (int)_persistentProgressService.Progress.moneyAmount;

        public int getMaximumMoney() => (int)_persistentProgressService.Progress.maxMoneyAmount;
    }
}
