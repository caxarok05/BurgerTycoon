using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client.UI
{
    public class OpenCloseWindow : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        public void OpenClose()
        {
            if (!panel.activeInHierarchy)
            {
                panel.SetActive(true);
                DisableCamera();
            }
            else
            {
                panel.SetActive(false);
                EnableCamera();
            }
        }

        public void Close()
        {
            if (panel.activeInHierarchy)
                panel.SetActive(false);
        }

        public void Open()
        {
            if (!panel.activeInHierarchy)
                panel.SetActive(true);
        }

        public void DisableCamera() => Camera.main.GetComponent<CameraMovement>().IsDisabled = true;

        public void EnableCamera() => Camera.main.GetComponent<CameraMovement>().IsDisabled = false;
    }
}