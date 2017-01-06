using UnityEngine;
using System.Collections;
using Util;

/// <summary>
/// A move behaviour that uses a list of waypoints to move. Does not use Unity's navmesh.
/// </summary>
public class MoveTo : LeafTask {
    private Transform[] _waypoints;
    private int _currWaypoint = 0;
    private float _speed;
    readonly float _dstToStop = 0.1f;
    private NavMeshAgent _agent;
    private FootUnit _myFootUnit;

    /// <summary>
    /// Manually moves the unity along waypoints. Optionally can take an array of waypoints.
    /// </summary>
    /// <param name="bb"></param>
    /// <param name="waypoints">Optional array of transforms (waypoints)</param>
    public MoveTo(BlackBoard bb, Transform[] waypoints = null) : base(bb) {
        this._waypoints = waypoints;
    }

    public override void Start() {
        base.Start();

        this._myFootUnit = bb.myUnit.GetComponent<FootUnit>();

        if (_waypoints == null)
            this._waypoints = bb.waypoints;

        _agent = _myFootUnit.GetComponent<NavMeshAgent>();
        _speed = _agent.speed;
        _agent.enabled = false;
//        _agent.Stop();
//        _agent.updatePosition = false;
    }

    public override void Update(float delta) {
        base.Update(delta);

        if (Vector3.Distance(_myFootUnit.transform.position, this._waypoints[_currWaypoint].position) <= _dstToStop)
        {
            _myFootUnit.CurrPathNode = _waypoints[_currWaypoint].GetComponent<PathNode>();
            _currWaypoint++;
            if (_currWaypoint >= this._waypoints.Length) {
                this.controller.FinishWithSuccess();
            }
        } else {
            var move = this._speed * delta;
            var newPos = Vector3.MoveTowards(_myFootUnit.transform.position, this._waypoints[_currWaypoint].position, move);
            _myFootUnit.transform.position = newPos;
        }
    }
}
