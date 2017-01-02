using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using Util;

public class WorkShop : Building {
    public enum WorkshopType { Lumber }
    public WorkshopType workshopType = WorkshopType.Lumber;

    private List<WorkerUnit> workerUnitList = new List<WorkerUnit>();

    public int itemsProducedAtATime = 20;
    public string producesItem = "Wood Plank";

	// Use this for initialization
	void Start () {
        base.Start();

        StartCoroutine(AssignWorkersCoroutine());

	    //TESTING
	    var entrance = EntranceSpots[0];
	    var work = WorkSpots[0];

	}

    // Update is called once per frame
    void Update () {
        
    }

    public void AddWorker(WorkerUnit worker) {
        this.workerUnitList.Add(worker);
    }

    public void RemoveWorker(WorkerUnit worker) {
        this.workerUnitList.Remove(worker);
    }

    IEnumerator AssignWorkersCoroutine() {
        var wait = new WaitForSeconds(1f);

        for (;;) {
            AssignWorkers();
            yield return wait;
        }
        
    }

    void AssignWorkers()
    {
        var count = workerUnitList.Count;
        switch (count)
        {
            case 1:
            {
                var worker = workerUnitList[0];
                if (worker.MyUnit.manager.idle) {
                    worker.MyUnit.manager.currTask = Tasks.WorkAtWorkshop(worker.MyUnit.manager.bb, true, true);
                }
            }
                break;
            case 2:
            {
                var worker = workerUnitList[0];
                if (worker.MyUnit.manager.idle) {
                    worker.MyUnit.manager.currTask = Tasks.WorkAtWorkshop(worker.MyUnit.manager.bb, false, true);
                }

                var worker2 = workerUnitList[1];
                if (worker2.MyUnit.manager.idle) {
                    worker2.MyUnit.manager.currTask = Tasks.HaulTask(worker2.MyUnit.manager.bb, "Wood Log");
                }
            }
                break;
            case 3:
            {
                var worker = workerUnitList[0];
                if (worker.MyUnit.manager.idle) {
                    worker.MyUnit.manager.currTask = Tasks.WorkAtWorkshop(worker.MyUnit.manager.bb, false, false);
                }

                var worker2 = workerUnitList[1];
                if (worker2.MyUnit.manager.idle) {
                    worker2.MyUnit.manager.currTask = Tasks.HaulTask(worker2.MyUnit.manager.bb, "Wood Log");
                }

                var worker3 = workerUnitList[2];
                if (worker3.MyUnit.manager.idle) {
                    worker3.MyUnit.manager.currTask = Tasks.SellItemFromStore(worker3.MyUnit.manager.bb);
                }
            }
                break;
        }
    }

}
