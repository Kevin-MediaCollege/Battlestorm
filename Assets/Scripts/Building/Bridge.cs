using UnityEngine;
using System.Collections;

public class Bridge:MonoBehaviour {
	public IslandData[] adjacentIslands;
	
	public GameObject spawnposition;
	
	public int bridgeParts;

	public bool isMade;
	public int finishedpieces;
	public LineRenderer[] ray;
	void Start() {
		if(isMade)
			BuildFree();
	}
	public void SetRay(bool state){
		if(ray.Length != 0){
			ray[0].enabled = state;
			ray[1].enabled = state;
		}
	}
	public void Build() {
		SetRay(false);
		for(int i = 0; i < bridgeParts; i++) {
			StartCoroutine(spawnbuildpart(0.2f * i,i));
		}

		PlayerData.Instance.goldAmount -= BridgeManager.Instance.first ? BridgeManager.Instance.goldCostFirst : BridgeManager.Instance.goldCostOther;
		PlayerData.Instance.stoneAmount -= BridgeManager.Instance.first ? BridgeManager.Instance.stoneCostFirst : BridgeManager.Instance.stoneCostOther;
		PlayerData.Instance.woodAmount -= BridgeManager.Instance.first ? BridgeManager.Instance.woodCostFirst : BridgeManager.Instance.woodCostOther;

		BridgeManager.Instance.first = false;
		
		isMade = true;
		Unlock();
	}
	IEnumerator spawnbuildpart(float delay, int i){
		yield return new WaitForSeconds(delay);
		Vector3 spawnpos = spawnposition.transform.position + (spawnposition.transform.forward * 0.5f);
		GameObject part = Instantiate(Resources.Load("Prefabs/Buildings/Bridge/BridgePart"), spawnpos,transform.rotation) as GameObject;
		part.GetComponent<AudioSource>().Play();
		part.transform.rotation = spawnposition.transform.rotation;
		part.transform.position += spawnposition.transform.forward * i;
		Instantiate(Resources.Load("Particles/BridgePoof"), part.transform.position,part.transform.rotation);
		part.name = "BridgePart";
		part.transform.parent = spawnposition.transform;
	}
	public void BuildFree() {
		for(int i = 0; i < bridgeParts; i++) {
			Vector3 spawnpos = spawnposition.transform.position + (spawnposition.transform.forward * 0.5f);
			GameObject part = Instantiate(Resources.Load("Prefabs/Buildings/Bridge/BridgePart"), spawnpos,transform.rotation) as GameObject;
			part.transform.rotation = spawnposition.transform.rotation;
			part.transform.position += spawnposition.transform.forward * i;
			part.GetComponentInChildren<QuickDestroy>().killdirect = true;
			part.name = "BridgePart";
			part.transform.parent = spawnposition.transform;
		}

		isMade = true;
		Unlock();
	}
	
	void Unlock(){
		for (int i = 0; i < adjacentIslands.Length; i++) {
			adjacentIslands[i].unlockIsland(true);
		}
	}
}
