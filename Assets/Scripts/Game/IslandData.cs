using UnityEngine;
using System.Collections;

public class IslandData:MonoBehaviour {
	public BuildingManager[] plots;

	public bool isUnlocked;
	public bool canHaveMine;
	public bool canHaveLumberMill;
	public bool canHaveTower;

	void Start() {

		for(int i = 0; i < plots.Length; i++)
			plots[i].isUnlocked = isUnlocked;
	}
	
	public void unlockIsland(bool b) {
		if(!isUnlocked){
			isUnlocked = b;

			for(int i = 0; i < plots.Length; i++){
				plots[i].isUnlocked = b;
				plots[i].transform.FindChild("platform").renderer.enabled = true;
			}
		}
	}
}
