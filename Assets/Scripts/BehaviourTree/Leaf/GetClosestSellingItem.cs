using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetClosestSellingItem : LeafTask {
    public GetClosestSellingItem(BlackBoard blackboard, string itemName = "", int itemAmount = 0) : base(blackboard) {
        if (!itemName.Equals("")){
            bb.targetItem.Name = itemName;
            bb.targetItem.Amount = itemAmount;
        }
    }

    public override void Start() {
        base.Start();

        List<SellItems> sellingList = new List<SellItems>();

        //TODO Maybe change this so that we can use more than just a building. Like a merchant selling from himself?

        //First, filter out every building that can sell items.
        foreach (var b in bb.myUnit.playerTeam.team.buildingList) {
            var selling = b.GetComponent<SellItems>();
            if (selling)
                sellingList.Add(selling);
        }

        //Secondly, get the closest selling building.
        Building closestBuilding = null;
        var closest = 0f;

        foreach(var sell in sellingList) {
            var dst = Vector3.Distance(this.bb.myUnit.transform.position, sell.transform.position);
            if(closestBuilding == null || dst <= closest) {
                closestBuilding = sell.GetComponent<Building>(); //TODO Maybe change this so that we can use more than just a building. Like a merchant selling from himself?
                closest = dst;
            }
        }

        this.bb.targetBuilding = closestBuilding;

        this.controller.FinishWithSuccess();

    }
}
