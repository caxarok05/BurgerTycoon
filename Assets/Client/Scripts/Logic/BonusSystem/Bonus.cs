using Unity.VisualScripting;
using UnityEngine;

namespace Client.Logic.BonusSystem
{
    public class Bonus
    {
        public string Name;
        public string Description;
        public Sprite Image;
        public BonusRarity Rarity;

        public void Construct(string name, string description, Sprite image, BonusRarity rarity)
        {
            Name = name;
            Description = description;
            Image = image;
            Rarity = rarity;
        }

        public float GetRarityWeight()
        {
            switch (Rarity)
            {
                case BonusRarity.Usual:
                    return 1.0f; 
                case BonusRarity.Rare:
                    return 5.0f; 
                case BonusRarity.Legendary:
                    return 30.0f; 
                default:
                    return float.MaxValue; 
            }
        }

        public virtual void UseBonus()
        {

        }
    }
}