using System.Collections.Generic;
using UnityEngine;

namespace Client.Units.Chef
{
    public class ChefTable : WorkPlace
    {
        public ChefBehaviour chef;

        public float _cookedDishes;
        public float _ingridientsAmount;

        public Transform chefPlace;
        public Transform deliveryPlace;
        public Transform takeOrderPlace;

        public float _maxCooked;
        public float _maxIngridients;

        public bool isOccupiedbyLoader;
        public bool isOccupiedbyCashier;

        private void Update()
        {
            if (chef != null && chef.isFree)
            {
                CookDish();
            }
        }


        public async void CookDish()
        {
            if (chef.IsEnoughIngridients(_ingridientsAmount) && _cookedDishes < _maxCooked)
            {
                _cookedDishes += await chef.StartCooking();
                _ingridientsAmount -= chef.ingridientsRequired;
                if (_cookedDishes > _maxCooked)
                    _cookedDishes = _maxCooked;

            }
        }

        public void AddNewChef(ChefBehaviour chef)
        {
            this.chef = chef;
        }

        public void GetIngridients(float amount)
        {
            if (_ingridientsAmount + amount > _maxIngridients)
            {
                _ingridientsAmount = _maxIngridients;
                return;
            }
            _ingridientsAmount += amount;
        }

        public void UpgradeMaxIngridients(int amount) => _maxIngridients = amount;

        public void UpgradeMaxCooked(int amount) => _maxCooked = amount;

    }
}
