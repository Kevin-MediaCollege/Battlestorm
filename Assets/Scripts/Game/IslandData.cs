using UnityEngine;
using System.Collections;

public class IslandData:MonoBehaviour {
	public BuildingManager[] plots;

	public Bridge[] bridges;

	public bool isUnlocked;
	public bool canHaveMine;
	public bool canHaveLumberMill;
	public bool canHaveTower;

	void Start() {
		for(int i = 0; i < plots.Length; i++)
			plots[i].isUnlocked = isUnlocked;
	}
	IEnumerator unlockPlot(int i,float delay,bool b){
		yield return new WaitForSeconds(delay);
		plots[i].SetParticle(true);
		plots[i].isUnlocked = b;
		plots[i].transform.FindChild("platform").renderer.enabled = true;
		plots[i].CreatePoofParticle();
	}
	public void unlockIsland(bool b) {
		if(!isUnlocked){
			isUnlocked = b;
			for(int i = 0; i <bridges.Length; i++){
				bridges[i].SetRay(true);
			}
			for(int i = 0; i < plots.Length; i++){
				StartCoroutine(unlockPlot(i,0.2f * i,b));
			}
		}
	}

}
