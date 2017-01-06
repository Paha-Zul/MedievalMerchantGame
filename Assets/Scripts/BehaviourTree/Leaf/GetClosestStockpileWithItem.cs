using UnityEngine;
using System.Collections;
using Util;

public class GetClosestStockpileWithItem : LeafTask {
    private readonly string _itemName;

    public GetClosestStockpileWithItem(BlackBoard blackboard, string itemName = "") : base(blackboard) {
        _itemName = itemName;
        
    }

    public override void Start() {
        base.Start();

        if (!_itemName.Equals(""))
            bb.targetItem.Name = _itemName;

        Building building = Finder.FindClosestStockpileWithItem(TeamManager.GetTeam("Player1").buildingList, bb.myself.transform.position, bb.targetItem.Name);

        //If nothing was found, finish with failure
        if (building == null) {
            this.controller.FinishWithFailure();
            Debug.Log("Couldn't find stockpile");

         //If found, set the blackboard target and finish with success.
        } else {
            this.bb.targetBuilding = building;
            this.bb.targetInventory = building.MyUnit.inventory;
            this.controller.FinishWithSuccess();
        }
    }
}
