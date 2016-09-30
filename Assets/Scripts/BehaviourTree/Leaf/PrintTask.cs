using UnityEngine;
using System.Collections;
using System;

public class PrintTask : LeafTask {
    string message;

    public PrintTask(BlackBoard blackboard, string message = "test") : base(blackboard) {
        this.message = message;
    }

    public override void Start() {
        base.Start();

        Debug.Log(message);
        this.controller.FinishWithSuccess();
    }
}
