using UnityEngine;
using System.Collections;

public class MerchantSpawner : MonoBehaviour {
    public GameObject merchantPrefab;
    public float[] spawnTimeRange = new float[] { 0f, 1f };
    float nextSpawnTime = 0f;

	// Use this for initialization
	void Start () {
        nextSpawnTime = Time.time + Random.Range(spawnTimeRange[0], spawnTimeRange[1]);
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time >= nextSpawnTime) {
            nextSpawnTime = Time.time + Random.Range(spawnTimeRange[0], spawnTimeRange[1]);
            var merchant = GameObject.Instantiate(merchantPrefab, GameMainScript.enterExitSpots[0].transform.position, Quaternion.identity) as GameObject;
            TeamManager.GetTeam("Player1").addFootUnit(merchant.GetComponent<FootUnit>());

            var unit = merchant.GetComponent<Unit>();
            unit.inventory.AddItem("Gold Coin", Random.Range(50, 500));
        }
	}
}
