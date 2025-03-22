namespace Client.Units.Loader.StateMachine
{
    public interface ILoaderState : IExitableLoaderState
    {
        void Enter();
    }

    public interface IPayloadLoaderState<TPayload> : IExitableLoaderState
    {
        void Enter(TPayload payload);
    }

    public interface IExitableLoaderState
    {
        void Exit();
    }
}