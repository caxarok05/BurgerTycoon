using Client.Logic;
using System.Threading.Tasks;

namespace Client.Units.Loader.StateMachine
{
    public class GetIngridientsState : ILoaderState
    {
        private LoaderBehaviour _loader;
        private WalkableAnimator _animator;
        private StorageCrate _storageCrate;

        public GetIngridientsState(LoaderBehaviour loader, StorageCrate storageCrate, WalkableAnimator animator)
        {
            _loader = loader;
            _storageCrate = storageCrate;
            _animator = animator;
        }

        public void Enter()
        {
            _loader.agent.SetDestination(_storageCrate.deliveryPoint.transform.position);
            _animator.StartWalking();
            _loader.gameObject.GetComponent<DestinationTracker>().OnDestinationReached += GetIngridients;
        }
        public void Exit()
        {
        }

        private async void GetIngridients()
        {
            _loader.gameObject.GetComponent<DestinationTracker>().OnDestinationReached -= GetIngridients;
            _animator.StartWorking();
            _loader.gameObject.transform.LookAt(_storageCrate.lookAtPoint);
            await _loader.gameObject.GetComponent<PutDelivery>().TakeDelivery(_storageCrate);
            _loader.hasWeight = true;
            _loader.isFree = true;
        }

    }
}