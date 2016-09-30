using UnityEngine;
using System.Collections;

public class CheckInventoryHasItem : LeafTask {
    string itemName;
    int amountNeeded;

    public CheckInventoryHasItem(BlackBoard blackboard, string invItemName, int amountNeeded, Inventory targetInventory = null) : base(blackboard) {
        if (targetInventory != null) bb.targetInventory = targetInventory;

        this.itemName = invItemName;
        this.amountNeeded = amountNeeded;
    }

    public override void Start() {
        base.Start();

        //Simply check if the target inventory has enough of the item.
        if (bb.targetInventory.GetItemAmount(itemName) >= amountNeeded)
            this.controller.FinishWithSuccess();
        else
            this.controller.FinishWithFailure();
    }
}
