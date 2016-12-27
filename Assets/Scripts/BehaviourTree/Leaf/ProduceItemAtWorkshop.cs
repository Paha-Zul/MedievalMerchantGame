using UnityEngine;

public class ProduceItemAtWorkshop : LeafTask {
    float counter = 0f;

    DataDefs.ProductionDef itemProduction;
    Inventory workshopInv;

    public ProduceItemAtWorkshop(BlackBoard blackboard) : base(blackboard) {

    }

    public override void Start() {
        base.Start();

        if(bb.myWorkerUnit == null) {
            Debug.Log("Failing ProduceItemAtWorkshop because blackboard.myWorkUnit is null");
            this.controller.FinishWithFailure();
        }

        itemProduction = DataDefs.prodDefMap[bb.myWorkerUnit.myBuilding.producesItem];
        workshopInv = bb.myWorkerUnit.myBuilding.myUnit.inventory;
    }

    public override void Update(float delta) {
        base.Update(delta);

        counter += delta;

        //When the counter reaches the threshold, produce the item.
        if(counter >= itemProduction.baseProdTime) {
            WorkerUnit worker = bb.myWorkerUnit;

            //Get the amount to produce. Limit this by the amount set on the workshop building.
            var amtToProduce = Mathf.Min(workshopInv.GetItemAmount(itemProduction.inputItem), itemProduction.inputAmount);

            //Remove the item that was needed to produce, and add the produced item. A switcharoo!
            workshopInv.RemoveItemAmount(itemProduction.inputItem, amtToProduce);
            workshopInv.AddItem(itemProduction.outputItem, itemProduction.outputAmount);

            this.controller.FinishWithSuccess();
            Debug.Log("Task: Produced " + itemProduction.outputItem);
        }
    }

}
