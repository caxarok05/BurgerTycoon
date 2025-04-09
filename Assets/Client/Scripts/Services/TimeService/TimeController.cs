using System.Collections;
using UnityEngine;

namespace Client.Services.TimeService
{
    public class TimeController : ITimeController
    {
        private float timeScale = 1f;
        private bool isTimeAccelerated = false;
        private Coroutine accelerationCoroutine;

        public void SetTimeScale(float scale)
        {
            timeScale = scale;
            isTimeAccelerated = scale > 1f;
            Time.timeScale = timeScale;
        }

        public void ResetTime()
        {
            SetTimeScale(1f);
        }

        public bool IsTimeAccelerated()
        {
            return isTimeAccelerated;
        }

        public Coroutine StartAccelerationCoroutine(float duration, MonoBehaviour coroutineOwner)
        {
            if (accelerationCoroutine != null)
            {
                coroutineOwner.StopCoroutine(accelerationCoroutine);
            }

            accelerationCoroutine = coroutineOwner.StartCoroutine(ResetTimeAfterDuration(duration));
            return accelerationCoroutine;
        }

        private IEnumerator ResetTimeAfterDuration(float duration)
        {
            yield return new WaitForSecondsRealtime(duration);
            ResetTime();
        }
    }
}