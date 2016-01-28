using UnityEngine;
using System.Collections;

public class IslandData:MonoBehaviour {

	public BuildingManager[] plots; //Plots that are on the Island.

	public Bridge[] bridges; // Bridges atached to the Island.

	public bool isUnlocked; // Check whether Island has been unlocked.

	public bool canHaveMine; // If you can build a mine on this Island.

	public bool canHaveLumberMill; // If you can build a lumbermill on this Island.

	public bool canHaveTower; // If you can build a Tower on this Island.

    public EBuildingType[] allowedBuildings;

	void Start() {

		for(int i = 0; i < plots.Length; i++){
			plots[i].isUnlocked = isUnlocked;
		}

	}

	IEnumerator unlockPlot(int i,float delay,bool b){

		yield return new WaitForSeconds(delay);

		plots[i].SetParticle(true);
		plots[i].isUnlocked = b;
		plots[i].transform.FindChild("platform").GetComponent<Renderer>().enabled = true;
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
