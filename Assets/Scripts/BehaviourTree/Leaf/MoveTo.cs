using UnityEngine;
using System.Collections;
using Util;

/// <summary>
/// A move behaviour that uses a list of waypoints to move. Does not use Unity's navmesh.
/// </summary>
public class MoveTo : LeafTask {
    Transform[] waypoints;
    int currWaypoint = 0;
    float speed;
    float dstToStop = 0.1f;
    NavMeshAgent agent;
    private FootUnit myFootUnit;

    /// <summary>
    /// Manually moves the unity along waypoints. Optionally can take an array of waypoints.
    /// </summary>
    /// <param name="bb"></param>
    /// <param name="waypoints">Optional array of transforms (waypoints)</param>
    public MoveTo(BlackBoard bb, Transform[] waypoints = null) : base(bb) {
        this.waypoints = waypoints;
    }

    public override void Start() {
        base.Start();

        this.myFootUnit = bb.myUnit.GetComponent<FootUnit>();

        if (waypoints == null)
            this.waypoints = bb.waypoints;

        agent = myFootUnit.GetComponent<NavMeshAgent>();
        speed = agent.speed;
        agent.Stop();
        agent.updatePosition = false;
    }

    public override void Update(float delta) {
        base.Update(delta);

        if (Vector3.Distance(myFootUnit.transform.position, this.waypoints[currWaypoint].position) <= dstToStop)
        {
            myFootUnit.CurrPathNode = waypoints[currWaypoint].GetComponent<PathNode>();
            currWaypoint++;
            if (currWaypoint >= this.waypoints.Length) {
                this.controller.FinishWithSuccess();
            }
        } else {
            var move = this.speed * delta;
            var newPos = Vector3.MoveTowards(myFootUnit.transform.position, this.waypoints[currWaypoint].position, move);
            myFootUnit.transform.position = newPos;
        }
    }
}
