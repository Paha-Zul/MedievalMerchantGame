using UnityEngine;
using System.Collections;

public class BlackBoard {
    public GameObject myself;
    public Inventory myInventory;
    public WorkerUnit myWorkerUnit;

    public Vector3 targetPosition = new Vector3();
    public Resource targetResource;
    public Building targetBuilding;
    public Inventory targetInventory;
    public Item targetItem = new Item("", 0);
	
    public BlackBoard() {

    }
}
