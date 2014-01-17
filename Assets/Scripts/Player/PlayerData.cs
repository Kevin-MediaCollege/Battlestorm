using UnityEngine;
using System.Collections;

public class PlayerData:MonoBehaviour {
	private static PlayerData instance = null;
	
	public int woodAmount;
	public int stoneAmount;
	public int goldAmount;
	
	public int health;

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
