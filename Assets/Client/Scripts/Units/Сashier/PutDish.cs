using Client.Units.Chef;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Units.Cashier
{
    public class PutDish : MonoBehaviour
    {
        public float takingTime;
        public float givingTime;

        public float oneTimeDishTaken;
        public Image deliveryProgressImage;
        public Image deliveryProgressObject;

        public void UpgradeServingTime(float amount)
        {
            takingTime = amount;
            givingTime = amount;
        }
        public async Task GiveOrder()
        {

            float elapsedTime = 0f;
            UpdateDeliveryProgress(0f);
            deliveryProgressObject.gameObject.SetActive(true);

            while (elapsedTime < givingTime)
            {
                elapsedTime += Time.deltaTime;
                float progress = Mathf.Clamp(elapsedTime / givingTime, 0f, 1f);
                UpdateDeliveryProgress(progress);
                await Task.Yield();
            }

            UpdateDeliveryProgress(1f);
            deliveryProgressObject.gameObject.SetActive(false);


        }

        public async Task TakeDish(ChefTable table, WalkableAnimator animator)
        {
            while (table._cookedDishes < oneTimeDishTaken)
            {    
                animator.SetIdle();
                await Task.Yield();
            }
            animator.StartWorking();
            deliveryProgressObject.gameObject.SetActive(true);
            UpdateDeliveryProgress(0f);
            float elapsedTime = 0f;

            while (elapsedTime < takingTime)
            {
                elapsedTime += Time.deltaTime;
                float progress = Mathf.Clamp(elapsedTime / takingTime, 0f, 1f);
                UpdateDeliveryProgress(progress);
                await Task.Yield(); 
            }

            if (table._cookedDishes >= oneTimeDishTaken)
                table._cookedDishes -= oneTimeDishTaken;

            UpdateDeliveryProgress(1f);
            deliveryProgressObject.gameObject.SetActive(false);
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