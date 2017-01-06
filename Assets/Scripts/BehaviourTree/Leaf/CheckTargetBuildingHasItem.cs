namespace Assets.Scripts.BehaviourTree.Leaf
{
    public class CheckTargetBuildingHasItem : LeafTask
    {
        private readonly string _itemName;

        public CheckTargetBuildingHasItem(BlackBoard blackboard, string itemName) : base(blackboard)
        {
            _itemName = itemName;
        }

        public override void Start()
        {
            base.Start();

            if(this.bb.targetBuilding.MyUnit.inventory.HasItem(_itemName))
                this.controller.FinishWithSuccess();
            else
                this.controller.FinishWithFailure();
        }
    }
}