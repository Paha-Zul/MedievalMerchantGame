using UnityEngine;

namespace Assets.Scripts.BehaviourTree.Leaf
{
    public class WaitTimeOrCondition : LeafTask
    {

        public delegate bool Predicate();

        private readonly Predicate _condition;
        private readonly float _timeToWait;

        private float _endTime = 0f;

        public WaitTimeOrCondition(BlackBoard blackboard,float timeToWait, Predicate condition) : base(blackboard)
        {
            this._condition = condition;
            _timeToWait = timeToWait;
        }

        public override void Start()
        {
            base.Start();

            this._endTime = Time.time + _timeToWait;
        }

        public override void Update(float delta)
        {
            base.Update(delta);

            //Whether our condition was satisfied or we ran out of time, finish with success
            if (Time.time >= this._endTime || _condition())
            {
                this.controller.FinishWithSuccess();
            }

        }

        public override void End()
        {
            base.End();
            this.bb.QueueFlag = false; //Reset the flag
        }
    }
}