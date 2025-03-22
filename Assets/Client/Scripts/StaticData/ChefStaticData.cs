using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Client.StaticData
{
    [CreateAssetMenu(fileName = "ChefData", menuName = "Static Data/Chef")]
    public class ChefStaticData : ScriptableObject, IUpgradeStaticData
    {
        [SerializeField] private Sprite icon;

        [TextArea()]
        [SerializeField] private string title;

        [TextArea()]
        [SerializeField] private string description;

        [Range(1, 8)]
        [SerializeField] private int upgradeLevel;

        [Range(1, 8)]
        public int chefAmount;

        [SerializeField] private int сost;

        private const int maxLevel = 8;

        public Sprite Icon => icon;
        public string Title => title;
        public string Description => description;
        public int Cost => сost;
        public int UpgradeLevel => upgradeLevel;
        public int MaxLevel => maxLevel;
    }
}
