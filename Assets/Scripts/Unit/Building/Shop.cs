using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using BehaviourTree;

[RequireComponent(typeof(Workers))]
public class Shop : Building
{

    // Use this for initialization
	void Start ()
	{
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

        foreach (var worker in Workers.WorkerUnitList)
        {
            if (worker.MyUnit.manager.idle) {
                worker.MyUnit.manager.currTask = Tasks.SellItemFromStore(worker.MyUnit.manager.bb);
            }
        }
    }
}
