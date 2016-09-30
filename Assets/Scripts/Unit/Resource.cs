using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(Health))]
public class Resource : MonoBehaviour {
    public List<GameObject> regularObjects;
    public GameObject depletedObject;

    public int maxHarvesters { get; private set; }
    public string resourceName = "Minerals";
    public string harvestedItemName = "Mineral";
    public float harvestTime = 2;

    private int harvestAmount = 10;
    public int currHarvesters = 0;

    private Health health;

    public void Start() {
        health = GetComponent<Health>();
        maxHarvesters = 1;

        if (this.transform.childCount < 1) {
            var normal = Instantiate(regularObjects[UnityEngine.Random.Range(0, regularObjects.Count)], Vector3.zero, Quaternion.identity) as GameObject;
            normal.transform.parent = this.transform;
            normal.transform.localPosition = new Vector3(0, 0, 0);
        }else {
            DestroyImmediate(this.transform.GetChild(0).gameObject);
            var normal = Instantiate(regularObjects[UnityEngine.Random.Range(0, regularObjects.Count)], Vector3.zero, Quaternion.identity) as GameObject;
            normal.transform.parent = this.transform;
            normal.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void Update() {
        //Mesh mesh = this.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;
        //Vector3[] vertices = mesh.vertices;
        //Vector3[] normals = mesh.normals;
        //int i = 0;
        //while (i < vertices.Length) {
        //    vertices[i] += normals[i] * Mathf.Sin(Time.time) * 0.00001f;
        //    i++;
        //}
        //mesh.vertices = vertices;
    }

    public int Harvest() {
        int harvestAmount = this.harvestAmount;
        this.health.currHealth -= harvestAmount;

        if (this.health.currHealth <= 0) {
            harvestAmount += (int)this.health.currHealth;
            DepleteResource();
        }

        return harvestAmount;
    }

    /// <summary>
    /// Checks if a harvester can be added.
    /// </summary>
    /// <returns>True if a harvester can be added, false otherwise.</returns>
    public bool CanAddHarvester() {
        return currHarvesters < maxHarvesters;
    }

    /// <summary>
    /// Attempts to add a harvester.
    /// </summary>
    /// <returns>True if added, false otherwise</returns>
    public bool AddHarvester() {
        if (CanAddHarvester()) {
            currHarvesters++;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Attempts to remove a harvester.
    /// </summary>
    /// <returns>true</returns>
    public bool RemoveHarvester() {
        currHarvesters--;
        if (currHarvesters < 0) currHarvesters = 0;
        return true;
    }

    private void DepleteResource() {
        this.maxHarvesters = 0;
        Destroy(this.transform.GetChild(0).gameObject);
        var depleted = Instantiate(depletedObject, Vector3.zero, Quaternion.identity) as GameObject;
        depleted.transform.parent = this.transform;
        depleted.transform.localPosition = new Vector3(0, 0, 0);
    }
}
