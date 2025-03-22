using UnityEngine;

namespace Client.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper != null) return;

            Instantiate(BootstrapperPrefab);
        }
    }
}