using UnityEngine;

public class ProduceItemAtWorkshop : LeafTask {
    private float _counter = 0f;

    private DataDefs.ProductionDef _itemProduction;
    private Inventory _workshopInv;

    public ProduceItemAtWorkshop(BlackBoard blackboard) : base(blackboard) {

    }

    public override void Start() {
        base.Start();

        if(bb.myWorkerUnit == null) {
            Debug.Log("Failing ProduceItemAtWorkshop because blackboard.myWorkUnit is null");
            this.controller.FinishWithFailure();
        }

        _itemProduction = DataDefs.prodDefMap[bb.myWorkerUnit.myBuilding.producesItem];
        _workshopInv = bb.myWorkerUnit.myBuilding.myUnit.inventory;
    }

    public override void Update(float delta) {
        base.Update(delta);

        _counter += delta;

        //When the counter reaches the threshold, produce the item.
        if(_counter >= _itemProduction.baseProdTime) {
            WorkerUnit worker = bb.myWorkerUnit;

            //Get the amount to produce. Limit this by the amount set on the workshop building.
            var amtToProduce = Mathf.Min(_workshopInv.GetItemAmount(_itemProduction.inputItem), _itemProduction.inputAmount);

            //Remove the item that was needed to produce, and add the produced item. A switcharoo!
            _workshopInv.RemoveItemAmount(_itemProduction.inputItem, amtToProduce);
            _workshopInv.AddItem(_itemProduction.outputItem, _itemProduction.outputAmount);

            this.controller.FinishWithSuccess();
        }
    }

    public override void Reset()
    {
        base.Reset();
        _counter = 0f;
    }
}
