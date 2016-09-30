using UnityEngine;
using System.Collections;
using System;

public class BehaviourManager : MonoBehaviour {
    private Task _currTask;

    public BlackBoard bb { get; private set; }

    public bool idle
    {
        get
        {
            return _currTask == null || !_currTask.controller.running;
        }
    }

    public string[] currTaskName = new string[10];

    /// <summary>
    /// Sets the current task. Handles resetting and starting the task.
    /// </summary>
    public Task currTask { get
        {
            return _currTask;
        }
        set
        {
            _currTask = value;
            _currTask.controller.reset();
            _currTask.controller.SafeStart();
        }
    }

    public delegate void Callback(); //Defines the callback delegate
    public Callback onCompletionCallback { get; set; } //The Callback variable.

    public void init() {
        this.bb = new BlackBoard();
        this.bb.myself = this.gameObject;
        onCompletionCallback = () => { return; };
    }

	// Update is called once per frame
	void Update () {
        if (!CurrTaskDone()) {
            this._currTask.Update(0.016f);
            this.currTaskName = this._currTask.ToString().Split(',');
            if (!this._currTask.controller.running) {
                onCompletionCallback();
            }
        }
	}

    public bool CurrTaskDone() {
        return currTask == null || !currTask.controller.running;
    }
}
