using UnityEngine;
using System.Collections;

public class Stockpile : Building {
    ItemStorage itemStorageScript;

	// Use this for initialization
	void Start () {
        base.Start();

        var storage = transform.FindChild("ItemStorage");
        this.itemStorageScript = storage.GetComponent<ItemStorage>();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
