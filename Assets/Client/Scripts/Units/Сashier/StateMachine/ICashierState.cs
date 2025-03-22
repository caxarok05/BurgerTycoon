namespace Client.Units.Cashier.StateMachine
{
    public interface ICashierState : IExitableCashierState
    {
        void Enter();
    }

    public interface IPayloadCashierState<TPayload> : IExitableCashierState
    {
        void Enter(TPayload payload);
    }

    public interface IExitableCashierState
    {
        void Exit();
    }
}

