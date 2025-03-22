

using UnityEngine;

namespace Client.Units.Cashier.StateMachine
{
    public class GiveOrderState : ICashierState
    {
        private CashierBehaviour _cashier;
        private OrderPlace _orderPlace;
        private WalkableAnimator _animator;

        public GiveOrderState(CashierBehaviour cashier, OrderPlace orderPlace, WalkableAnimator animator)
        {
            _cashier = cashier;
            _orderPlace = orderPlace;
            _animator = animator;
        }

        public void Enter()
        {
            _cashier.agent.SetDestination(_orderPlace.giveOrderPoint.position);
            _animator.StartWalking();
            _cashier.destinationTracker.OnDestinationReached += ReachOrderPointTable;
        }
        public void Exit()
        {
        }

        public async void ReachOrderPointTable()
        {
            Debug.Log("Order");

            _cashier.destinationTracker.OnDestinationReached -= ReachOrderPointTable;
            _cashier.gameObject.transform.LookAt(_orderPlace.CashierlookAtPoint);
            _animator.StartWorking();
            await _cashier.gameObject.GetComponent<PutDish>().GiveOrder();
            _orderPlace.GiveOrder();
            _cashier.hasOrder = false;
            _cashier.isFree = true;
            _cashier.hasWeight = false;
        }

    }
}