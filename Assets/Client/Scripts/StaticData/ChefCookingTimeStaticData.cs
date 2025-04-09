using System;
using UnityEngine;

namespace Client.StaticData
{
    [CreateAssetMenu(fileName = "ChefCookingTimeData", menuName = "Static Data/ChefCookingTime")]
    public class ChefCookingTimeStaticData : ScriptableObject, IUpgradeStaticData
    {
        [SerializeField] private Sprite icon;

        [TextArea()]
        [SerializeField] private string title;

        [TextArea()]
        [SerializeField] private string description;

        [Range(0, 3)]
        [SerializeField] private int upgradeLevel;

        [Range(0, 10)]
        public int cookingTime;

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
