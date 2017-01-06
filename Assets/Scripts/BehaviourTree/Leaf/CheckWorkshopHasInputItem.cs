using UnityEngine;
using System.Collections;

public class CheckWorkshopHasInputItem : LeafTask {
    public CheckWorkshopHasInputItem(BlackBoard blackboard) : base(blackboard) {

    }

    public override void Start() {
        base.Start();

        WorkShop shop = bb.myWorkerUnit.MyBuilding as WorkShop;

        var inputItemName = DataDefs.prodDefMap[shop.ItemsProduced.ItemToProduce].inputItem;
        var amountInInventory = shop.MyUnit.inventory.GetItemAmount(inputItemName);

        //If the Workshop has enough of the input item, success! Otherwise, fail!
        if (amountInInventory >= shop.ItemsProduced.ItemsProducedAtATime)
            this.controller.FinishWithSuccess();
        else {
            Debug.Log("Task: Inventory doesn't have enough. Amount needed: " + shop.ItemsProduced.ItemsProducedAtATime + ", amount in inv: " + amountInInventory);
            this.controller.FinishWithFailure();
        }
    }
}
