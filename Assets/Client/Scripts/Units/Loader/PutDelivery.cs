using Client.Units.Chef;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Units.Loader
{
    public class PutDelivery : MonoBehaviour
    {

        public float oneTimeIngridentsDeliver;
        public float deliveryTime;
        public Image deliveryProgressImage;
        public Image deliveryProgressObject;

        public async Task Deliver(ChefTable table)
        {
            deliveryProgressObject.gameObject.SetActive(true);
            UpdateDeliveryProgress(0f);
            await DeliveryTimePassage();

            if (table._ingridientsAmount < table._maxIngridients)
                table._ingridientsAmount += oneTimeIngridentsDeliver;

            if (table._ingridientsAmount > table._maxIngridients)
                table._ingridientsAmount = table._maxIngridients;

            UpdateDeliveryProgress(1f);
            deliveryProgressObject.gameObject.SetActive(false);
        }

        public async Task TakeDelivery(StorageCrate crate, WalkableAnimator animator)
        {
            while (crate.currentIngridients < oneTimeIngridentsDeliver)
            {
                animator.SetIdle();
                await Task.Yield();
            }
            animator.StartWorking();

            deliveryProgressObject.gameObject.SetActive(true);
            UpdateDeliveryProgress(0f);
            await DeliveryTimePassage();
            if (crate.currentIngridients >= oneTimeIngridentsDeliver)
                crate.currentIngridients -= oneTimeIngridentsDeliver;

            UpdateDeliveryProgress(1f);
            deliveryProgressObject.gameObject.SetActive(false);
        }

        private async Task DeliveryTimePassage()
        {
            for (float elapsed = 0; elapsed < deliveryTime; elapsed += Time.deltaTime)
            {
                UpdateDeliveryProgress(elapsed / deliveryTime);
                await Task.Yield(); 
            }
        }

        private void UpdateDeliveryProgress(float progress)
        {
            if (deliveryProgressImage != null)
            {
                deliveryProgressImage.fillAmount = progress;
            }
        }
    }
}