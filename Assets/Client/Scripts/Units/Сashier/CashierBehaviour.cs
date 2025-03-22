
using Client.Logic;

namespace Client.Units.Cashier
{
    public class CashierBehaviour : Unit
    {
        public WalkableAnimator animator;
        public DestinationTracker destinationTracker;
        public PutDish putDish;

        public bool isFree = true;
        public bool hasWeight = false;
        public bool hasOrder = false;
    }
}