using UnityEngine;
using System.Collections;

public class GetWalkSpotOfTargetBuilding : LeafTask {
    public GetWalkSpotOfTargetBuilding(BlackBoard blackboard) : base(blackboard) {
    }

    public override void Start() {
        base.Start();
        var spots = bb.targetBuilding.walkSpots.Count-1;
        this.bb.targetPosition = this.bb.targetBuilding.walkSpots[Random.Range(0, spots)];
        this.controller.FinishWithSuccess();
    }
}
