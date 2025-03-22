using Client.Logic;
using Client.Units.Chef;
using System.Threading.Tasks;

namespace Client.Units.Loader.StateMachine
{
    public class SetIngridientsState : IPayloadLoaderState<ChefTable>
    {
        private LoaderBehaviour _loader;
        private WalkableAnimator _animator;
        private ChefTable _chefTable;

        public SetIngridientsState(LoaderBehaviour loader, WalkableAnimator animator)
        {
            _loader = loader;
            _animator = animator;
        }

        public void Enter(ChefTable payload)
        {
            _chefTable = payload;
            _loader.agent.SetDestination(_chefTable.deliveryPlace.transform.position);
            _animator.StartWalking();
            _loader.gameObject.GetComponent<DestinationTracker>().OnDestinationReached += SetIngridients;
        }

        public void Exit()
        {
        }

        public async void SetIngridients()
        {
            _loader.gameObject.GetComponent<DestinationTracker>().OnDestinationReached -= SetIngridients;
            _animator.StartWorking();
            _loader.gameObject.transform.LookAt(_chefTable.gameObject.transform.position);
            await _loader.gameObject.GetComponent<PutDelivery>().Deliver(_chefTable);

            _loader.hasWeight = false;
            _loader.isFree = true;

            _chefTable.isOccupiedbyLoader = false;
        }

    }
}