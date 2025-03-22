using Client.Services;
using Client.Services.SaveLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class SaveTrigger : MonoBehaviour
    {
        public float saveInterval = 300f;
        public Image saveNotificationImage;

        private float _timeSinceLastSave = 0f;
        private bool _isAutoSaveEnabled = true;
        private CanvasGroup _canvasGroup;

        private ISaveLoadService _saveLoadService;


        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _canvasGroup = saveNotificationImage.GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (_isAutoSaveEnabled)
            {
                _timeSinceLastSave += Time.deltaTime;

                if (_timeSinceLastSave >= saveInterval)
                {
                    Save();
                    _timeSinceLastSave = 0f;
                }
            }
        }

        public void Save()
        {
            _saveLoadService.SaveProgress();
            StartCoroutine(ShowSaveNotification());
        }

        public void EnableAutoSave(bool enable) => _isAutoSaveEnabled = enable;

        private IEnumerator ShowSaveNotification()
        {

            saveNotificationImage.gameObject.SetActive(true);
            _canvasGroup.alpha = 0f; 

            float fadeDuration = 1f; 
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
                yield return null;
            }

            saveNotificationImage.gameObject.SetActive(false);
        }
    }
}
