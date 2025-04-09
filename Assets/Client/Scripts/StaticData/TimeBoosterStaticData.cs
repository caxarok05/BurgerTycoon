using System;
using UnityEngine;

namespace Client.StaticData
{
    [CreateAssetMenu(fileName = "TimeBoosterData", menuName = "Static Data/Time Booster")]
    public class TimeBoosterStaticData : ScriptableObject, IUpgradeStaticData
    {
        [SerializeField] private Sprite icon;

        [TextArea()]
        [SerializeField] private string title;

        [TextArea()]
        [SerializeField] private string description;

        [Range(0, 1)]
        [SerializeField] private int upgradeLevel;

        [Range(0, 1)]
        public int priceUpAmount;

        [SerializeField] private int сost;

        private const int maxLevel = 1;

        public Sprite Icon => icon;
        public string Title => title;
        public string Description => description;
        public int Cost => сost;
        public int UpgradeLevel => upgradeLevel;
        public int MaxLevel => maxLevel;
    }
}
