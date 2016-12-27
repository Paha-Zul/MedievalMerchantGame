using UnityEngine;
using System.Collections;


public class Unit : MonoBehaviour {
    public string unitName = "Default_Name";
    public PlayerTeam playerTeam;
    public Inventory inventory;
    public BehaviourManager manager { get; private set; }

    void Awake() {
        this.playerTeam = this.GetComponent<PlayerTeam>();
        this.manager = this.GetComponent<BehaviourManager>();
        this.inventory = this.GetComponent<Inventory>();

        if (!manager) return;

        this.manager.init();

        this.manager.bb.myInventory = this.GetComponent<Inventory>();
        this.manager.bb.myself = this.gameObject;
        this.manager.bb.myUnit = this;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
