using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Client.Units.Chef
{
    public class ChefWorkDisplayer : MonoBehaviour
    {
        [SerializeField] private ChefTable chefTable;
        [SerializeField] private TMP_Text dishesText;
        [SerializeField] private TMP_Text ingridientsText;

        private void Update()
        {
            ShowText();
        }
        public void ShowText()
        {
            dishesText.text = $"{chefTable._cookedDishes.ToString()}/{chefTable._maxCooked.ToString()}";
            ingridientsText.text = $"{chefTable._ingridientsAmount.ToString()}/{chefTable._maxIngridients.ToString()}";
        }

    }
}
