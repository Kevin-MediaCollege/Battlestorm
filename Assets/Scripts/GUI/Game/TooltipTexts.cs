using UnityEngine;
using System.Collections;

public class TooltipTexts:MonoBehaviour {
	//Text Being used in the Tooltips.

	private static TooltipTexts instance = null;

	public string build_tower_default = "";

	public string build_tower_normal = "Deals damage to enemies.";

	public string build_tower_ice = "Slows enemies down.";

	public string build_tower_fire = "Damages enemies over time.";

	public string build_mine = "Gathers stone resources.";

	public string build_lumbermill = "Gathers wood resources.";

	public string build_bridge = "Unlocks a new island.";

	public string build_not_available = "Insufficient resources";

	public string build_not_available_island = "This building\ncan't be placed here";

	public static TooltipTexts Instance {
		get {
			if(instance == null) {
				instance = FindObjectOfType(typeof(TooltipTexts)) as TooltipTexts;
			}
			
			if(instance == null) {
				GameObject go = new GameObject("TooltipTexts");
				instance = go.AddComponent(typeof(TooltipTexts)) as TooltipTexts;
			}
			
			return instance;
		}
	}
	
	void OnApplicationQuit() {
		instance = null;
	}
}
