using Client.Services.TimeService;
using UnityEngine;

namespace Client.Logic.TimeBooster
{
    public class AdTimeBooster : MonoBehaviour
    {
        private ITimeController _timeController;

        public void Construct(ITimeController timeController)
        {
            _timeController = timeController;
        }

        public void AccelerateTimeAfterAd()
        {
            Debug.Log("Реклама просмотрена. Ускорение времени на 2x активировано на 15 секунд.");
            _timeController.SetTimeScale(2f);
            _timeController.StartAccelerationCoroutine(15f, this);
        }
    }
}