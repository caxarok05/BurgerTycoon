using Client.Services.MoneyService;
using Client.Services.StaticData;

namespace Client.Logic.BonusSystem.BonusUpgrades
{
    public class MoneySmallBonus : Bonus
    {
        private int _moneyBonus;
        private const float _moneyСoefficient = 0.1f;
        private readonly IStaticDataService _staticDataService;
        private readonly IMoneyService _moneyService;
        private readonly BonusNotification _bonusNotification;

        public MoneySmallBonus(IStaticDataService staticDataService, IMoneyService moneyService, BonusNotification bonusNotification)
        {
            _staticDataService = staticDataService;
            _moneyService = moneyService;
            _bonusNotification = bonusNotification;

            var data = staticDataService.ForBonus(BonusType.MoneyRewardSmall);
            Construct(data.title, data.description, data.icon, data.rarity);
        }

        public override void UseBonus()
        {
            int currentMoney = _moneyService.DisplayMoney();
            int maxMoney = _moneyService.getMaximumMoney();

            float averageMoney = (currentMoney + maxMoney) / 2 * _moneyСoefficient;
            _moneyBonus = (int)averageMoney;
            _moneyService.AddMoney((int)averageMoney);

            var data = _staticDataService.ForBonus(BonusType.MoneyRewardBig);
            _bonusNotification.Notify($"You get {_moneyBonus}$", data.rarity);
        }

        public int GetMoneyBonus() => _moneyBonus;
    }
}