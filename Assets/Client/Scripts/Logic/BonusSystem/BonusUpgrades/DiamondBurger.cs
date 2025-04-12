using Client.Infrastructure.Factory;
using Client.Services.StaticData;
using System.Collections;
using UnityEngine;

namespace Client.Logic.BonusSystem.BonusUpgrades
{
    public class DiamondBurger : Bonus
    {
        private const float _burgerBonus = 3;
        private const float _timeDuration = 30;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly MonoBehaviour _coroutineHandler;
        private readonly ActiveBonusPanel _activeBonus;

        public DiamondBurger(IStaticDataService staticDataService, IGameFactory gameFactory, MonoBehaviour coroutineHandler, ActiveBonusPanel activeBonusPanel)
        {
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
            _coroutineHandler = coroutineHandler;

            var data = staticDataService.ForBonus(BonusType.DiamondBurger);
            Construct(data.title, data.description, data.icon, data.rarity);
        }

        public override void UseBonus()
        {
            _gameFactory.BonusSpeed(_burgerBonus);
            var data = _staticDataService.ForBonus(BonusType.DiamondBurger);
            _activeBonus.ShowBonus(data.icon, _timeDuration);

            _coroutineHandler.StartCoroutine(ResetBonusAfterDuration());
        }

        private IEnumerator ResetBonusAfterDuration()
        {
            yield return new WaitForSeconds(_timeDuration);
            _gameFactory.BonusSpeed(1 / _burgerBonus);
        }


    }
}