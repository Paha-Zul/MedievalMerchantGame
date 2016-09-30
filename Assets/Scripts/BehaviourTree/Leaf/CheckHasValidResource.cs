using UnityEngine;
using System.Collections;

/// <summary>
/// Simply checks if the Resource in the BlackBoard is valid (null) or not. It invalid, fails, otherwise, succeeds.
/// </summary>
public class CheckHasValidResource : LeafTask {
    public CheckHasValidResource(BlackBoard blackboard) : base(blackboard) {
    }

    public override void Start() {
        base.Start();

        //Simply check if we have a not null resource.
        if (!this.bb.targetResource)
            this.controller.FinishWithFailure();
        else 
            this.controller.FinishWithSuccess();
    }
}
