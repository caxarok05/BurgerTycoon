using Client.Infrastructure.Factory;
using Client.Logic.BonusSystem.BonusUpgrades;
using Client.Services;
using Client.Services.MoneyService;
using Client.Services.StaticData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Logic.BonusSystem
{
    public class BonusPanelController : MonoBehaviour
    {
        public GameObject bonusPanel;
        public TMP_Text bonusNameText;
        public TMP_Text bonusDescriptionText;
        public Image bonusImage;
        public Button useBonusButton;

        public BonusNotification bonusNotification;
        public ActiveBonusPanel activeBonusPanel;

        private List<Bonus> bonuses = new List<Bonus>();
        private float totalWeight;
        private Bonus currentBonus;



        void Start()
        {
            bonuses.Add(new SpeedBonus(
                AllServices.Container.Single<IStaticDataService>(),
                AllServices.Container.Single<IGameFactory>(),
                this,
                activeBonusPanel
            ));

            bonuses.Add(new RandomUpgradeBonus(
                AllServices.Container.Single<IStaticDataService>(),
                AllServices.Container.Single<IGameFactory>(),
                AllServices.Container.Single<IMoneyService>(),
                bonusNotification
            ));

            bonuses.Add(new MoneyBigBonus(
                AllServices.Container.Single<IStaticDataService>(),
                AllServices.Container.Single<IMoneyService>(),
                bonusNotification
            ));

            bonuses.Add(new MoneySmallBonus(
                AllServices.Container.Single<IStaticDataService>(),
                AllServices.Container.Single<IMoneyService>(), 
                bonusNotification
            ));
            bonuses.Add(new GoldenBurger(
                AllServices.Container.Single<IStaticDataService>(),
                AllServices.Container.Single<IGameFactory>(),
                this,
                activeBonusPanel
            ));
            bonuses.Add(new DiamondBurger(
                AllServices.Container.Single<IStaticDataService>(),
                AllServices.Container.Single<IGameFactory>(),
                this,
                activeBonusPanel
            ));
            bonuses.Add(new CustomerMadness(
                AllServices.Container.Single<IStaticDataService>(),
                AllServices.Container.Single<IGameFactory>(),
                this
            ));
            bonuses.Add(new RandomSkinBonus(
                AllServices.Container.Single<IStaticDataService>(),
                AllServices.Container.Single<IGameFactory>(),
                this
            ));
            bonuses.Add(new LootBoxBonus(
                AllServices.Container.Single<IStaticDataService>(),
                AllServices.Container.Single<IGameFactory>(),
                this
            ));

            useBonusButton.onClick.AddListener(UseBonus);

            //Construct из gameFactory с массивом всех бонусов через HudVariables
            StartCoroutine(ShowRandomBonus());
        }

        private IEnumerator ShowRandomBonus()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(2, 3)); // Ждем от 5 до 15 секунд

                Bonus selectedBonus = GetRandomBonus();
                if (selectedBonus != null)
                {
                    DisplayBonus(selectedBonus);
                }
            }
        }

        private Bonus GetRandomBonus()
        {
            totalWeight = 0;
            foreach (var bonus in bonuses)
            {
                totalWeight += 1 / bonus.GetRarityWeight();
            }
            float randomValue = Random.Range(0, totalWeight);
            float cumulativeWeight = 0;

            foreach (var bonus in bonuses)
            {
                cumulativeWeight += 1 / bonus.GetRarityWeight();
                if (randomValue < cumulativeWeight)
                {
                    return bonus;
                }
            }

            return null;
        }

        private void DisplayBonus(Bonus bonus)
        {
            currentBonus = bonus;
            bonusNameText.text = bonus.Name;
            bonusDescriptionText.text = bonus.Description;
            bonusImage.sprite = bonus.Image;
            bonusPanel.SetActive(true);

            StartCoroutine(HideBonusPanel());
        }

        private IEnumerator HideBonusPanel()
        {
            yield return new WaitForSeconds(8); // Показываем бонус на 5 секунд
            bonusPanel.SetActive(false);
        }

        private void UseBonus()
        {
            if (currentBonus != null)
            {
                currentBonus.UseBonus();

                //рэклама

                bonusPanel.SetActive(false);
            }
        }
    }
}