using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour {
	public int bridgeParts;
	public GameObject spawnposition;

	public bool beenMade;
	public int buildCost;
	public IslandData[] adjacentIslands;
	// Use this for initialization
	void Start () {
	}
	
	public void BuildBridge(){
		for(int i = 0; i < bridgeParts; i++){
			Vector3 spawnpos = spawnposition.transform.position + (spawnposition.transform.forward * 0.5f);
			GameObject part = Instantiate(Resources.Load("Prefabs/Buildings/Bridge/BridgePart"),spawnpos,transform.rotation) as GameObject;
			part.transform.parent = spawnposition.transform;
			part.transform.rotation = spawnposition.transform.rotation;
			part.name = "BridgePart";
			part.transform.position += spawnposition.transform.forward * i;
		}
		beenMade = true;
		unlockIsland();
	}
	public void unlockIsland(){
		for(int i = 0; i < adjacentIslands.Length; i++){
			adjacentIslands[i].unlockIsland(true);
		}
	}
}
