using UnityEngine;
using System.Collections;

public class TaskController {

    public bool success { get; private set; }
    public bool failed { get; private set; }
    public bool running { get; private set; }

    public Task task { get; private set; }

    public TaskController(Task task) {
        this.task = task;
    }

    /// <summary>
    /// Safely starts the TaskController and its task. Sets the task as running.
    /// </summary>
    public void SafeStart() {
        this.running = true;
        this.task.Start();
    }

    /// <summary>
    /// Safely ends the TaskController and its task. Sets the task as not running.
    /// </summary>
    public void SafeEnd() {
        this.running = false;
        this.task.End();
    }

    /// <summary>
    /// Sets failed as true. Ends this Task.
    /// </summary>
    public void FinishWithFailure() {
        this.failed = true;
        this.SafeEnd();
    }

    /// <summary>
    /// Sets success as true. Ends this Task.
    /// </summary>
    public void FinishWithSuccess() {
        this.success = true;
        this.SafeEnd();
    }

    /// <summary>
    /// Resets the TaskController and its task
    /// </summary>
    public virtual void Reset() {
        this.success = this.failed = this.running = false;
        this.task.Reset();
    }
}
