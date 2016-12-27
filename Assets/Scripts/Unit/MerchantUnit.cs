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

        this.myUnit.manager.currTask = Tasks.BuyItem(this.myUnit.manager.bb, "Wood Plank", 10);
	}
	
	// Update is called once per frame
	void Update () {

        if (this.myUnit.manager.CurrTaskDone()) {
            this.myUnit.manager.currTask = Tasks.BuyItem(this.myUnit.manager.bb, "Wood Plank", 10);
        }
	}
}
