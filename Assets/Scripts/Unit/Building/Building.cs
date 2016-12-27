using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerTeam))]
public class Building : MonoBehaviour {
    public Unit myUnit { get; private set; }

    public Queue<FootUnit> unitQueue = new Queue<FootUnit>();

    public List<Transform> walkSpots = new List<Transform>();
    public List<Transform> deliverySpots = new List<Transform>();
    public List<Transform> workSpots = new List<Transform>();
    public List<Transform> entranceSpots = new List<Transform>();
    public List<Transform> sellSpots = new List<Transform>();

    public Task baseTask;

    public void Awake() {
        this.tag = "Building";
        this.myUnit = this.GetComponent<Unit>();

        var walkspots = transform.FindChild("WalkSpots");
        if (walkspots != null) {
            foreach (Transform child in walkspots)
                this.walkSpots.Add(child);
        }
        
        //Get all of the walk spots
        foreach(Transform child in transform) {
            if (child.name.Equals("DeliverySpot"))
                deliverySpots.Add(child);
            else if(child.name.Equals("WorkSpot"))
                workSpots.Add(child);
            else if (child.name.Equals("EntranceSpot"))
                entranceSpots.Add(child);
        }
    }

    public void Start() {

    }


    public void Update() {

    }
}
