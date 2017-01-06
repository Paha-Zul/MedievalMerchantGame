using System.Linq;
using Util;

namespace Assets.Scripts.BehaviourTree.Leaf
{
    /// <summary>
    /// Takes the bb.targetPosition and attempts to work towards the nearest entrance. If an entrance is found, the
    /// task succeeds and the entrance is stored in bb.targetPosition
    /// </summary>
    public class GetEntranceThatLeadsToPoint : LeafTask
    {
        /// <summary>
        /// Takes the bb.targetPosition and attempts to work towards the nearest entrance. If an entrance is found, the
        /// task succeeds and the entrance is stored in bb.targetPosition
        /// </summary>
        /// <param name="blackboard">The BlackBoard object.</param>
        public GetEntranceThatLeadsToPoint(BlackBoard blackboard) : base(blackboard)
        {

        }

        public override void Start()
        {
            base.Start();

            var path = global::Util.Util.FindPathToNode(bb.targetPosition.GetComponent<PathNode>(), "Spot_Entrance");

            if (path.Length > 0)
            {
                this.controller.FinishWithSuccess();
                this.bb.targetPosition = path[path.Length-1];
            }
            else
                this.controller.FinishWithFailure();
        }
    }
}