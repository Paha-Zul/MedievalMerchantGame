namespace Assets.Scripts.BehaviourTree.Leaf
{
    public class SellItem : LeafTask
    {
        public SellItem(BlackBoard blackboard) : base(blackboard)
        {

        }

        public override void Start()
        {
            base.Start();

            var item = this.bb.targetItem;

            //We need to exchange items to the unit buying items here.
            //Swap items.
            var amt = this.bb.myInventory.RemoveItemAmount(item.Name, item.Amount);
            this.bb.targetFootUnit.myUnit.inventory.AddItem(item.Name, amt);

            this.controller.FinishWithSuccess();
        }
    }
}