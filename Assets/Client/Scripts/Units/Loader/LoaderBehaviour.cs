using Client.Infrastructure.Factory;
using Client.Infrastructure;
using Client.Infrastructure.States;
using Client.Units.Chef;


namespace Client.Units.Loader
{
    public class LoaderBehaviour : Unit
    {
        public WalkableAnimator animator;

        public PutDelivery putDelivery;

        public float oneTimeIngridientsDeliver { get; private set; } = 1;

        public bool isFree = true;

        public bool hasWeight = false;


        public void GoToStorage(StorageCrate crate)
        {
            agent.SetDestination(crate.deliveryPoint.position);
            animator.StartWalking();
            hasWeight = false;
        }

        public void GoToDeliver(ChefTable to)
        {
            agent.SetDestination(to.deliveryPlace.transform.position);
            animator.StartWalking();
            hasWeight = true;
        }

        public bool CheckDistance()
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    
}