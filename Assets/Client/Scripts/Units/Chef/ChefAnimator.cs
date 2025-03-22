using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Client.Units.Chef
{
    public class ChefAnimator : MonoBehaviour
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Working = Animator.StringToHash("Working");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetIdle() => _animator.SetTrigger(Idle);

        public void StartWorking() => _animator.SetTrigger(Working);
    }
}
