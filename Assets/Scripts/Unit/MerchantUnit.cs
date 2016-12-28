using UnityEngine;
using System.Collections;
using BehaviourTree;

public class MerchantUnit : FootUnit {

    void Awake() {
        base.Awake();
    }

    // Use this for initialization
    void Start () {
        base.Start();

        this.MyUnit.manager.currTask = Tasks.BuyItem(this.MyUnit.manager.bb, "Wood Plank", 10);
	}
	
	// Update is called once per frame
	void Update () {

        if (this.MyUnit.manager.CurrTaskDone()) {
            this.MyUnit.manager.currTask = Tasks.BuyItem(this.MyUnit.manager.bb, "Wood Plank", 10);
        }
	}
}
