using UnityEngine;
using System.Collections;

public class Bridge:MonoBehaviour {
	private static bool first = false;

	public IslandData[] adjacentIslands;
	
	public GameObject spawnposition;

	[HideInInspector]
	public bool isMade;

	public int bridgeParts;
	
	public int goldCostFirst;
	public int stoneCostFirst;
	public int woodCostFirst;
	
	public int goldCostOther;
	public int stoneCostOther;
	public int woodCostOther;

	public void Build() {
		for(int i = 0; i < bridgeParts; i++) {
			Vector3 spawnpos = spawnposition.transform.position + (spawnposition.transform.forward * 0.5f);
			GameObject part = Instantiate(Resources.Load("Prefabs/Buildings/Bridge/BridgePart"), spawnpos,transform.rotation) as GameObject;

			part.transform.rotation = spawnposition.transform.rotation;
			part.transform.position += spawnposition.transform.forward * i;

			part.name = "BridgePart";
			part.transform.parent = spawnposition.transform;
		}
		
		PlayerData.Instance.goldAmount -= first ? goldCostFirst : goldCostOther;
		PlayerData.Instance.stoneAmount -= first ? stoneCostFirst : stoneCostOther;
		PlayerData.Instance.woodAmount -= first ? woodCostFirst : woodCostOther;
		
		first = false;
		
		isMade = true;
		Unlock();
	}
	
	void Unlock(){
		for(int i = 0; i < adjacentIslands.Length; i++) {
			adjacentIslands[i].unlockIsland(true);
		}
	}
	
	public int GoldCost() {
		return first ? goldCostFirst : goldCostOther;
	}
	
	public int StoneCost() {
		return first ? stoneCostFirst : stoneCostOther;
	}
	
	public int WoodCost() {
		return first ? woodCostFirst : woodCostOther;
	}
}
