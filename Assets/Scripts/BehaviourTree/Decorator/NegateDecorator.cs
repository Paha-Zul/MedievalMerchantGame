namespace Assets.Scripts.BehaviourTree.Decorator
{
    public class NegateDecorator : global::Decorator
    {
        public NegateDecorator(BlackBoard blackboard, Task taskToDecorate, string taskName = "") : base(blackboard, taskToDecorate, taskName)
        {

        }

        public override void Update(float delta)
        {
            base.Update(delta);

            if(this.TaskToDecorate.controller.running)
                return;

            if(this.TaskToDecorate.controller.failed)
                this.controller.FinishWithSuccess();
            else
                this.controller.FinishWithFailure();
        }
    }
}