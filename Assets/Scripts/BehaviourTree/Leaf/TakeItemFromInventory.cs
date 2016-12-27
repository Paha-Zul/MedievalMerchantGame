using UnityEngine;
using System.Collections;

public class TakeItemFromInventory : LeafTask {
    public TakeItemFromInventory(BlackBoard blackboard, string itemName = "", int amount = 1) : base(blackboard) {
        if (!itemName.Equals("")) {
            this.bb.targetItem.Name = itemName;
            this.bb.targetItem.Amount = amount;
        }

    }

    public override void Start() {
        base.Start();

        var amountTaken = this.bb.targetInventory.RemoveItemAmount(bb.targetItem.Name, bb.targetItem.Amount);
        if(amountTaken > 0) {
            this.bb.myInventory.AddItem(bb.targetItem.Name, amountTaken);
            this.controller.FinishWithSuccess();
        }
    }
}
