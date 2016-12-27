using UnityEngine;
using System.Collections;
using System;
using Util;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(BehaviourManager))]
[RequireComponent(typeof(PlayerTeam))]
public class FootUnit : MonoBehaviour {
    
    public Unit myUnit { get; private set; }

    public string unitName = "Such";

    [HideInInspector] public PathNode CurrPathNode = null;

    public void Awake() {
        this.tag = "Foot";
        this.myUnit = this.GetComponent<Unit>();
    }

    public void Start() {
        this.myUnit.manager.bb.myFootUnit = this;
    }

    public void Update() {
        if (this.myUnit.manager.CurrTaskDone()) {
            GetHarvestResourceTask();
            this.myUnit.manager.onCompletionCallback = GetHarvestResourceTask;
        }
    }

    void GetHarvestResourceTask() {
        var task = new Selector(this.myUnit.manager.bb);
        var sequence = new Sequence(this.myUnit.manager.bb);
        var building = this.myUnit.playerTeam.team.buildingList[0];

        sequence.controller.AddTask(new GetClosestResource(this.myUnit.manager.bb));
        sequence.controller.AddTask(new CheckHasValidResource(this.myUnit.manager.bb));
        sequence.controller.AddTask(new MoveToNavmesh(this.myUnit.manager.bb));
        sequence.controller.AddTask(new HarvestTask(this.myUnit.manager.bb));
        sequence.controller.AddTask(new MoveToNavmesh(this.myUnit.manager.bb, building.transform.position));

        var idle = new IdleTask(this.myUnit.manager.bb, 1f);

        task.controller.AddTask(sequence);
        task.controller.AddTask(idle);

        this.myUnit.manager.currTask = task;
    }
}
