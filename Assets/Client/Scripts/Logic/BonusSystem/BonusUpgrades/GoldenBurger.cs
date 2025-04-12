using Client.Infrastructure.Factory;
using Client.Services.StaticData;
using System.Collections;
using UnityEngine;

namespace Client.Logic.BonusSystem.BonusUpgrades
{
    public class GoldenBurger : Bonus
    {
        private const float _burgerBonus = 2;
        private const float _timeDuration = 30;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly MonoBehaviour _coroutineHandler;
        private readonly ActiveBonusPanel _activeBonus;
        public GoldenBurger(IStaticDataService staticDataService, IGameFactory gameFactory, MonoBehaviour coroutineHandler, ActiveBonusPanel activeBonusPanel)
        {
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
            _coroutineHandler = coroutineHandler;
            _activeBonus = activeBonusPanel;

            var data = staticDataService.ForBonus(BonusType.GoldBurger);
            Construct(data.title, data.description, data.icon, data.rarity);
        }

        public override void UseBonus()
        {
            _gameFactory.BonusSpeed(_burgerBonus);
            var data = _staticDataService.ForBonus(BonusType.GoldBurger);

            _activeBonus.ShowBonus(data.icon, _timeDuration);
            _coroutineHandler.StartCoroutine(ResetBonusAfterDuration());
        }

        private IEnumerator ResetBonusAfterDuration()
        {
            yield return new WaitForSeconds(_timeDuration);
            _gameFactory.BonusSpeed(1/_burgerBonus);
        }


    }
}