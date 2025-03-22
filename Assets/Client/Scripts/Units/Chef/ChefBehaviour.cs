using Client.Units;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Units.Chef
{
    public class ChefBehaviour : Unit
    {
        public ChefAnimator animator;
        public bool isFree = true;
        public float cookingTime;
        public Image cookingProgress;
        public Image cookingProgressObject;

        public float ingridientsRequired { get; private set; } = 1;
        public float oneTimeDishesCooked { get; private set; } = 1;

        public async Task<float> StartCooking()
        {
            isFree = false;
            animator.StartWorking();
            cookingProgressObject.gameObject.SetActive(true);
            UpdateCookingProgress(0f);
            await CookAsync(cookingTime);
            return oneTimeDishesCooked;
        }

        public bool IsEnoughIngridients(float amount)
        {
            if (amount >= ingridientsRequired)
                return true;
            return false;
        }

        private async Task CookAsync(float time)
        {
            float elapsedTime = 0f;

            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / time;
                UpdateCookingProgress(progress);
                await Task.Yield(); 
            }

            isFree = true;
            animator.SetIdle();
            UpdateCookingProgress(1f);
            cookingProgressObject.gameObject.SetActive(false);
        }

        private void UpdateCookingProgress(float progress)
        {
            if (cookingProgress != null)
            {
                cookingProgress.fillAmount = progress;
            }
        }
    }
}
