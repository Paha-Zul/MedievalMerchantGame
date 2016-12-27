using UnityEngine;
using System.Collections;

public class CheckWorkshopHasInputItem : LeafTask {
    public CheckWorkshopHasInputItem(BlackBoard blackboard) : base(blackboard) {

    }

    public override void Start() {
        base.Start();

        WorkShop shop = bb.myWorkerUnit.myBuilding;

        var inputItemName = DataDefs.prodDefMap[shop.producesItem].inputItem;
        var amountInInventory = shop.myUnit.inventory.GetItemAmount(inputItemName);

        //If the Workshop has enough of the input item, success! Otherwise, fail!
        if (amountInInventory >= shop.itemsProducedAtATime)
            this.controller.FinishWithSuccess();
        else {
            Debug.Log("Task: Inventory doesn't have enough. Amount needed: " + shop.itemsProducedAtATime + ", amount in inv: " + amountInInventory);
            this.controller.FinishWithFailure();
        }
    }
}
