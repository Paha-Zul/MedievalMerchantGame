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

        StartCoroutine(AssignWorkers());

	    //TESTING
	    var entrance = entranceSpots[0];
	    var work = workSpots[0];

//	    var path = Util.Util.FindPathToNode(entrance.GetComponent<PathNode>(), work.GetComponent<PathNode>());
//	    Debug.Log("Path: " + path);
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

    IEnumerator AssignWorkers() {
        var wait = new WaitForSeconds(1f);

        for (;;) {
            assignWorkers();
            yield return wait;
        }
        
    }

    void assignWorkers() {
        var count = workerUnitList.Count;
        if(count == 1) {
            var worker = workerUnitList[0];
            if (worker.MyUnit.manager.idle) {
                worker.MyUnit.manager.currTask = Tasks.WorkAtWorkshop(worker.MyUnit.manager.bb, true, true);
            }
        }else if(count == 2) {
            var worker = workerUnitList[0];
            if (worker.MyUnit.manager.idle) {
                worker.MyUnit.manager.currTask = Tasks.WorkAtWorkshop(worker.MyUnit.manager.bb, false, true);
            }

            var worker2 = workerUnitList[1];
            if (worker2.MyUnit.manager.idle) {
                worker2.MyUnit.manager.currTask = Tasks.HaulTask(worker2.MyUnit.manager.bb, "Wood Log");
            }
        }
    }

}
