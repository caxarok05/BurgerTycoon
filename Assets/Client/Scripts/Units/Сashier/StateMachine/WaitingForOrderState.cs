using UnityEngine;

namespace Client.Units.Cashier.StateMachine
{
    public class WaitingForOrderState : ICashierState
    {
        private CashierStateMachine _stateMachine;
        private CashierBehaviour _cashier;
        private OrderPlace _orderPlace;
        private WalkableAnimator _animator;
        public WaitingForOrderState(CashierStateMachine stateMachine, CashierBehaviour cashier, OrderPlace orderPlace, WalkableAnimator animator)
        {
            _cashier = cashier;
            _orderPlace = orderPlace;
            _animator = animator;
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            _cashier.agent.SetDestination(_orderPlace.giveOrderPoint.position);
            _animator.StartWalking();
            _cashier.destinationTracker.OnDestinationReached += ReachOrderPlace;
        }
        public void Exit()
        {
        }

        private void ReachOrderPlace()
        {
            _cashier.destinationTracker.OnDestinationReached -= ReachOrderPlace;
            _cashier.gameObject.transform.LookAt(_orderPlace.CashierlookAtPoint);
            _animator.SetIdle();
        }

    }
}