using System.Collections.Generic;
using UnityEngine;

namespace Client.Logic.BonusSystem
{
    public class BonusSystem
    {
        private List<Bonus> bonuses = new List<Bonus>();
        private float totalWeight;

        public void AddBonus(Bonus bonus)
        {
            bonuses.Add(bonus);
            RecalculateWeights();
        }

        private void RecalculateWeights()
        {
            totalWeight = 0;
            foreach (var bonus in bonuses)
            {
                totalWeight += 1 / bonus.GetRarityWeight();
            }
        }

        public Bonus GetRandomBonus()
        {
            float randomValue = Random.Range(0, totalWeight);
            float cumulativeWeight = 0;

            foreach (var bonus in bonuses)
            {
                cumulativeWeight += 1 / bonus.GetRarityWeight();
                if (randomValue < cumulativeWeight)
                {
                    return bonus;
                }
            }

            return null;
        }
    }
}