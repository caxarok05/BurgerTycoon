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

        public void SetIdle()
        {
            //if (!IsCurrentAnimation("Idle"))
            //{
                _animator.ResetTrigger(Moving);
                _animator.SetTrigger(Idle);
            //}
        }

        public void StartWorking()
        {
            if (!IsCurrentAnimation("Working"))
            {
                _animator.SetTrigger(Working);
            }
        }

        public void StartWalking()
        {
            if (!IsCurrentAnimation("Moving"))
            {
                _animator.ResetTrigger(Idle);
                _animator.SetTrigger(Moving);
            }
        }

        private bool IsCurrentAnimation(string animationName)
        {
            var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.IsName(animationName);
        }
    }
}