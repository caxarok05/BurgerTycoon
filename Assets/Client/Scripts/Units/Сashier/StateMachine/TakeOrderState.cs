using Client.Units.Chef;
using UnityEngine;

namespace Client.Units.Cashier.StateMachine
{
    public class TakeOrderState : IPayloadCashierState<ChefTable>
    {
        private CashierBehaviour _cashier;
        private OrderPlace _orderPlace;
        private WalkableAnimator _animator;

        private ChefTable _currentTable;
        public TakeOrderState(CashierBehaviour cashier, OrderPlace orderPlace, WalkableAnimator animator)
        {
            _cashier = cashier;
            _orderPlace = orderPlace;
            _animator = animator;
        }


        public void Enter(ChefTable table)
        {
            _cashier.agent.SetDestination(table.takeOrderPlace.position);
            _animator.StartWalking();
            _currentTable = table;
            _cashier.destinationTracker.OnDestinationReached += ReachChefTable;
        }
        public void Exit()
        {
        }

        private async void ReachChefTable()
        {
            _cashier.destinationTracker.OnDestinationReached -= ReachChefTable;
            _cashier.gameObject.transform.LookAt(_currentTable.takeOrderPlace.position);
            _animator.StartWorking();
            await _cashier.gameObject.GetComponent<PutDish>().TakeDish(_currentTable);

            _cashier.hasWeight = true;
            _cashier.isFree = true;
            _currentTable.isOccupiedbyCashier = false;

        }

    }
}