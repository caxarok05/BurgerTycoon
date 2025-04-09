using Client.Services.TimeService;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Logic.TimeBooster
{
    public class ToggleBooster : MonoBehaviour
    {
        public Toggle toggle;
        private ITimeController _timeController;

        public void Construct(ITimeController timeController)
        {
            _timeController = timeController;
        }

        private void Start()
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool isOn)
        {
            if (isOn)
                _timeController.SetTimeScale(2f);
            else
                _timeController.ResetTime();
        }
    }
}