using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.MonoBehaviours;
using BehaviourTree;
using Util;
using Util = Util.Util;

[RequireComponent(typeof(Workers))]
[RequireComponent(typeof(ProducesItem))]
public class WorkShop : Building {
    public enum WorkshopType { Lumber }
    public WorkshopType workshopType = WorkshopType.Lumber;

	// Use this for initialization
	void Start () {
        base.Start();

        StartCoroutine(AssignWorkersCoroutine());
	}

    // Update is called once per frame
    void Update () {
        
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
        var count = Workers.WorkerUnitList.Count;
        switch (count)
        {
            case 1:
            {
                var worker = Workers.WorkerUnitList[0];
                if (worker.MyUnit.manager.idle) {
                    worker.MyUnit.manager.currTask = Tasks.WorkAtWorkshop(worker.MyUnit.manager.bb, true, true);
                }
            }
                break;
            case 2:
            {
                var worker = Workers.WorkerUnitList[0];
                if (worker.MyUnit.manager.idle) {
                    worker.MyUnit.manager.currTask = Tasks.WorkAtWorkshop(worker.MyUnit.manager.bb, false, true);
                }

                var worker2 = Workers.WorkerUnitList[1];
                if (worker2.MyUnit.manager.idle) {
                    worker2.MyUnit.manager.currTask = Tasks.HaulTask(worker2.MyUnit.manager.bb, "Wood Log");
                }
            }
                break;
            case 3:
            {
                var worker = Workers.WorkerUnitList[0];
                if (worker.MyUnit.manager.idle) {
                    worker.MyUnit.manager.currTask = Tasks.WorkAtWorkshop(worker.MyUnit.manager.bb, false, false);
                }

                var worker2 = Workers.WorkerUnitList[1];
                if (worker2.MyUnit.manager.idle) {
                    worker2.MyUnit.manager.currTask = Tasks.HaulTask(worker2.MyUnit.manager.bb, "Wood Log");
                }

                var worker3 = Workers.WorkerUnitList[2];
                if (worker3.MyUnit.manager.idle) {
                    worker3.MyUnit.manager.currTask = Tasks.SellItemFromStore(worker3.MyUnit.manager.bb);
                }
            }
                break;
        }
    }

}
