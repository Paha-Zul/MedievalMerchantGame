using UnityEngine;
using System.Collections;
using Util;

public class BlackBoard {
    public GameObject myself;
    public Inventory myInventory;
    public WorkerUnit myWorkerUnit;
    public Unit myUnit;
    public FootUnit myFootUnit;

    public Transform[] waypoints;
    public Transform targetPosition;
    public Building targetBuilding;
    public Inventory targetInventory;
    public Resource targetResource;
    public FootUnit targetFootUnit;

    public PathNode TargetStartNode, TargetEndNode;

    public Item targetItem = new Item("", 0);

    public bool QueueFlag = false;
	
    public BlackBoard() {

    }
}
