using Assets.Scripts.Util;

namespace BehaviourTree.Leaf
{
    public class SetFootUnitCurrSpotFromBuilding:LeafTask
    {
        private readonly SpotType _spotType;
        private readonly int _index;

        public SetFootUnitCurrSpotFromBuilding(BlackBoard blackboard, SpotType spotType, int index = 0) : base(blackboard)
        {
            _spotType = spotType;
            _index = index;
        }

        public override void Start()
        {
            base.Start();
            this.bb.myFootUnit.CurrPathNode = Util.Util.GetSpotOfBuilding(_spotType, bb.targetBuilding);

            if(this.bb.myFootUnit.CurrPathNode != null) this.controller.FinishWithSuccess();
            else this.controller.FinishWithFailure();
        }
    }
}