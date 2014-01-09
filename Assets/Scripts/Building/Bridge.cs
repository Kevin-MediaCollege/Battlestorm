using UnityEngine;
using System.Collections;

public class Bridge:MonoBehaviour {
	public IslandData[] adjacentIslands;

	public GameObject spawnposition;

	public bool isMade;

	public int bridgeParts;
	public int buildCost;

	public void Build() {
		for(int i = 0; i < bridgeParts; i++) {
			Vector3 spawnpos = spawnposition.transform.position + (spawnposition.transform.forward * 0.5f);
			GameObject part = Instantiate(Resources.Load("Prefabs/Buildings/Bridge/BridgePart"), spawnpos,transform.rotation) as GameObject;

			part.transform.rotation = spawnposition.transform.rotation;
			part.transform.position += spawnposition.transform.forward * i;

			part.name = "BridgePart";
			part.transform.parent = spawnposition.transform;
		}

		isMade = true;
		Unlock();
	}
	
	void Unlock(){
		for(int i = 0; i < adjacentIslands.Length; i++) {
			adjacentIslands[i].unlockIsland(true);
		}
	}
}
