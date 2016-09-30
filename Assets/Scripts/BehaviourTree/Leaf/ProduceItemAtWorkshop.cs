using UnityEngine;
using System.Collections;

public class ProduceItemAtWorkshop : LeafTask {
    float counter = 0f;

    public ProduceItemAtWorkshop(BlackBoard blackboard) : base(blackboard) {

    }

    public override void Start() {
        base.Start();

        if(bb.myWorkerUnit == null) {
            Debug.Log("Failing ProduceItemAtWorkshop because blackboard.myWorkUnit is null");
            this.controller.FinishWithFailure();
        }

    }

    public override void Update(float delta) {
        base.Update(delta);

        counter += delta;

        //When the counter reaches the threshold, produce the item.
        if(counter >= bb.myWorkerUnit.myBuilding.itemProduction.baseProdTime) {
            WorkShop workshop = bb.myWorkerUnit.myBuilding;
            WorkerUnit worker = bb.myWorkerUnit;

            //Get the amount to produce. Limit this by the amount set on the workshop building.
            var amtToProduce = Mathf.Min(workshop.inventory.GetItemAmount(workshop.itemProduction.inputItemName), workshop.itemsProducedAtATime);

            //Remove the item that was needed to produce, and add the produced item. A switcharoo!
            workshop.inventory.RemoveItemAmount(workshop.itemProduction.inputItemName, amtToProduce);
            workshop.inventory.AddItem(workshop.itemProduction.outputItemName, amtToProduce);
            this.controller.FinishWithSuccess();
        }
    }

}
