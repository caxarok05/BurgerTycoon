using System.Collections.Generic;
using System;

namespace Client.Units.Cashier.StateMachine
{
    public class CashierStateMachine
    {
        private Dictionary<Type, IExitableCashierState> _states;

       /* private */ public IExitableCashierState _activeState;

        public CashierStateMachine(CashierBehaviour cashier, OrderPlace orderPlace, WalkableAnimator animator)
        {
            _states = new Dictionary<Type, IExitableCashierState>
            {
                [typeof(WaitingForOrderState)] = new WaitingForOrderState(this, cashier, orderPlace, animator),
                [typeof(TakeOrderState)] = new TakeOrderState(cashier, orderPlace, animator),
                [typeof(GiveOrderState)] = new GiveOrderState(cashier, orderPlace, animator),
            };
        }

        public void Enter<TState>() where TState : class, ICashierState
        {
            
            ICashierState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadCashierState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableCashierState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableCashierState =>
          _states[typeof(TState)] as TState;

    }
}