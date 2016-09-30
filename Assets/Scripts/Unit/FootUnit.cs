using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(BehaviourManager))]
[RequireComponent(typeof(PlayerTeam))]
public class FootUnit : MonoBehaviour {
    public BehaviourManager manager { get; private set; }
    public PlayerTeam playerTeam { get; private set; }
    public Inventory inventory { get; private set; }

    public string unitName = "Such";

    public void Awake() {
        this.tag = "Foot";
        this.playerTeam = this.GetComponent<PlayerTeam>();
        this.manager = this.GetComponent<BehaviourManager>();
        this.inventory = this.GetComponent<Inventory>();

        manager.init();

        manager.bb.myInventory = this.inventory;
        manager.bb.myself = this.gameObject;
    }

    public void Start() {
        
    }

    public void Update() {
        if (manager.CurrTaskDone()) {
            GetHarvestResourceTask();
            this.manager.onCompletionCallback = GetHarvestResourceTask;
        }
    }

    void GetHarvestResourceTask() {
        var task = new Selector(manager.bb);
        var sequence = new Sequence(manager.bb);
        var building = this.playerTeam.team.buildingList[0];

        sequence.controller.AddTask(new GetClosestResource(manager.bb));
        sequence.controller.AddTask(new CheckHasValidResource(manager.bb));
        sequence.controller.AddTask(new MoveTo(manager.bb));
        sequence.controller.AddTask(new HarvestTask(manager.bb));
        sequence.controller.AddTask(new MoveTo(manager.bb, building.transform.position));

        var idle = new IdleTask(manager.bb, 1f);

        task.controller.AddTask(sequence);
        task.controller.AddTask(idle);

        manager.currTask = task;
    }
}
