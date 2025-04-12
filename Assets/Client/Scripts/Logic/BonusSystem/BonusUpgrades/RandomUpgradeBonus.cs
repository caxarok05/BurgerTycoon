using Client.Infrastructure.Factory;
using Client.Logic.EntityUpgrade;
using Client.Services.MoneyService;
using Client.Services.StaticData;
using Client.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Logic.BonusSystem.BonusUpgrades
{
    public class RandomUpgradeBonus : Bonus
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly IMoneyService _moneyService;
        private readonly BonusNotification _bonusNotificaton;

        public RandomUpgradeBonus(IStaticDataService staticDataService, IGameFactory gameFactory, IMoneyService moneyService, BonusNotification bonusNotification)
        {
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
            _moneyService = moneyService;
            _bonusNotificaton = bonusNotification;

            var data = staticDataService.ForBonus(BonusType.RandomUpgrade);
            Construct(data.title, data.description, data.icon, data.rarity);
        }

        public override void UseBonus()
        {
            UpgradeItemConfigurator upgrade = GetRandomUpgradable(_gameFactory.Upgrades);
            _moneyService.AddMoney(upgrade.GetUpgrade().GetUpgradeCost());
            upgrade.BuyUpgrade();

            var data = _staticDataService.ForBonus(BonusType.RandomUpgrade);
            _bonusNotificaton.Notify($"You get {upgrade.Title} upgrade", data.rarity);
        }

        public UpgradeItemConfigurator GetRandomUpgradable(List<UpgradeItemConfigurator> Upgrades)
        {
            if (Upgrades.Count == 0)
                return null; 

            UpgradeItemConfigurator selectedUpgrade;
            int attempts = 0; 

            do
            {
               
                int randomIndex = Random.Range(0, Upgrades.Count);
                selectedUpgrade = Upgrades[randomIndex];
                attempts++;

                
                if (attempts > Upgrades.Count)
                    return null; 

            } while (selectedUpgrade.GetUpgrade().ReturnRealLevel() >= selectedUpgrade.GetUpgrade().ReturnMaxLevel());

            return selectedUpgrade; 
        }
    }
}