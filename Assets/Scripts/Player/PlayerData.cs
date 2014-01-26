using UnityEngine;
using System.Collections;

public class PlayerData:MonoBehaviour {
	private static PlayerData instance = null;
	
	public int woodAmount; //Amount of wood you currently have.
	public int stoneAmount; // Amount of stone you currently have.
	public int goldAmount; // Amount of gold you currently have.
	
	public int health; // Health of your Base.

	public static PlayerData Instance {
		get {
			if(instance == null) {
				instance = FindObjectOfType(typeof(PlayerData)) as PlayerData;
			}

			if(instance == null) {
				GameObject go = new GameObject("PlayerData");
				instance = go.AddComponent(typeof(PlayerData)) as PlayerData;
			}

			return instance;
		}
	}

	void OnApplicationQuit() {
		instance = null;
	}
}
