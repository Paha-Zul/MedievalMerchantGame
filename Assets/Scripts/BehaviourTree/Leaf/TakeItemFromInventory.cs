using UnityEngine;
using System.Collections;

public class TakeItemFromInventory : LeafTask {
    public TakeItemFromInventory(BlackBoard blackboard, string itemName = "", int amount = 1) : base(blackboard) {
        if (!itemName.Equals("")) {
            this.bb.targetItem.name = itemName;
            this.bb.targetItem.amount = amount;
        }

    }

    public override void Start() {
        base.Start();

        var amountTaken = this.bb.targetInventory.RemoveItemAmount(bb.targetItem.name, bb.targetItem.amount);
        if(amountTaken > 0) {
            this.bb.myInventory.AddItem(bb.targetItem.name, amountTaken);
            this.controller.FinishWithSuccess();
        }
    }
}
