using UnityEngine;
using System.Collections;

public class IdleTask : LeafTask {
    float counter = 0f;
    float idleTime = 2f;

    public IdleTask(BlackBoard blackboard, float randLow = 2f, float randHigh = 2f) : base(blackboard) {
        this.idleTime = Random.Range(randLow, randHigh);
    }

    public override void Start() {
        base.Start();
    }

    public override void Update(float delta) {
        base.Update(delta);

        counter += delta;
        if (counter >= idleTime)
            this.controller.FinishWithSuccess();
    }
}
