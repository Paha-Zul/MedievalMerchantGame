

class RepeatUntilFail : Decorator {
    public RepeatUntilFail(BlackBoard blackboard, Task taskToDecorate, string taskName = "") : base(blackboard, taskToDecorate, taskName) {
    }

    public override void Update(float delta) {
        base.Update(delta);

        if (TaskToDecorate.controller.success)
            TaskToDecorate.controller.Reset();
    }
}
