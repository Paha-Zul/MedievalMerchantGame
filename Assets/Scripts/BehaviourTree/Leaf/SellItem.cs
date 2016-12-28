using System;
using Assets.Scripts.Util;

namespace Assets.Scripts.BehaviourTree.Leaf
{
    public class SellItem : LeafTask
    {

        private readonly int _someRandomAmountForNow = 10;

        public SellItem(BlackBoard blackboard) : base(blackboard)
        {

        }

        public override void Start()
        {
            base.Start();

            //TODO This is a hack for testing. Smarter way to handle what the customer wants?
            var wanted = this.bb.targetFootUnit.MyUnit.manager.bb.targetItem;
            this.bb.targetItem.Set(wanted.Name, wanted.Amount);

            ItemCostPair itemWanted = new ItemCostPair("", 0);
            var selling = this.bb.targetBuilding.GetComponent<SellItems>();

            //Find the item we are looking for in the being sold list. We have to do this to get the updated price.
            foreach(var item in selling.itemsBeingSold) {
                if (this.bb.targetItem.Name.Equals(item.Name)) {
                    itemWanted = item.Copy(); //Copy it and break outta here!
                    break;
                }
            }

            //If we didn't find any item matching, fail and return
            if (itemWanted.Name.Equals(""))
            {
                this.controller.FinishWithFailure();
                return;
            }

            var buildingInventory = this.bb.targetBuilding.myUnit.inventory;
            var targetInventory = this.bb.targetFootUnit.MyUnit.inventory;

            var amtToSell = Math.Min(buildingInventory.GetItemAmount(itemWanted.Name), _someRandomAmountForNow); //The amount the building inventory has to offer

            var numItemsCanAfford = targetInventory.GetItemAmount("Gold Coin") / itemWanted.Cost; //Number of items the target can afford
            amtToSell = Math.Min(numItemsCanAfford, amtToSell); //Take the lesser of the two

            var totalCost = itemWanted.Cost*amtToSell; //The total cost of the item with how much we are selling

            //Take the item from the BUILDING's inventory and give it gold coins
            amtToSell = buildingInventory.RemoveItemAmount(itemWanted.Name, amtToSell);
            buildingInventory.AddItem("Gold Coin", totalCost);

            //Give the item to the target but take its money
            targetInventory.AddItem(itemWanted.Name, amtToSell);
            targetInventory.RemoveItemAmount("Gold Coin", totalCost);

            this.bb.targetFootUnit.MyUnit.manager.bb.QueueFlag = true; //Set this to true to trigger its response. This means we have handled the sale.

            this.controller.FinishWithSuccess();
        }
    }
}