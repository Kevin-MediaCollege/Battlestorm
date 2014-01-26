using UnityEngine;
using System.Collections;

public class BridgeManager:MonoBehaviour {
	private static BridgeManager instance = null;
	
 	public bool first;

	public int goldCostFirst;
	public int stoneCostFirst;
	public int woodCostFirst;
	
	public int goldCostOther;
	public int stoneCostOther;
	public int woodCostOther;

	public int GoldCost() {
		return first ? goldCostFirst : goldCostOther;
	}

	public int StoneCost() {
		return first ? stoneCostFirst : stoneCostOther;
	}

	public int WoodCost() {
		return first ? woodCostFirst : woodCostOther;
	}

	public static BridgeManager Instance {
		get {
			if(instance == null) {
				instance = FindObjectOfType(typeof(BridgeManager)) as BridgeManager;
			}
			
			if(instance == null) {
				GameObject go = new GameObject("BridgeManager");
				instance = go.AddComponent(typeof(BridgeManager)) as BridgeManager;
			}
			
			return instance;
		}
	}
	
	void OnApplicationQuit() {
		instance = null;
	}
}
