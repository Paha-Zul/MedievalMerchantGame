using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class holds information about the team and objects linked to the team, such as buildings and units. This should be expanded
/// upon for things like vision and such.
/// </summary>
public class Player {
    //Buildings
    //Units
    //Resources
    // ?

    public List<Building> buildingList = new List<Building>();
    public List<FootUnit> footUnitList = new List<FootUnit>();

    public string teamName { get; private set; }

    public Player(string name) {
        this.teamName = name;
    }
	
    public void addBuilding(Building building) {
        building.myUnit.playerTeam.team = this;
        buildingList.Add(building);
    }

    public void addBuilding(GameObject[] buildings) {
        foreach (GameObject building in buildings) 
            addBuilding(building.GetComponent<Building>());
    }

    public void addFootUnit(FootUnit unit) {
        unit.myUnit.playerTeam.team = this;
        footUnitList.Add(unit);
    }

    public void addFootUnit(GameObject[] units) {
        foreach (GameObject unit in units) 
            addFootUnit(unit.GetComponent<FootUnit>());
    }

}
