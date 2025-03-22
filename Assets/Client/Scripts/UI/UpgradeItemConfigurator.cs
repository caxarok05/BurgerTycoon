using Client.Logic.EntityUpgrade;
using Client.StaticData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class UpgradeItemConfigurator : MonoBehaviour
    {
        public Image Icon;
        public TMP_Text Title;
        public TMP_Text Description;
        public TMP_Text Cost;

        public Slider upgradeLevelSlider;
        public Button upgradeButton;

        private IUpgradeStaticData _staticData;
        private IUpgradable _upgradable;

        public void Construct(IUpgradeStaticData staticData, IUpgradable upgradable)
        {
            _staticData = staticData;
            _upgradable = upgradable;

            SetData(_staticData);

        }

        private void SetData(IUpgradeStaticData staticData)
        {
            upgradeLevelSlider.maxValue = staticData.MaxLevel;
            upgradeLevelSlider.value = _upgradable.ReturnRealLevel();
            if (upgradeLevelSlider.value == upgradeLevelSlider.maxValue)
            {
                upgradeButton.interactable = false;
                Cost.text = "MAX";
            }
            else
            {
                Cost.text = staticData.Cost.ToString();
            }
            Icon.sprite = staticData.Icon;
            Title.text = staticData.Title;
            Description.text = staticData.Description;

        }

        public void BuyUpgrade()
        {
            if (_upgradable.CanUpgrade())
            {
                _upgradable.Upgrade();
                SetData(_upgradable.GetNewData());
            }

        }
    }
}