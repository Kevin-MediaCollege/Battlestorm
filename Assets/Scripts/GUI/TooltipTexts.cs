using UnityEngine;
using System.Collections;

public class TooltipTexts:MonoBehaviour {
	private static TooltipTexts instance = null;
	
	public string build_tower = "Builds a new Tower";
	public string build_mine = "Builds a new Mine";
	public string build_lumbermill = "Builds a new Lumber\nMill";
	public string build_bridge = "Builds a new bridge";
	public string build_not_available = "You can't build this building here";

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
