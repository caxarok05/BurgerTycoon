using Client.Infrastructure.States;
using Client.Services;
using Client.Logic;

namespace Client.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}