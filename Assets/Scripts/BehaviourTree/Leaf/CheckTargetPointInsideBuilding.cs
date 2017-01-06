using Util;

namespace Assets.Scripts.BehaviourTree.Leaf
{
    public class CheckTargetPointInsideBuilding : LeafTask
    {
        public CheckTargetPointInsideBuilding(BlackBoard blackboard) : base(blackboard)
        {

        }

        public override void Start()
        {
            base.Start();

            //If our target position is not null, check for a path node.
            if (this.bb.targetPosition != null)
            {
                var point = bb.targetPosition.GetComponent<PathNode>();

                //If our path node is not null and it has neighbors, then we are in fact inside a building.
                if (point != null && point.NeighborNodes.Count > 0)
                {
                    this.controller.FinishWithSuccess();
                    return;
                }
            }

            //Fail if we make it here.
            this.controller.FinishWithFailure();
        }
    }
}