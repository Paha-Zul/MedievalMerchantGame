using UnityEngine;
using System.Collections;

public class SetMyBuildingAsTarget : LeafTask {
    public SetMyBuildingAsTarget(BlackBoard blackboard) : base(blackboard) {

    }

    public override void Start() {
        base.Start();

        this.bb.targetBuilding = bb.myWorkerUnit.MyBuilding;
        this.controller.FinishWithSuccess();
    }
}
