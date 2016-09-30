using UnityEngine;
using System.Collections;

public class SetTargetInventoryFromTargetBuilding : LeafTask {
    public SetTargetInventoryFromTargetBuilding(BlackBoard blackboard) : base(blackboard) {

    }

    public override void Start() {
        base.Start();

        bb.targetInventory = bb.targetBuilding.inventory;
        this.controller.FinishWithSuccess();
    }
}
