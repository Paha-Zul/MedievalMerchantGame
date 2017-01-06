using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemStorage : MonoBehaviour {
    public string[] excludeItemList = new string[] { "Gold Coin" };
    public GameObject storageItem;

    ItemStorageSpot[] itemStorageSpots;

    Dictionary<string, LinkedList<ItemStack>> stackMap = new Dictionary<string, LinkedList<ItemStack>>();
    HashSet<string> excludeItems = new HashSet<string>();

    void Awake() {
        foreach (var name in excludeItemList)
            excludeItems.Add(name);

        //Calculates the local scale of the item we will spawn. This would be the scale of the item if we were to parent it under this item storage object.
        var localScale = new Vector3(storageItem.transform.lossyScale.x / this.transform.localScale.x, storageItem.transform.lossyScale.y / this.transform.localScale.y,
            storageItem.transform.lossyScale.z / this.transform.localScale.z);

        int rows = (int)(transform.lossyScale.x / storageItem.transform.lossyScale.x);
        int cols = (int)(transform.lossyScale.z / storageItem.transform.lossyScale.z);
        float rowSpace = ((transform.lossyScale.x - rows* storageItem.transform.lossyScale.x)/(rows + 1))/transform.lossyScale.x;
        float colSpace = ((transform.lossyScale.z - cols * storageItem.transform.lossyScale.z) / (cols + 1)) / transform.lossyScale.z;

        itemStorageSpots = new ItemStorageSpot[rows * cols];

        var startX = -0.5f + localScale.x / 2f;
        var startZ = -0.5f + localScale.z / 2f;

        int row = 0;
        int col = 0;
        for (int i = 0; i < itemStorageSpots.Length; i++) {
            row = i % rows;
            col = i / rows;

            var pos = new Vector3(startX + row * rowSpace + row * localScale.x, transform.position.y - transform.lossyScale.y, startZ + col * colSpace + col * localScale.z);
            itemStorageSpots[i] = new ItemStorageSpot(pos, "", 10, 20);
        }

        var inventory = this.transform.parent.GetComponent<Inventory>();
        inventory.onInventoryChange = (name, amt, newlyChanged) => {
            if (amt > 0) {
                this.AddItem(name, amt);
            } else if (amt < 0) {
                this.RemoveItem(name, -amt); //Negate the amt here since it comes in as a negative
            }
        };
    }

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    public void AddItem(string itemName, int amount) {
        if (excludeItems.Contains(itemName))
            return;

        LinkedList<ItemStack> stackList;
        stackMap.TryGetValue(itemName, out stackList);

        ItemStorageSpot spot = null;
        ItemStack stack;

        //If the list doesn't exist, add a new list and a new stack.
        if (stackList == null) {
            stackList = new LinkedList<ItemStack>();
            stack = new ItemStack();
            stackList.AddLast(stack);
            stackMap.Add(itemName, stackList);
            AddNewSpot(stack);
        }

        if(stackList.Count <= 0) {
            stack = new ItemStack();
            stackList.AddLast(stack);
            AddNewSpot(stack);
        }

        //Get the last stack in the list...
        stack = stackList.Last.Value;
        spot = stack.spot;
        int amountCanAdd = spot.stackAmount - stack.amount; //Calculate the amount we can add
        int amountToAdd = Mathf.Min(amountCanAdd, amount);
        int leftover = (stack.amount + amount) - spot.stackAmount; //Calculate the leftover

        stack.amount += amountToAdd; //Add the amount to the current spot.

        //While we have leftovers, keep making new spots
        while (leftover > 0) {
            stack = new ItemStack();
            spot = AddNewSpot(stack);
            if (spot == null) break;

            stackList.AddLast(stack);
            stack.amount += Mathf.Min(spot.stackAmount, leftover);

            leftover -= spot.stackAmount; //We can simply use the stack amount from the spot since this is our full 'potential' amount.
        }
    }

    public void RemoveItem(string itemName, int amount) {
        if (excludeItems.Contains(itemName))
            return;

        LinkedList<ItemStack> stackList;
        stackMap.TryGetValue(itemName, out stackList);

        //If the list doesn't exist, add a new list and a new stack.
        if (stackList == null) {
            Debug.Log("[ItemStorage] Failed to remove item from graphical storage with name " + itemName + ", no list exists.");
            return;
        }

        //Get the last stack in the list...
        var stack = stackList.Last.Value;
        int amountCanRemove = stack.spot.stackAmount; //We can always remove the whole stack from the spot.
        int amountToRemove = Mathf.Min(amountCanRemove, amount); //Either we can remove the amount needed or the total amount we can remove from the stack.
        int leftover = amount - stack.amount; //Calculate the leftover

        stack.amount -= amountToRemove; //Add the amount to the current spot.

        //While we have leftovers, keep removing from spots.
        while (leftover >= 0) {
            ClearSpot(stack.spot); //Remove the current spot
            stackList.RemoveLast(); //Remove the last spot;
            if (stackList.Count <= 0)
                break;

            stack = stackList.Last.Value; //Get the next stack down;

            stack.amount -= Mathf.Min(leftover, stack.spot.stackAmount); //Remove an amount

            leftover -= stack.spot.stackAmount; //We can simply use the stack amount from the spot since this is our full 'potential' amount.
        }
    }

    ItemStorageSpot GetNextOpenSpot() {
        foreach(var spot in itemStorageSpots) {
            if (spot.itemStack == null)
                return spot;
        }

        return null;
    }

    ItemStorageSpot AddNewSpot(ItemStack stack) {
        var spot = GetNextOpenSpot();

        //TODO Need to deal with overflow here. We gotta keep track of the overflow!
        if (spot == null) return null;

        spot.cube = Instantiate(storageItem, spot.spot, transform.rotation) as GameObject;
        spot.cube.transform.parent = this.transform;
        spot.cube.transform.localPosition = spot.spot;
        spot.itemStack = stack;
        stack.spot = spot;
        return spot;
    }

    void ClearSpot(ItemStorageSpot spot) {
        Destroy(spot.cube);
        spot.itemStack = null; //Must clear this!! IMPORTANT!!
    }

    class ItemStorageSpot {
        public Vector3 spot;
        public ItemStack itemStack;
        public string itemName;
        public int storageAmount;
        public int stackAmount;

        public GameObject cube;

        public ItemStorageSpot(Vector3 spot, string itemName, int storageAmount, int stackAmount) {
            this.spot = spot;
            this.itemName = itemName;
            this.storageAmount = storageAmount;
            this.stackAmount = stackAmount;
        }
    }

    class ItemStack {
        public ItemStorageSpot spot;
        public string name;
        public int amount;
    }
}
