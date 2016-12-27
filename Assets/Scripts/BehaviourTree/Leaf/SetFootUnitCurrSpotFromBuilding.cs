namespace BehaviourTree.Leaf
{
    public class SetFootUnitCurrSpotFromBuilding:LeafTask
    {
        private readonly string _spotName;
        private readonly int _index;

        public SetFootUnitCurrSpotFromBuilding(BlackBoard blackboard, string spotName, int index = 0) : base(blackboard)
        {
            _spotName = spotName;
            _index = index;
        }

        public override void Start()
        {
            base.Start();
            this.bb.myFootUnit.CurrPathNode = Util.Util.GetSpotOfBuilding(_spotName, bb.targetBuilding);

            if(this.bb.myFootUnit.CurrPathNode != null) this.controller.FinishWithSuccess();
            else this.controller.FinishWithFailure();
        }
    }
}