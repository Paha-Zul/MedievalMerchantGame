using UnityEngine;
using System.Collections;

public class MoveToBuilding : LeafTask {
    Vector3 targetPosition;
    Vector3? _incomingBuildingPosition = null;

    /// <summary>
    /// A Move To task. Optionally can take a target position instead of using the BlackBoard's value.
    /// </summary>
    /// <param name="bb"></param>
    /// <param name="targetPosition"></param>
    public MoveToBuilding(BlackBoard bb, Building targetBuilding = null) : base(bb) {
        if (targetBuilding != null)
            _incomingBuildingPosition = targetBuilding.transform.position;
    }

    public override void Start() {
        base.Start();
        this.targetPosition = _incomingBuildingPosition ?? this.bb.targetBuilding.transform.position;

        NavMeshAgent agent = bb.myself.GetComponent<NavMeshAgent>();
        agent.destination = this.targetPosition;
    }

    public override void Update(float delta) {
        base.Update(delta);

        if (Vector3.Distance(bb.myself.transform.position, this.targetPosition) <= 0.8f) {
            this.controller.FinishWithSuccess();
            return;
        } else {
            //bb.myself.transform.position = Vector3.MoveTowards(bb.myself.transform.position, targetPosition, 0.1f);
        }
    }
}
