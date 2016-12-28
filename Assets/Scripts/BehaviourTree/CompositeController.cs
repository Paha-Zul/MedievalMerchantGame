using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompositeController : TaskController {
    public List<Task> taskList;
    public Task currTask = null;
    public int index = 0;

    public CompositeController(Task task) : base(task) {
        this.taskList = new List<Task>();
    }

    public Task AddTask(Task task) {
        taskList.Add(task);
        return task;
    }

    public override void Reset() {
        base.Reset();
        this.currTask = null;
        this.index = 0;
    }

}
