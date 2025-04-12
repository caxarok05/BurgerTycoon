using Client.Infrastructure.Factory;
using Client.Services.StaticData;
using UnityEngine;

namespace Client.Logic.BonusSystem.BonusUpgrades
{
    public class LootBoxBonus : Bonus
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly MonoBehaviour _coroutineHandler;
        public LootBoxBonus(IStaticDataService staticDataService, IGameFactory gameFactory, MonoBehaviour coroutineHandler)
        {
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
            _coroutineHandler = coroutineHandler;

            var data = staticDataService.ForBonus(BonusType.LootBox);
            Construct(data.title, data.description, data.icon, data.rarity);
        }

        public override void UseBonus()
        {

        }


    }
}