using System;
using UnityEngine;

namespace Client.StaticData
{
    [CreateAssetMenu(fileName = "LoaderIngridientsRechargeData", menuName = "Static Data/LoaderIngridientsRecharge")]
    public class LoaderIngridiensRechargeStaticData : ScriptableObject, IUpgradeStaticData
    {
        [SerializeField] private Sprite icon;

        [TextArea()]
        [SerializeField] private string title;

        [TextArea()]
        [SerializeField] private string description;

        [Range(0, 3)]
        [SerializeField] private int upgradeLevel;

        [Range(0, 20)]
        public int rechargeTime;

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
