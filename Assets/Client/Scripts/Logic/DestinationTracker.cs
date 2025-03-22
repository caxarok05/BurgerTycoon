using Client.Units;
using Client.Units.Loader;
using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Client.Logic
{
    public class DestinationTracker : MonoBehaviour
    {
        public Unit unit;
        public event Action OnDestinationReached;

        private bool hasReachedDestination = false;

        private void Update()
        {
            if (!hasReachedDestination)
                CheckDistance();

        }
        public void CheckDistance()
        {
            if (!unit.agent.pathPending)
            {
                if (unit.agent.remainingDistance <= unit.agent.stoppingDistance)
                {
                    if (!unit.agent.hasPath || unit.agent.velocity.sqrMagnitude == 0f)
                    {
                        OnDestinationReached?.Invoke();
                        unit.agent.ResetPath();
                    }
                }
            }
        }

        public void SetDistination(bool hasReachedDestination)
        {
            this.hasReachedDestination = hasReachedDestination;
        }
    }
}