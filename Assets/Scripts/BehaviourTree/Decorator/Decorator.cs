using UnityEngine;
using System.Collections;

public class Decorator : Task {

    protected Task TaskToDecorate;

    public Decorator(BlackBoard blackboard, Task taskToDecorate, string taskName = "") : base(blackboard, taskName) {
        this.controller = new TaskController(this);
        this.TaskToDecorate = taskToDecorate;
    }

    public override void Start()
    {
        base.Start();
        TaskToDecorate.controller.SafeStart();
    }

    public override void End()
    {
        base.End();
        TaskToDecorate.controller.SafeEnd();
    }

    public override void Update(float delta)
    {
        base.Update(delta);

        this.TaskToDecorate.Update(delta);
    }

    public override string ToString() {
        return base.ToString() + "#" + this.TaskToDecorate.ToString();
    }
}
