using UnityEngine;
using System.Collections;

public class IslandData : MonoBehaviour {
	public BuildingManager[] plots;
	public bool isUnlocked;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < plots.Length; i++){
			plots[i].isUnlocked = isUnlocked;
		}
	}
	
	public void unlockIsland(bool b){
		isUnlocked = b;
		for(int i = 0; i < plots.Length; i++){
			Debug.Log(i);
			plots[i].isUnlocked = b;
		}
	}
}
