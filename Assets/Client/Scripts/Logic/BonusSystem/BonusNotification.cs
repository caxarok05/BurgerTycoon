using System.Collections;
using TMPro;
using UnityEngine;

namespace Client.Logic.BonusSystem
{
    public class BonusNotification : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void Notify(string text, BonusRarity rarity)
        {
            _text.gameObject.SetActive(true);
            _text.text = text;

            
            switch (rarity)
            {
                case BonusRarity.None:
                case BonusRarity.Usual:
                    _text.color = Color.white;
                    break;
                case BonusRarity.Rare:
                    _text.color = Color.yellow;
                    break;
                case BonusRarity.Legendary:
                    _text.color = Color.blue;
                    break;
                default:
                    break;
            }

            StartCoroutine(MoveDurationText());
        }

        private IEnumerator MoveDurationText()
        {
            Vector3 initialPosition = _text.transform.localPosition;
            Vector3 targetPosition = initialPosition + new Vector3(0, 50, 0);
            float initialAlpha = _text.color.a;
            float time = 0;
            float moveDuration = 1f;

            while (time < moveDuration)
            {
                time += Time.deltaTime;
                float t = time / moveDuration;
                
                _text.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, t);

                Color newColor = _text.color;
                newColor.a = Mathf.Lerp(initialAlpha, 0, t);
                _text.color = newColor;

                yield return null;
            }

            _text.gameObject.SetActive(false);
        }
    }
}