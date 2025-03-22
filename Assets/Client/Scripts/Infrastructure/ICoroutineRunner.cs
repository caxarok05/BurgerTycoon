using System.Collections;
using UnityEngine;

namespace Client.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}