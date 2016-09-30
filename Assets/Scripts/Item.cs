using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item{
    public string name;
    public int amount;

    public Item(string name, int amount) {
        this.name = name;
        this.amount = amount;
    }
}
