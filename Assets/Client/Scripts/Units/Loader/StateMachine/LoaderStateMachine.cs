using System;
using System.Collections.Generic;

namespace Client.Units.Loader.StateMachine
{
    public class LoaderStateMachine
    {
        private Dictionary<Type, IExitableLoaderState> _states;
        private IExitableLoaderState _activeState;

        public LoaderStateMachine(LoaderBehaviour loader, StorageCrate crate, WalkableAnimator animator)
        {
            _states = new Dictionary<Type, IExitableLoaderState>
            {
                [typeof(GetIngridientsState)] = new GetIngridientsState(loader, crate, animator),
                [typeof(SetIngridientsState)] = new SetIngridientsState(loader, animator),
            };
        }

        public void Enter<TState>() where TState : class, ILoaderState
        {
            ILoaderState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadLoaderState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableLoaderState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableLoaderState =>
          _states[typeof(TState)] as TState;
    }
}