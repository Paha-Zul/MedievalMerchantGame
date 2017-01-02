using UnityEngine;
using System.Collections;

public class GetWorldExit : LeafTask {
    public GetWorldExit(BlackBoard blackboard) : base(blackboard) {
    }

    public override void Start() {
        base.Start();

        this.bb.targetPosition = GameMainScript.enterExitSpots[0].transform;

        this.controller.FinishWithSuccess();
    }
}
