using UnityEngine;
using System.Collections;

public class DestroySelf : LeafTask {
    public DestroySelf(BlackBoard blackboard) : base(blackboard) {
    }

    public override void Start() {
        base.Start();

        GameObject.Destroy(this.bb.myUnit.gameObject);

        this.controller.FinishWithSuccess();
    }
}
