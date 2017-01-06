using System;
using Assets.Scripts.Util;
using Util;

namespace Assets.Scripts.BehaviourTree.Leaf
{
    public class SetTargetNode:LeafTask
    {

        public enum TargetNodeType{ Start, End}

        private readonly TargetNodeType _targetType;

        /// <summary>
        /// Sets the
        /// </summary>
        /// <param name="blackboard"></param>
        /// <param name="targetType"></param>
        public SetTargetNode(BlackBoard blackboard, TargetNodeType targetType) : base(blackboard)
        {
            this._targetType = targetType;
        }

        public override void Start()
        {
            base.Start();
            if (_targetType == TargetNodeType.Start)
                this.bb.TargetStartNode = bb.targetPosition.GetComponent<PathNode>();
            else
                this.bb.TargetEndNode = bb.targetPosition.GetComponent<PathNode>();

            this.controller.FinishWithSuccess();
        }
    }
}