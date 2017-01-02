using UnityEngine;
using System.Collections;

/// <summary>
/// A task to move a Unit.
/// </summary>
public class MoveToNavmesh : LeafTask {
    private Vector3 _targetPosition;
    private readonly Vector3? _incomingPosition;
    private NavMeshAgent _agent;

    /// <summary>
    /// Moves using Unity's navmesh. Moves to the blackboard's target position unless otherwise specified
    /// </summary>
    /// <param name="bb"></param>
    /// <param name="targetPosition"></param>
    public MoveToNavmesh(BlackBoard bb, Vector3? targetPosition = null) : base(bb) {
        _incomingPosition = targetPosition;
    }

    public override void Start() {
        base.Start();
        this.bb.myFootUnit.CurrPathNode = null; //Reset this. Since we are moving on the navmesh it means we're not inside a building or following a path.
        this._targetPosition = _incomingPosition ?? this.bb.targetPosition.position;

        _agent = bb.myself.GetComponent<NavMeshAgent>();
        _agent.destination = this._targetPosition;
        _agent.Resume();
        _agent.updatePosition = true;
    }

    public override void Update(float delta) {
        base.Update(delta);

        if(Vector3.Distance(bb.myself.transform.position, this._targetPosition) <= 0.8f) {
            this.controller.FinishWithSuccess();
            _agent.Stop();
            return;
        }else {
            //bb.myself.transform.position = Vector3.MoveTowards(bb.myself.transform.position, targetPosition, 0.1f);
        }
    }

}
