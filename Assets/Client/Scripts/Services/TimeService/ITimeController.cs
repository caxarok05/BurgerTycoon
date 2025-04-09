using UnityEngine;

namespace Client.Services.TimeService
{
    public interface ITimeController : IService
    {
        void SetTimeScale(float scale);
        void ResetTime();
        bool IsTimeAccelerated();
        Coroutine StartAccelerationCoroutine(float duration, MonoBehaviour coroutineOwner);
    }
}