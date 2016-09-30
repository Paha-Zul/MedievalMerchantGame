using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public int maxItemTypes = 1;
    public int maxNumItems = 999999;

    [SerializeField]
    private int _numItemsInInventory = 0;
    public int numItemsInInventory { get { return _numItemsInInventory; } private set { _numItemsInInventory = value; } }

    public Dictionary<string, Item> itemMap = new Dictionary<string, Item>();

    public delegate void InventoryChangeCallback(int amtChange, bool newlyAdded, bool newlyRemoved); //Defines the callback delegate
    public InventoryChangeCallback onInventoryChange { get; set; }   //The Callback variable. Set this to an empty function by default

    public List<Item> testItemList = new List<Item>();

    // Use this for initialization
    void Start () {
        onInventoryChange = (amt, added, removed) => { return; };

        testItemList.ForEach(item => {
            this.AddItem(item.name, item.amount);
        });
    }

    public bool AddItem(string name, int amount) {
        if(CanAddItem(name, amount)) {
            bool newlyAdded = false;
            Item item;
            if (!itemMap.TryGetValue(name, out item)) {
                item = new Item(name, amount);
                itemMap.Add(name, item);
                newlyAdded = true;
            } else {
                item.amount += amount;
            }
            //TODO Account for the limit of items when addding? IE: we arleady have 90 and max is 100 but we're trying to add 20?
            onInventoryChange(amount, newlyAdded, false);
            numItemsInInventory += amount;
            return true;
        }

        return false;
    }

    public bool AddItem(Item item) {
        //TODO Item is discarded here, maybe pooling if needed?
        return this.AddItem(item.name, item.amount);
    }

    /// <summary>
    /// Removes a certain amount from an Item in the inventory. If it takes all of the Item, the Item is removed.
    /// </summary>
    /// <param name="name">The name of the Item.</param>
    /// <param name="amount">The amount of the Item to remove.</param>
    /// <returns>The amount actually removed from the Item.</returns>
    public int RemoveItemAmount(string name, int amount) {
        int amountRemoved = 0;
        Item item;
        if (itemMap.TryGetValue(name, out item)) {
            bool newlyRemoved = false;
            amountRemoved = amount; //Initially set this
            item.amount -= amount; //Subtract the amount from the item.
            //If it brought us below zero, add the amount removed to the negative amount in the item. This gives us what we actually COULD take.
            if (item.amount < 0) {
                amountRemoved += item.amount;
                itemMap.Remove(name); //Remove this from the map since we have no more.
                newlyRemoved = true;
            }

            numItemsInInventory -= amountRemoved;
            onInventoryChange(amountRemoved, false, newlyRemoved);
        }

        return amountRemoved;
     }

    /// <summary>
    /// Checks if an Item can be added to this inventory.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool CanAddItem(string name = "", int amount = 1) {
        var hasItem = itemMap.ContainsKey(name);
        var hasEnoughSpace = numItemsInInventory + amount <= maxNumItems;
        bool hasEnoughTypeSpace = true;

        if (!hasItem) hasEnoughTypeSpace = itemMap.Count + 1 <= maxItemTypes;

        return hasEnoughSpace && hasEnoughTypeSpace;
    }

    /// <summary>
    /// Checks if the Inventory has an Item by name.
    /// </summary>
    /// <param name="name">The name of the Item.</param>
    /// <returns>True if the Item exists, false otherwise.</returns>
    public bool HasItem(string name) {
        return itemMap.ContainsKey(name);
    }

    /// <summary>
    /// Gets the amount of an Item. If 0, the item does not exist in the Inventory.
    /// </summary>
    /// <param name="name">The name of the Item.</param>
    /// <returns>The amount of the item, or 0 if it does not exist.</returns>
    public int GetItemAmount(string name) {
        if (HasItem(name))
            return itemMap[name].amount;

        return 0;
    }
}
