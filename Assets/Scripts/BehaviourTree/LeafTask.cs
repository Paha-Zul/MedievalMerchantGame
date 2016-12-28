using UnityEngine;
using System.Collections;

public class LeafTask : Task{
    public new TaskController controller { get { return base.controller as TaskController; } private set { base.controller = value; } }

    public LeafTask(BlackBoard blackboard) : base(blackboard) {
        this.controller = new TaskController(this);
    }
}