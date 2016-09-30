using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkShop : Building {
    public enum WorkshopType { Lumber }
    public WorkshopType workshopType = WorkshopType.Lumber;

    private List<WorkerUnit> workerUnitList = new List<WorkerUnit>();

    public int itemsProducedAtATime = 20;
    public ItemProduction itemProduction = new ItemProduction("Wood Log", "Wood Plank", 1, 1, 10f);

	// Use this for initialization
	void Start () {
        base.Start();

        StartCoroutine(AssignWorkers());
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
            if (worker.manager.idle) {
                worker.manager.currTask = Tasks.ProduceItemAtWorkshop(worker.manager.bb, true);
            }
        }else if(count == 2) {
            var worker = workerUnitList[0];
            if (worker.manager.idle) {
                worker.manager.currTask = Tasks.ProduceItemAtWorkshop(worker.manager.bb, false);
            }

            var worker2 = workerUnitList[1];
            if (worker2.manager.idle) {
                worker2.manager.currTask = Tasks.HaulTask(worker2.manager.bb);
            }
        }
    }

}
