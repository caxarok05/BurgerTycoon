using Client.Infrastructure.Factory;
using Client.Services.StaticData;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Client.Logic.BonusSystem.BonusUpgrades
{
    public class SpeedBonus : Bonus
    {
        private const float _speedMultiplier = 1.2f;
        private const float _timeDuration = 60;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly MonoBehaviour _coroutineHandler;
        private readonly ActiveBonusPanel _activeBonusPanel;
        public SpeedBonus(
            IStaticDataService staticDataService, 
            IGameFactory gameFactory, 
            MonoBehaviour coroutineHandler, 
            ActiveBonusPanel activeBonusPanel)
        {
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
            _coroutineHandler = coroutineHandler;
            _activeBonusPanel = activeBonusPanel;

            var data = staticDataService.ForBonus(BonusType.SpeedBonus);
            Construct(data.title, data.description, data.icon, data.rarity);
        }

        public override void UseBonus()
        {
            _gameFactory.BonusSpeed(_speedMultiplier);

            var data = _staticDataService.ForBonus(BonusType.SpeedBonus);
            _activeBonusPanel.ShowBonus(data.icon, _timeDuration);

            _coroutineHandler.StartCoroutine(ResetSpeedAfterDuration());
        }

        private IEnumerator ResetSpeedAfterDuration()
        {
            yield return new WaitForSeconds(_timeDuration);
            _gameFactory.BonusSpeed(1 / _speedMultiplier);
        }
    }
}