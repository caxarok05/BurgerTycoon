using UnityEngine;

namespace Client.Units
{
    public class WalkableAnimator : MonoBehaviour
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int Working = Animator.StringToHash("Working");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetIdle() => _animator.SetTrigger(Idle);

        public void StartWorking() => _animator.SetTrigger(Working);

        public void StartWalking()
        {
            _animator.ResetTrigger(Idle);
            _animator.SetTrigger(Moving);
        }
    }
}