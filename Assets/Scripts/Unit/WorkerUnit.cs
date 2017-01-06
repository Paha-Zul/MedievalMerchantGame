using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Util;

public class WorkerUnit : FootUnit {
    public Building MyBuilding;

    void Awake() {
        base.Awake();
    }

    // Use this for initialization
    void Start () {
        base.Start();

        this.MyUnit.manager.bb.myWorkerUnit = this;
        StartCoroutine(FindMyBuilding());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator FindMyBuilding() {
        var foundBuilding = false;
        var wait = new WaitForSeconds(0.5f);
        while(!foundBuilding) {
            yield return wait;
            if (MyBuilding == null) {
                var building = Finder.FindClosestBuildingOfType<Building>(TeamManager.GetTeam("Player1").buildingList, this.transform.position);
                if (building != null)
                {
                    var workers = building.GetComponent<Workers>();
                    if (workers == null) continue;

                    this.MyBuilding = building;
                    workers.AddWorker(this);
                }
            } else
                foundBuilding = true;
        }
    }
}
