
using Client.Logic.BonusSystem;
using System;
using UnityEngine;

namespace Client.StaticData
{
    [CreateAssetMenu(fileName = "BonusData", menuName = "Static Data/Bonus Data")]
    public class BonusStaticData : ScriptableObject
    {
        public Sprite icon;

        [TextArea()]
        public string title;

        [TextArea()]
        public string description;

        public BonusType bonusType;

        public BonusRarity rarity;
    }  
}
