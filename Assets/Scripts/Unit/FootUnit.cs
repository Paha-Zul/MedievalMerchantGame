using UnityEngine;
using System.Collections;
using System;
using Util;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(BehaviourManager))]
[RequireComponent(typeof(PlayerTeam))]
public class FootUnit : MonoBehaviour {
    
    public Unit MyUnit { get; private set; }

    public string UnitName = "Such";

    public PathNode CurrPathNode = null;

    public void Awake() {
        this.tag = "Foot";
        this.MyUnit = this.GetComponent<Unit>();
    }

    public void Start() {
        this.MyUnit.manager.bb.myFootUnit = this;
    }

    public void Update() {
        if (this.MyUnit.manager.CurrTaskDone()) {
            GetHarvestResourceTask();
            this.MyUnit.manager.onCompletionCallback = GetHarvestResourceTask;
        }
    }

    void GetHarvestResourceTask() {
        var task = new Selector(this.MyUnit.manager.bb);
        var sequence = new Sequence(this.MyUnit.manager.bb);
        var building = this.MyUnit.playerTeam.team.buildingList[0];

        sequence.controller.AddTask(new GetClosestResource(this.MyUnit.manager.bb));
        sequence.controller.AddTask(new CheckHasValidResource(this.MyUnit.manager.bb));
        sequence.controller.AddTask(new MoveToNavmesh(this.MyUnit.manager.bb));
        sequence.controller.AddTask(new HarvestTask(this.MyUnit.manager.bb));
        sequence.controller.AddTask(new MoveToNavmesh(this.MyUnit.manager.bb, building.transform.position));

        var idle = new IdleTask(this.MyUnit.manager.bb, 1f);

        task.controller.AddTask(sequence);
        task.controller.AddTask(idle);

        this.MyUnit.manager.currTask = task;
    }
}
