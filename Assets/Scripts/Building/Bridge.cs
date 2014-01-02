using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour {

	//Amount of Pieces the bridge is made from.
	public int bridgeParts;
	//Spawn position of the first BridgePart.
	public GameObject spawnposition;

	//Check whether it's been made.
	public bool beenMade;
	//The cost of the Bridge.
	public int buildCost;
	//The Island's adjacent to the bridge.
	public IslandData[] adjacentIslands;
	// Use this for initialization.
	void Start () {
	}
	
	public void BuildBridge(){
		for(int i = 0; i < bridgeParts; i++){
			Vector3 spawnpos = spawnposition.transform.position + (spawnposition.transform.forward * 0.5f);
			GameObject part = Instantiate(Resources.Load("Prefabs/Buildings/Bridge/BridgePart"),spawnpos,transform.rotation) as GameObject;
			part.transform.rotation = spawnposition.transform.rotation;
			part.transform.position += spawnposition.transform.forward * i;

			//Keeping the Heirachy clean.
			part.name = "BridgePart";
			part.transform.parent = spawnposition.transform;
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
