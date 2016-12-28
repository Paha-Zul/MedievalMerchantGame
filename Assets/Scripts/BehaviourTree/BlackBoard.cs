using UnityEngine;
using System.Collections;

public class BlackBoard {
    public GameObject myself;
    public Inventory myInventory;
    public WorkerUnit myWorkerUnit;
    public Unit myUnit;
    public FootUnit myFootUnit;

    public Transform[] waypoints;
    public Vector3 targetPosition = new Vector3();
    public Building targetBuilding;
    public Inventory targetInventory;
    public Resource targetResource;
    public FootUnit targetFootUnit;
    public Item targetItem = new Item("", 0);

    public bool QueueFlag = false;
	
    public BlackBoard() {

    }
}
