using UnityEngine;
using System.Collections;

public class GetClosestStockpileWithItem : LeafTask {
    public GetClosestStockpileWithItem(BlackBoard blackboard, string itemName = "") : base(blackboard) {
        if (!itemName.Equals("")) bb.targetItem.name = itemName;
    }

    public override void Start() {
        base.Start();

        Building building = Finder.FindClosestStockpileWithItem(TeamManager.GetTeam("Player1").buildingList, bb.myself.transform.position, bb.targetItem.name);

        //If nothing was found, finish with failure
        if (building == null) {
            this.controller.FinishWithFailure();
            Debug.Log("Couldn't find stockpile");

         //If found, set the blackboard target and finish with success.
        } else {
            this.bb.targetBuilding = building;
            this.bb.targetInventory = building.inventory;
            this.controller.FinishWithSuccess();
        }
    }
}
