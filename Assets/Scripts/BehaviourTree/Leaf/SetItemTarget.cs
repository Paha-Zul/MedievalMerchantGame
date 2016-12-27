using UnityEngine;
using System.Collections;

public class SetItemTarget : LeafTask {
    public SetItemTarget(BlackBoard blackboard, string itemName, int itemAmount) : base(blackboard) {
        this.bb.targetItem.Name = itemName;
        this.bb.targetItem.Amount = itemAmount;
    }

    public override void Start() {
        base.Start();

        this.controller.FinishWithSuccess();
    }
}
