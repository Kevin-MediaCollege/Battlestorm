using UnityEngine;
using System.Collections;

public class Tooltip:MonoBehaviour {
	public Texture goldTexture;
	public Texture stoneTexture;
	public Texture woodTexture;

	public GUIStyle tooltipStyle;
	public GUIStyle tooltipTextStyle;

	private Vector2 mousePos;
	
	private BuildingStats stats;
	private string text;

	private bool drawGUI;
	private bool isBridge;
	
	void Update() {
		mousePos = new Vector2(Input.mousePosition.x,(Screen.height - Input.mousePosition.y));
	}

	public void drawTooltip(string type, bool state) {
		isBridge = false;
		
		switch(type) {
		case "Tower":
			text = TooltipTexts.Instance.build_tower;
			
			if(stats == null)
				stats = (Instantiate(Resources.Load("Prefabs/Buildings/Tower")) as GameObject).GetComponent<BuildingStats>();
			break;
		case "Mine":
			text = TooltipTexts.Instance.build_mine;
			
			if(stats == null)
				stats = (Instantiate(Resources.Load("Prefabs/Buildings/Mine")) as GameObject).GetComponent<BuildingStats>();
			break;
		case "LumberMill":
			text = TooltipTexts.Instance.build_lumbermill;
			
			if(stats == null)
				stats = (Instantiate(Resources.Load("Prefabs/Buildings/LumberMill")) as GameObject).GetComponent<BuildingStats>();
			break;
		case "Bridge":
			text = TooltipTexts.Instance.build_bridge;
			isBridge = true;
			break;
		case "NotAvailable":
			text = TooltipTexts.Instance.build_not_available;
			break;
		}

		drawGUI = state;
	}
	public void unloadGUI() {
		text = "";
		
		drawGUI = false;
		
		if(stats != null)
			Destroy(stats.gameObject);
	}

	void OnGUI() {
		if(drawGUI) {
			GUI.depth = -1000;
			GUI.Label(new Rect(mousePos.x + 8, mousePos.y - 75, 200, 100), text, tooltipStyle);
			
			if(!isBridge) {
				GUI.DrawTexture(new Rect(mousePos.x + 120, mousePos.y - 30, 24, 24), goldTexture); 
				GUI.Label(new Rect(mousePos.x + 139, mousePos.y - 30, 200, 100), stats.goldCostPerLevel[0].ToString(), tooltipTextStyle);
				
				GUI.DrawTexture(new Rect(mousePos.x + 14, mousePos.y - 6, 24, 24), stoneTexture); 
				GUI.Label(new Rect(mousePos.x + 33, mousePos.y - 6, 200, 100), stats.stoneCostPerLevel[0].ToString(), tooltipTextStyle);
				
				GUI.DrawTexture(new Rect(mousePos.x + 120, mousePos.y - 6, 24, 24), woodTexture); 
				GUI.Label(new Rect(mousePos.x + 139, mousePos.y - 6, 200, 100), stats.woodCostPerLevel[0].ToString(), tooltipTextStyle);
			} else {
				/*Bridge bridgeManager = (Instantiate(Resources.Load("Prefabs/Buildings/Bridge/BridgePart")) as GameObject).GetComponent<Bridge>();
				
				GUI.DrawTexture(new Rect(mousePos.x + 120, mousePos.y - 30, 24, 24), goldTexture); 
				GUI.Label(new Rect(mousePos.x + 139, mousePos.y - 30, 200, 100), bridgeManager.GoldCost().ToString(), tooltipTextStyle);
				
				GUI.DrawTexture(new Rect(mousePos.x + 14, mousePos.y - 6, 24, 24), stoneTexture); 
				GUI.Label(new Rect(mousePos.x + 33, mousePos.y - 6, 200, 100), bridgeManager.StoneCost().ToString(), tooltipTextStyle);
				
				GUI.DrawTexture(new Rect(mousePos.x + 120, mousePos.y - 6, 24, 24), woodTexture); 
				GUI.Label(new Rect(mousePos.x + 139, mousePos.y - 6, 200, 100), bridgeManager.WoodCost().ToString(), tooltipTextStyle);
				
				Destroy(bridgeManager.gameObject);*/
			}
		}
	}
}
