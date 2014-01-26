using UnityEngine;
using System.Collections;

public class Bridge:MonoBehaviour {


	public IslandData[] adjacentIslands; // The Islands adjacent to the Bridge.
	
	public GameObject spawnposition; // The spawnposition for the BridgeParts.
	
	public int bridgeParts; // Amount of bridgeparts used to build the bridge.

	public bool isMade; // Check whether the Bridge has been made.
	
	public LineRenderer[] ray; // The Ray of light above the Bridge.

	void Start() {
		//Create the bridge on Start of the scene.
		if(isMade)
			BuildFree();
	}
	public void SetRay(bool state){
		//Sets the state of the Ray of light above the Bridge.

		if(ray.Length != 0){
			ray[0].enabled = state;
			ray[1].enabled = state;
		}
	}

	public void Build() {

		SetRay(false);

		for(int i = 0; i < bridgeParts; i++) {
			StartCoroutine(SpawnBridgePart(0.2f * i,i));
		}

		PlayerData.Instance.goldAmount -= BridgeManager.Instance.first ? BridgeManager.Instance.goldCostFirst : BridgeManager.Instance.goldCostOther;
		PlayerData.Instance.stoneAmount -= BridgeManager.Instance.first ? BridgeManager.Instance.stoneCostFirst : BridgeManager.Instance.stoneCostOther;
		PlayerData.Instance.woodAmount -= BridgeManager.Instance.first ? BridgeManager.Instance.woodCostFirst : BridgeManager.Instance.woodCostOther;

		BridgeManager.Instance.first = false;
		
		isMade = true;

		Unlock();
	}

	IEnumerator SpawnBridgePart(float delay, int i){
		//Creates a BridgePart at said position.

		yield return new WaitForSeconds(delay);

		Vector3 spawnpos = spawnposition.transform.position + (spawnposition.transform.forward * 0.5f);

		GameObject part = Instantiate(Resources.Load("Prefabs/Buildings/Bridge/BridgePart"), spawnpos,transform.rotation) as GameObject;
		part.GetComponent<AudioSource>().Play();
		part.transform.rotation = spawnposition.transform.rotation;
		part.transform.position += spawnposition.transform.forward * i;
		part.transform.parent = spawnposition.transform;
		part.name = "BridgePart";

		Instantiate(Resources.Load("Particles/BridgePoof"), part.transform.position,part.transform.rotation);
	}

	public void BuildFree() {
		//Creates the bridge without a cost.

		for(int i = 0; i < bridgeParts; i++) {

			Vector3 spawnpos = spawnposition.transform.position + (spawnposition.transform.forward * 0.5f);

			GameObject part = Instantiate(Resources.Load("Prefabs/Buildings/Bridge/BridgePart"), spawnpos,transform.rotation) as GameObject;
			part.transform.rotation = spawnposition.transform.rotation;
			part.transform.position += spawnposition.transform.forward * i;
			part.GetComponentInChildren<QuickDestroy>().killdirect = true;
			part.transform.parent = spawnposition.transform;
			part.name = "BridgePart";

		}

		isMade = true;
		Unlock();

	}
	
	void Unlock(){
		//Unlocks the Islands adjacent to the bridge.

		for (int i = 0; i < adjacentIslands.Length; i++) {
			adjacentIslands[i].unlockIsland(true);
		}

	}

}
