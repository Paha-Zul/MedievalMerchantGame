namespace BehaviourTree.Leaf
{
    /// <summary>
    /// Simply sets the blackboard.myWorkerUnit's flag (AtWorkStation) to true.
    /// </summary>
    public class SetAtWorkStation : LeafTask
    {
        public SetAtWorkStation(BlackBoard blackboard) : base(blackboard)
        {
        }

        public override void Start()
        {
            base.Start();
//            bb.myWorkerUnit.AtWorkStation = true;
            this.controller.FinishWithSuccess();
        }
    }
}