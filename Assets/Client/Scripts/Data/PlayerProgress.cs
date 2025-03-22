using Client.Units.Chef;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public float moneyAmount;
        public UpgradeData upgradeData;

        public PlayerProgress()
        {
            upgradeData = new UpgradeData();
        }
    }

}