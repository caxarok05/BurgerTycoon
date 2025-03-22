using Client.Services;
using Client.Services.MoneyService;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Client.Logic
{
    public class MoneyDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        private IMoneyService moneyService;

        private void Start()
        {
            moneyService = AllServices.Container.Single<IMoneyService>();
        }
        private void Update()
        {
            moneyText.text = moneyService.DisplayMoney().ToString();
        }
    }
}