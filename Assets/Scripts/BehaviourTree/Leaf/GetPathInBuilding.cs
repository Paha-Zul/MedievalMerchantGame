﻿using Assets.Scripts.Util;
using UnityEngine;
using Util;

namespace BehaviourTree.Leaf
{
    public class GetPathInBuilding : LeafTask {
        private readonly PathType _pathTarget;
        private PathNode _closestPathPoint;
        private readonly PathType _pathStart;

        public GetPathInBuilding(BlackBoard blackboard, PathType pathTarget, PathType pathStart = PathType.Curr) : base(blackboard)
        {
            this._pathTarget = pathTarget;
            _pathStart = pathStart;
        }

        public GetPathInBuilding(BlackBoard blackboard, PathType pathTarget, PathNode pointStart = null) : base(blackboard)
        {
            this._pathTarget = pathTarget;
            this._closestPathPoint = pointStart;
        }

        public override void Start() {
            base.Start();


            var spots = bb.targetBuilding.walkSpots.Count - 1;

            PathNode start = GetPathPoint();
            PathNode end = null;

            //TODO Handle more than 1 entrance spot?
            if (start == null)
                start = bb.targetBuilding.entranceSpots[0].GetComponent<PathNode>(); //If our start is still null, use the first entrance spot.

            switch (_pathTarget)
            {
                case PathType.Work:
                    end = bb.targetBuilding.workSpots[0].GetComponent<PathNode>();
                    break;
                case PathType.Sell:
                    end = bb.targetBuilding.sellSpots[0].GetComponent<PathNode>();
                    break;
                default:
                    end = bb.targetBuilding.sellSpots[0].GetComponent<PathNode>();
                    break;
            }

            var path = Util.Util.FindPathToNode(start, end);

            this.bb.waypoints = path;
            this.controller.FinishWithSuccess();
        }

        private PathNode GetPathPoint()
        {
            //TODO Handle multiple spots in array.
            switch (_pathStart)
            {
                case PathType.Work:
                    return bb.targetBuilding.workSpots[0].GetComponent<PathNode>();
                    break;
                case PathType.Sell:
                    return bb.targetBuilding.sellSpots[0].GetComponent<PathNode>();
                    break;
                case PathType.Entrance:
                    return bb.targetBuilding.entranceSpots[0].GetComponent<PathNode>();
                    break;
                default:
                    return bb.myFootUnit.CurrPathNode;
                    break;
            }
        }
    }
}