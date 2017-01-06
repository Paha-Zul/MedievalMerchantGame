using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {
    public bool Debug = false;
    public int MaxItemTypes = 1;
    public int MaxNumItems = 999999;

    [SerializeField]
    private int _numItemsInInventory = 0;
    public int NumItemsInInventory { get { return _numItemsInInventory; } private set { _numItemsInInventory = value; } }

    public Dictionary<string, Item> itemMap = new Dictionary<string, Item>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">The item itemName.</param>
    /// <param name="amtChange">The amount changed. Positive means added, negative means removed.</param>
    /// <param name="newlyChanged">If the item was new... ie: Added as a new item to the inventory (didn't exist before), just entirely removed from the inventory.</param>
    public delegate void InventoryChangeCallback(string name, int amtChange, bool newlyChanged);
    public InventoryChangeCallback onInventoryChange = (name, amt, newlyChanged) => { return; };   //The Callback variable. Set this to an empty function by default

    public List<Item> TestItemList = new List<Item>();
    public List<Item> DebugItemList = new List<Item>();

    // Use this for initialization
    void Start () {
        TestItemList.ForEach(item => {
            this.AddItem(item.Name, item.Amount);
        });
    }

    public bool AddItem(string name, int amount) {
        if (amount <= 0) return false;
        if(CanAddItem(name, amount)) {
            bool newlyAdded = false;
            Item item;
            if (!itemMap.TryGetValue(name, out item)) {
                item = new Item(name, amount);
                itemMap.Add(name, item);
                newlyAdded = true;
            } else {
                item.Amount += amount;
            }

            //TODO Account for the limit of items when addding? IE: we arleady have 90 and max is 100 but we're trying to add 20?
            onInventoryChange(name, amount, newlyAdded);
            NumItemsInInventory += amount;
            if (Debug) DebugItemList = itemMap.Values.ToList();
            return true;
        }

        return false;
    }

    public bool AddItem(Item item) {
        //TODO Item is discarded here, maybe pooling if needed?
        return this.AddItem(item.Name, item.Amount);
    }

    /// <summary>
    /// Removes a certain amount from an Item in the inventory. If it takes all of the Item, the Item is removed.
    /// </summary>
    /// <param name="itemName">The itemName of the Item.</param>
    /// <param name="amount">The amount of the Item to remove.</param>
    /// <returns>The amount actually removed from the Item.</returns>
    public int RemoveItemAmount(string itemName, int amount) {
        var amountRemoved = 0;
        Item item;

        if (!itemMap.TryGetValue(itemName, out item)) return amountRemoved;

        var newlyRemoved = false;
        amountRemoved = amount; //Initially set this
        item.Amount -= amount; //Subtract the amount from the item.
        //If it brought us below zero, add the amount removed to the negative amount in the item. This gives us what we actually COULD take.
        if (item.Amount <= 0) {
            amountRemoved += item.Amount;
            itemMap.Remove(itemName); //Remove this from the map since we have no more.
            newlyRemoved = true;
        }

        NumItemsInInventory -= amountRemoved;
        onInventoryChange(itemName, -amountRemoved, newlyRemoved);
        if (Debug) DebugItemList = itemMap.Values.ToList();

        return amountRemoved;
     }

    /// <summary>
    /// Checks if an Item can be added to this inventory.
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool CanAddItem(string itemName = "", int amount = 1) {
        var hasItem = itemMap.ContainsKey(itemName);
        var hasEnoughSpace = NumItemsInInventory + amount <= MaxNumItems;
        var hasEnoughTypeSpace = true;

        if (!hasItem) hasEnoughTypeSpace = itemMap.Count + 1 <= MaxItemTypes;

        return hasEnoughSpace && hasEnoughTypeSpace;
    }

    /// <summary>
    /// Checks if the Inventory has an Item by itemName.
    /// </summary>
    /// <param name="itemName">The itemName of the Item.</param>
    /// <returns>True if the Item exists, false otherwise.</returns>
    public bool HasItem(string itemName) {
        return itemMap.ContainsKey(itemName);
    }


    /// <summary>
    /// Gets the amount of an Item. If 0, the item does not exist in the Inventory.
    /// </summary>
    /// <param name="name">The itemName of the Item.</param>
    /// <returns>The amount of the item, or 0 if it does not exist.</returns>
    public int GetItemAmount(string name) {
        if (HasItem(name))
            return itemMap[name].Amount;

        return 0;
    }
}
