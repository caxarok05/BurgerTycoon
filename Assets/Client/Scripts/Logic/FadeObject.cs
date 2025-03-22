using System.Collections;
using UnityEngine;

namespace Client.Logic
{
    public class FadeObject : MonoBehaviour
    {

        public float duration = 1f;

        private void Start()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            foreach (var renderer in renderers)
            {
                foreach (var material in renderer.materials)
                {
                    Color color = material.color;
                    color.a = 0f; 
                    material.color = color;
                }
            }

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                foreach (var renderer in renderers)
                {
                    for (int i = 0; i < renderer.materials.Length; i++)
                    {
                        Color color = renderer.materials[i].color;
                        color.a = Mathf.Lerp(0f, 1f, normalizedTime);
                        renderer.materials[i].color = color;
                    }
                }
                yield return null;
            }

            foreach (var renderer in renderers)
            {
                foreach (var material in renderer.materials)
                {
                    Color color = material.color;
                    color.a = 1f;
                    material.color = color;
                }
            }
        }
    }
}