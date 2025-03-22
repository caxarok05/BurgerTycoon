using TMPro;
using UnityEngine;

namespace Client.Units.Loader
{
    public class LoaderWorkDisplayer : MonoBehaviour
    {
        [SerializeField] private StorageCrate storageCrate;
        [SerializeField] private TMP_Text ingridientsText;

        private void Update()
        {
            ShowText();
        }
        public void ShowText()
        {
            ingridientsText.text = $"{storageCrate.currentIngridients.ToString()}/{storageCrate._maxIngridients.ToString()}";
        }

    }
}