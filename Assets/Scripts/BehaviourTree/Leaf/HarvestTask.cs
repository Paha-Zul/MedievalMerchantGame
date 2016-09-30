using UnityEngine;
using System.Collections;

public class HarvestTask : LeafTask {
    float counter = 0f;
    float harvestTime = 2f;

    bool addedHarvester = false;

    public HarvestTask(BlackBoard blackboard, Resource targetResource = null) : base(blackboard) {
        if (targetResource != null) bb.targetResource = targetResource;
    }

    public override void Start() {
        base.Start();

        if (bb.targetResource == null) {
            this.controller.FinishWithFailure();
            return;
        }

        this.harvestTime = bb.targetResource.harvestTime;
        this.addedHarvester = true;
    }

    public override void Update(float delta) {
        base.Update(delta);

        counter += delta;
        if(counter >= harvestTime) {
            var harvestAmount = this.bb.targetResource.Harvest();
            this.bb.myInventory.AddItem(bb.targetResource.harvestedItemName, harvestAmount);
            this.controller.FinishWithSuccess();
        }
    }

    public override void End() {
        base.End();
        if(addedHarvester)
            this.bb.targetResource.RemoveHarvester();
    }
}
