namespace Assets.Scripts.BehaviourTree.Leaf
{
    public class CheckInsideBuilding : LeafTask
    {
        public CheckInsideBuilding(BlackBoard blackboard) : base(blackboard)
        {
        }

        public override void Start()
        {
            base.Start();
            if(this.bb.myFootUnit.CurrPathNode != null)
                this.controller.FinishWithSuccess();
            else
                this.controller.FinishWithFailure();
        }
    }
}