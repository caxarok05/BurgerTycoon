using System;
using UnityEngine;

namespace Client.StaticData
{
    [CreateAssetMenu(fileName = "LoaderData", menuName = "Static Data/Loader")]
    public class LoaderStaticData : ScriptableObject, IUpgradeStaticData
    {
        [SerializeField] private Sprite icon;

        [TextArea()]
        [SerializeField] private string title;

        [TextArea()]
        [SerializeField] private string description;

        [Range(1, 6)]
        [SerializeField] private int upgradeLevel;

        [Range(1, 6)]
        public int loaderAmount;

        [SerializeField] private int cost;

        private const int maxLevel = 6;

        public Sprite Icon => icon;
        public string Title => title;
        public string Description => description;
        public int Cost => cost;
        public int UpgradeLevel => upgradeLevel;
        public int MaxLevel => maxLevel;
    }
}
