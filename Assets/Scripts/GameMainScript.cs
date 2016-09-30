using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMainScript : MonoBehaviour {
    [HideInInspector]

    public static List<Resource> resources = new List<Resource>();

    public void Awake() {
        
    }

    // Use this for initialization
    void Start() {
        var res = GameObject.FindGameObjectsWithTag("Resource");
        foreach (GameObject obj in res) {
            resources.Add(obj.GetComponent<Resource>());
        }

        var team = TeamManager.AddNewTeam("Player1");
        team.addBuilding(GameObject.FindGameObjectsWithTag("Building"));
        team.addFootUnit(GameObject.FindGameObjectsWithTag("Foot"));
    }

    // Update is called once per frame
    void Update() {

    }
}
