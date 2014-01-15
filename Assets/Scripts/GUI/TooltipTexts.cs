using UnityEngine;
using System.Collections;

public class TooltipTexts:MonoBehaviour {
	private static TooltipTexts instance = null;

	public string build_tower_default = "You can build towers\nfrom this menu";
	public string build_tower_normal = "Build a normal tower";
	public string build_tower_ice = "Build an ice tower\nSlows enemies down";
	public string build_tower_fire = "Build a fire tower\nSets enemies on fire";
	public string build_mine = "Builds a new Mine";
	public string build_lumbermill = "Builds a new Lumber\nMill";
	public string build_bridge = "Builds a new bridge";
	public string build_not_available = "Insufficient resources";
	public string build_not_available_island = "This building can't\n be placed here";

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
