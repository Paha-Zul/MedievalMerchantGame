using UnityEngine;
using System.Collections;

public class GiveItemToInventory : LeafTask {
    public GiveItemToInventory(BlackBoard blackboard) : base(blackboard) {

    }

    public override void Start() {
        base.Start();

        var name = bb.targetItem.Name;
        var amount = bb.myInventory.RemoveItemAmount(name, int.MaxValue); //Take as much as we can

        bb.targetInventory.AddItem(name, amount);

        this.controller.FinishWithSuccess();
    }
}
