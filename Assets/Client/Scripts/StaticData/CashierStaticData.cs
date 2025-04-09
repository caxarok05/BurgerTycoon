using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Client.StaticData
{
    [CreateAssetMenu(fileName = "CashierData", menuName = "Static Data/Cashier")]
    public class CashierStaticData : ScriptableObject, IUpgradeStaticData
    {
        [SerializeField]private Sprite icon;

        [TextArea()]
        [SerializeField]private string title;

        [TextArea()]
        [SerializeField]private string description;

        [Range(1, 4)]
        [SerializeField] private int upgradeLevel;

        [Range(1, 4)]
        public int cashierAmount;

        [SerializeField] private int сost;

        private const int maxLevel = 4;

        public Sprite Icon => icon;
        public string Title => title;
        public string Description => description;
        public int Cost => сost;
        public int UpgradeLevel => upgradeLevel;
        public int MaxLevel => maxLevel;
    }

}
