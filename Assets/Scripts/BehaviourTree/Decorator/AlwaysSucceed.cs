using UnityEngine;

namespace Assets.Scripts.BehaviourTree.Decorator
{
    public class AlwaysSucceed : global::Decorator
    {
        public AlwaysSucceed(BlackBoard blackboard, Task taskToDecorate, string taskName = "") : base(blackboard, taskToDecorate, taskName)
        {

        }

        public override void Update(float delta) {
            base.Update(delta);

            if (TaskToDecorate.controller.running) return;

            this.controller.FinishWithSuccess();
        }
    }
}