using UnityEngine;
using System.Collections;

public class Bridge:MonoBehaviour {
	public IslandData[] adjacentIslands;
	
	public GameObject spawnposition;
	
	public int bridgeParts;

	public bool isMade;
	
	public void Build() {
		for(int i = 0; i < bridgeParts; i++) {
			Vector3 spawnpos = spawnposition.transform.position + (spawnposition.transform.forward * 0.5f);
			GameObject part = Instantiate(Resources.Load("Prefabs/Buildings/Bridge/BridgePart"), spawnpos,transform.rotation) as GameObject;
			
			part.transform.rotation = spawnposition.transform.rotation;
			part.transform.position += spawnposition.transform.forward * i;
			
			part.name = "BridgePart";
			part.transform.parent = spawnposition.transform;
		}

		PlayerData.Instance.goldAmount -= BridgeManager.Instance.first ? BridgeManager.Instance.goldCostFirst : BridgeManager.Instance.goldCostOther;
		PlayerData.Instance.stoneAmount -= BridgeManager.Instance.first ? BridgeManager.Instance.stoneCostFirst : BridgeManager.Instance.stoneCostOther;
		PlayerData.Instance.woodAmount -= BridgeManager.Instance.first ? BridgeManager.Instance.woodCostFirst : BridgeManager.Instance.woodCostOther;
		
		BridgeManager.Instance.first = false;
		
		isMade = true;
		Unlock();
	}
	
	void Unlock(){
		for (int i = 0; i < adjacentIslands.Length; i++) {
			adjacentIslands [i].unlockIsland (true);
		}
	}
}
