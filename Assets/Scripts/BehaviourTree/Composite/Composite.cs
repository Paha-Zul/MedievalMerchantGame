using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Composite : Task {
    public new CompositeController controller { get { return base.controller as CompositeController; } private set { base.controller = value; } }

    public Composite(BlackBoard blackboard, string taskName = "") : base(blackboard, taskName){
        this.controller = new CompositeController(this);
    }

    public override void Start() {
        base.Start();

        if (controller.taskList.Count == 0)
            this.controller.FinishWithFailure();
        else
            this.controller.currTask = this.controller.taskList[0];
    }

    public override void Update(float delta) {
        base.Update(delta);

        if (this.controller.currTask == null || !this.controller.running) {
            this.controller.FinishWithFailure();
            return;
        }

        //If the current task is not running, go to the next task.
        if (!this.controller.currTask.controller.running) {
            //If we are out of tasks, end this task!
            if (this.controller.currTask.controller.failed)
                this.ChildFailed();
            else if (this.controller.currTask.controller.success)
                this.ChildSucceeded();
            else
                this.controller.currTask.controller.SafeStart();
        }

        //If the current task is running, update it!
        if (this.controller.currTask.controller.running) {
            this.controller.currTask.Update(delta);
        }
    }

    /// <summary>
    /// Logic to handle when a child fails.
    /// </summary>
    protected virtual void ChildFailed() { }

    /// <summary>
    /// Logic to handle when a child succeeds.
    /// </summary>
    protected virtual void ChildSucceeded() { }

    public override string ToString() {
        var childName = "";
        if (this.controller.currTask != null)
            childName = this.controller.currTask.ToString();

        var name = this.taskName + "," + childName;
        return name;
    }
}
