using UnityEngine;
using System.Collections;
using Assets.Scripts.Util;

/// <summary>
/// A Component attached to buildings that are able to sell items from it.
/// </summary>
public class SellItems : MonoBehaviour {
    public bool debug = false;
    public bool sellingItems;

    public ItemCostPair[] itemsBeingSold = new ItemCostPair[0];

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
