using System;
using UnityEngine;

namespace Client.StaticData
{
    [CreateAssetMenu(fileName = "CashierServingTimeData", menuName = "Static Data/CashierServingTime")]
    public class CashierServingTimeStaticData : ScriptableObject, IUpgradeStaticData
    {
        [SerializeField] private Sprite icon;

        [TextArea()]
        [SerializeField] private string title;

        [TextArea()]
        [SerializeField] private string description;

        [Range(0, 3)]
        [SerializeField] private int upgradeLevel;

        [Range(0, 5)]
        public int servingTime;

        [SerializeField] private int сost;

        private const int maxLevel = 3;

        public Sprite Icon => icon;
        public string Title => title;
        public string Description => description;
        public int Cost => сost;
        public int UpgradeLevel => upgradeLevel;
        public int MaxLevel => maxLevel;
    }

}
