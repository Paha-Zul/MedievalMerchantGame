using Assets.Scripts.Util;
using UnityEngine;
using Util;

namespace BehaviourTree.Leaf
{
    public class GetPathInBuilding : LeafTask {
        private readonly SpotType _targetType, _startType;
        private PathNode targetNode;
        private PathNode _closestPathPoint;

        /// <summary>
        /// Uses the bb.TargetStartNode and bb.TargetEndNode as the start and end for the path calculations.
        /// </summary>
        /// <param name="blackboard"></param>
        public GetPathInBuilding(BlackBoard blackboard) : base(blackboard)
        {
        }

        public GetPathInBuilding(BlackBoard blackboard, SpotType targetType, SpotType startType = SpotType.Curr) : base(blackboard)
        {
            this._targetType = targetType;
            _startType = startType;
        }

        public GetPathInBuilding(BlackBoard blackboard, SpotType targetType, PathNode pointStart = null) : base(blackboard)
        {
            this._targetType = targetType;
            this._closestPathPoint = pointStart;
        }

        public override void Start() {
            base.Start();

            var spots = bb.targetBuilding.BuySpots.Count - 1;

            PathNode start;
            PathNode end;

            //If we never assigned a spot type, we want to pull values from the blackboard
            if (_targetType == SpotType.None && _startType == SpotType.None)
            {
                start = bb.TargetStartNode;
                end = bb.TargetEndNode;
            }
            //If only our target isn't set, set the target to our current target position
            else if (_targetType == SpotType.None)
            {
                end = bb.targetPosition.GetComponent<PathNode>();
                start = GetPathPoint(_targetType);
            }
            //If only our target isn't set, set the target to our current target position
            else if (_startType == SpotType.None)
            {
                start = bb.targetPosition.GetComponent<PathNode>();
                end = GetPathPoint(_targetType);
            }
            //Otherwise, get our path points.
            else
            {
                start = GetPathPoint(_startType);
                end = GetPathPoint(_targetType);

                //TODO Handle more than 1 entrance spot?
                if (start == null)
                    start = bb.targetBuilding.EntranceSpots[0].GetComponent<PathNode>(); //If our start is still null, use the first entrance spot.
            }

            var path = Util.Util.FindPathToNode(start, end);

            this.bb.waypoints = path;
            this.controller.FinishWithSuccess();
        }

        private PathNode GetPathPoint(SpotType spotType)
        {
            //TODO Handle multiple spots in array.
            switch (spotType)
            {
                case SpotType.Work:
                    return bb.targetBuilding.WorkSpots[0].GetComponent<PathNode>();
                case SpotType.Sell:
                    return bb.targetBuilding.SellSpots[0].GetComponent<PathNode>();
                case SpotType.Entrance:
                    return bb.targetBuilding.EntranceSpots[0].GetComponent<PathNode>();
                default:
                    return bb.myFootUnit.CurrPathNode;
            }
        }
    }
}
