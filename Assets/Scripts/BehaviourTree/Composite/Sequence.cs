using UnityEngine;
using System.Collections;
using System;

public class Sequence : Composite {

    public Sequence(BlackBoard blackboard, string taskName = "") : base(blackboard, taskName) {

    }

    public override void Start() {
        base.Start();
    }

    public override void Update(float delta) {
        base.Update(delta);
    }

    public override void End() {
        base.End();
    }

    protected override void ChildFailed() {
        Debug.Log("Sequence failed on " + this.controller.currTask.taskName);
        this.controller.FinishWithFailure();
        this.controller.currTask.controller.SafeEnd();
    }

    protected override void ChildSucceeded() {
        this.controller.index++;

        if (this.controller.index < this.controller.taskList.Count) {
            this.controller.currTask = this.controller.taskList[this.controller.index];
        } else
            this.controller.FinishWithSuccess();
    }
}
