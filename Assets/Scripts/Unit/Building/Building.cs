using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerTeam))]
public class Building : MonoBehaviour {
    public string buildingName = "Default_Name";
    public PlayerTeam playerTeam;
    public Inventory inventory;

    public Queue<FootUnit> unitQueue = new Queue<FootUnit>();
    public List<Vector3> walkSpots = new List<Vector3>();

    public Task baseTask;

    public void Awake() {
        this.tag = "Building";
        this.playerTeam = this.GetComponent<PlayerTeam>();
        this.inventory = this.GetComponent<Inventory>();
        
        //Get all of the walk spots
        foreach(Transform child in transform) {
            if (child.name.Equals("WalkSpot"))
                walkSpots.Add(child.position);
        }
    }

    public void Start() {
        
    }


    public void Update() {

    }
}
