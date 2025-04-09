using System;
using UnityEngine;

namespace Client.StaticData
{
    [CreateAssetMenu(fileName = "LoaderOneTimeDeliverData", menuName = "Static Data/LoaderOneTimeDeliver")]
    public class LoaderOneTimeDeliverStaticData : ScriptableObject, IUpgradeStaticData
    {
        [SerializeField] private Sprite icon;

        [TextArea()]
        [SerializeField] private string title;

        [TextArea()]
        [SerializeField] private string description;

        [Range(0, 2)]
        [SerializeField] private int upgradeLevel;

        [Range(0, 5)]
        public int oneTimeDeliver;

        [SerializeField] private int сost;

        private const int maxLevel = 2;

        public Sprite Icon => icon;
        public string Title => title;
        public string Description => description;
        public int Cost => сost;
        public int UpgradeLevel => upgradeLevel;
        public int MaxLevel => maxLevel;
    }
}
