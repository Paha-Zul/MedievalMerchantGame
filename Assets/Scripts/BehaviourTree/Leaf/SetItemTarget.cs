using UnityEngine;
using System.Collections;

public class SetItemTarget : LeafTask {
    public SetItemTarget(BlackBoard blackboard, string itemName, int itemAmount) : base(blackboard) {
        this.bb.targetItem.name = itemName;
        this.bb.targetItem.amount = itemAmount;
    }

    public override void Start() {
        base.Start();

        this.controller.FinishWithSuccess();
    }
}
