using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Logic.TimeBooster
{
    public class X2TimerSwitcher : MonoBehaviour
    {
        public event Action<bool> OnStateChangedEvent;

        [SerializeField] private List<GameObject> _disabledObjectsAfterOnEnable;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private GameObject _checkMarkDisable;

        private void OnEnable()
        {
            DisableObjects();
            _toggle.onValueChanged.AddListener(ToggleValueChanged);
        }

        private void ToggleValueChanged(bool toggleValue)
        {
            OnStateChangedEvent?.Invoke(toggleValue);
            _checkMarkDisable.SetActive(!toggleValue);
        }

        private void DisableObjects()
        {
            foreach (GameObject obj in _disabledObjectsAfterOnEnable)
            {
                obj.SetActive(false);
            }
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(ToggleValueChanged);
        }
    }

}
