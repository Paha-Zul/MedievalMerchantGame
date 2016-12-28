using UnityEngine;
using System.Collections;

[System.Serializable]
public class Task {
    public BlackBoard bb { get; private set; }

    [System.NonSerialized]
    private TaskController control;

    public string taskName = "Task";

    public virtual TaskController controller { get { return control; } protected set { control = value; } }

    public Task(BlackBoard blackboard, string taskName = "") {
        this.bb = blackboard;
        this.taskName = this.GetType().Name;
        if (!taskName.Equals(""))
            this.taskName += " - " + taskName;
    }

    public virtual void Start() {

    }

    public virtual void Update(float delta) {

    }

    public virtual void End() {

    }

    public virtual void Reset()
    {

    }

    public override string ToString() {
        return this.taskName;
    }
}
