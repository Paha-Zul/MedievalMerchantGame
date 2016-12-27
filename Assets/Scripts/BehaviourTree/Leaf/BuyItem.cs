using UnityEngine;
using System.Collections;

public class BuyItem : LeafTask {
    public BuyItem(BlackBoard blackboard) : base(blackboard) {

    }

    public override void Start() {
        base.Start();

        ItemCostPair itemWanted = null;
        var selling = this.bb.targetBuilding.GetComponent<SellItems>();

        //Find the item we are looking for in the being sold list. We have to do this to get the updated price.
        foreach(var item in selling.itemsBeingSold) {
            if (this.bb.targetItem.Name.Equals(item.first)) {
                itemWanted = item.Copy(); //Copy it and break outta here!
                break;
            }
        }

        //Return if we didn't find what we were looking for
        if(itemWanted == null) {
            this.controller.FinishWithFailure();
            return;
        }

        //Get the amount (either what the inventory has left or the amount we want, whichever is lower)
        var amount = Mathf.Min(bb.targetBuilding.myUnit.inventory.GetItemAmount(bb.targetItem.Name), bb.targetItem.Amount);
        var cost = itemWanted.second * amount; //Calulcate the cost

        //Check if we have enough money. If not, recalculate cost and amount to be taken.
        var money = this.bb.myUnit.inventory.GetItemAmount("Gold Coin");
        if(cost > money) {
            amount = money / itemWanted.second;
            cost = itemWanted.second * amount;
        }

        //Transfer the money over.
        this.bb.myUnit.inventory.RemoveItemAmount("Gold Coin", cost);
        this.bb.targetBuilding.myUnit.inventory.AddItem("Gold Coin", cost);

        //Take from the building's inventory and put into ours, transfer!
        amount = bb.targetBuilding.myUnit.inventory.RemoveItemAmount(itemWanted.first, amount);
        bb.myUnit.inventory.AddItem(itemWanted.first, amount);

        //Finish!
        this.controller.FinishWithSuccess();
    }
}
