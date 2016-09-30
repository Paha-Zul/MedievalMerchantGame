using UnityEngine;
using System.Collections;

public class Selector : Composite {
    public Selector(BlackBoard blackboard, string taskName = "") : base(blackboard, taskName) {
        
    }

    protected override void ChildSucceeded() {
        this.controller.FinishWithSuccess();
    }

    protected override void ChildFailed() {
        this.controller.index++;

        if (this.controller.index < this.controller.taskList.Count) {
            this.controller.currTask = this.controller.taskList[this.controller.index];
        } else
            this.controller.FinishWithFailure();
    }
}
