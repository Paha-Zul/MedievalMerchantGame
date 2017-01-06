using UnityEngine;
using System.Collections;
using Util;

/// <summary>
/// A task to move a Unit.
/// </summary>
public class MoveToNavmesh : LeafTask {
    private Vector3 _targetPosition;
    private readonly Vector3? _incomingPosition;
    private NavMeshAgent _agent;
    private PathNode _potentialPathNode;

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
        if (_incomingPosition == null)
        {
            this._targetPosition = this.bb.targetPosition.position;
            this._potentialPathNode = this.bb.targetPosition.GetComponent<PathNode>();
        }
        else
            this._targetPosition = _incomingPosition.Value;

        _agent = bb.myself.GetComponent<NavMeshAgent>();
        _agent.enabled = true;
        _agent.destination = this._targetPosition;
//        _agent.Resume();
//        _agent.updatePosition = true;
    }

    public override void Update(float delta) {
        base.Update(delta);

        if(Vector3.Distance(bb.myself.transform.position, this._targetPosition) <= 0.8f) {
            this.controller.FinishWithSuccess();
            bb.myFootUnit.CurrPathNode = _potentialPathNode;
//            _agent.Stop();
            return;
        }else {
            //bb.myself.transform.position = Vector3.MoveTowards(bb.myself.transform.position, targetPosition, 0.1f);
        }
    }

}
