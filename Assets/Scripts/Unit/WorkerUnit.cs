using UnityEngine;
using System.Collections;
using Util;

public class WorkerUnit : FootUnit {
    public WorkShop myBuilding;



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
        bool foundBuilding = false;
        var wait = new WaitForSeconds(0.5f);
        while(!foundBuilding) {
            yield return wait;
            if (myBuilding == null) {
                var workshop = Finder.FindClosestBuildingOfType<WorkShop>(TeamManager.GetTeam("Player1").buildingList, this.transform.position);
                if (workshop != null) {
                    this.myBuilding = workshop;
                    workshop.AddWorker(this);
                }
            } else
                foundBuilding = true;
        }
    }
}
