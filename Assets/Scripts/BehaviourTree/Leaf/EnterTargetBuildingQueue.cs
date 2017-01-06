namespace Assets.Scripts.BehaviourTree.Leaf
{
    public class EnterTargetBuildingQueue : LeafTask
    {
        public EnterTargetBuildingQueue(BlackBoard blackboard) : base(blackboard)
        {
        }

        public override void Start()
        {
            base.Start();
            this.bb.targetBuilding.UnitQueue.Enqueue(this.bb.myFootUnit);
            this.controller.FinishWithSuccess();
        }
    }
}