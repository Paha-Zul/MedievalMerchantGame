using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item{
    public string Name;
    public int Amount;

    public Item(string name, int amount) {
        this.Name = name;
        this.Amount = amount;
    }

    public void Set(string name, int amount)
    {
        this.Name = name;
        this.Amount = amount;
    }
}
