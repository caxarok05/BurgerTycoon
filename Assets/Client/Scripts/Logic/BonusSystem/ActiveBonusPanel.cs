using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Logic.BonusSystem
{
    public class ActiveBonusPanel : MonoBehaviour
    {
        public Image bonusIcon;
        public TMP_Text timerText; 
        public GameObject panel; 

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = panel.GetComponent<CanvasGroup>();
        }

        public void ShowBonus(Sprite iconSprite, float duration)
        {
            bonusIcon.sprite = iconSprite; 
            panel.SetActive(true);
            canvasGroup.alpha = 1;

            StartCoroutine(HandleBonusDuration(duration));
        }

        private IEnumerator HandleBonusDuration(float duration)
        {
            float fadeStartTime = duration - 5f; 
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;

                int remainingTime = Mathf.CeilToInt(duration - time);
                int minutes = remainingTime / 60;
                int seconds = remainingTime % 60;
                timerText.text = $"{minutes}m:{seconds}s";

                if (time >= fadeStartTime)
                {
                    float fadeDuration = 5f;
                    float t = (time - fadeStartTime) / fadeDuration;
                    canvasGroup.alpha = Mathf.Lerp(1, 0, t); 
                }

                yield return null;
            }

            panel.SetActive(false); 
        }
    }
}