using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class DataDefs {
    public static Dictionary<string, ItemDef> itemDefMap = new Dictionary<string, ItemDef>();
    public static Dictionary<string, ProductionDef> prodDefMap = new Dictionary<string, ProductionDef>();

    public static void init() {

        LoadItemDefs();
        LoadProductionDef();

        Debug.Log("Defs loaded");
    }

    private static void LoadItemDefs() {
        //Load the Json file out of the resources folder
        var json = Resources.Load(Path.Combine("Data", "ItemDef")) as TextAsset;

        //We can't have an array of json objects like in Java. Top level has to be a single object.
        var itemDefs = JsonUtility.FromJson<itemDefWrapper>(json.text);

        //For each ItemDef, load it into the dictionary!
        foreach (var def in itemDefs.items) {
            itemDefMap.Add(def.name, def);
        }
    }

    private static void LoadProductionDef() {
        //Load the Json file out of the resources folder
        var json = Resources.Load(Path.Combine("Data", "ItemProdDef")) as TextAsset;

        //We can't have an array of json objects like in Java. Top level has to be a single object.
        var prodDefs = JsonUtility.FromJson<ProductionDefWrapper>(json.text);

        //For each ItemDef, load it into the dictionary!
        foreach (var def in prodDefs.productions) {
            prodDefMap.Add(def.outputItem, def);
        }
    }

    [System.Serializable]
    struct itemDefWrapper {
        public ItemDef[] items;
    }

    [System.Serializable]
    public struct ItemDef {
        public string name;
        public int storageStackSize;
    }

    [System.Serializable]
    struct ProductionDefWrapper {
        public ProductionDef[] productions;
    }

    [System.Serializable]
    public struct ProductionDef {
        public string inputItem, outputItem;
        public int inputAmount, outputAmount;
        public float baseProdTime;
    }

}
