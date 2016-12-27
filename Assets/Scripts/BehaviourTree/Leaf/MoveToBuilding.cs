using UnityEngine;
using System.Collections;

public class MoveToBuilding : LeafTask {
    Vector3 targetPosition;
    Vector3? _incomingBuildingPosition = null;
    NavMeshAgent agent;

    /// <summary>
    /// Moves using Unity's navmesh agent. Moves to the bb.targetBuilding unless specified otherwise.
    /// </summary>
    /// <param name="bb"></param>
    /// <param name="targetBuilding">An optional building to specify. If not provided, uses the blackboard's target building.</param>
    public MoveToBuilding(BlackBoard bb, Building targetBuilding = null) : base(bb) {
        if (targetBuilding != null)
            _incomingBuildingPosition = targetBuilding.transform.position;
    }

    public override void Start() {
        base.Start();
        this.targetPosition = _incomingBuildingPosition ?? this.bb.targetBuilding.transform.position;

        agent = bb.myself.GetComponent<NavMeshAgent>();
        agent.destination = this.targetPosition;
        agent.Resume();
    }

    public override void Update(float delta) {
        base.Update(delta);

        if (Vector3.Distance(bb.myself.transform.position, this.targetPosition) <= 0.8f) {
            this.controller.FinishWithSuccess();
            agent.Stop();
            return;
        } else {
            //bb.myself.transform.position = Vector3.MoveTowards(bb.myself.transform.position, targetPosition, 0.1f);
        }
    }
}
