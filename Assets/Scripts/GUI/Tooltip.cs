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

	private string type;
	private string text;

	private bool drawGUI;

	void Update() {
		mousePos = new Vector2(Input.mousePosition.x,(Screen.height - Input.mousePosition.y));
	}

	public void drawTooltip(string type, bool state) {		
		switch(type) {
		case "TowerMenu":
			this.type = type;
			text = TooltipTexts.Instance.build_tower_default;
			break;
		case "TowerNormal":
			unloadGUI();

			this.type = type;
			text = TooltipTexts.Instance.build_tower_normal;
			
			if(stats == null) stats = (Instantiate(Resources.Load("Prefabs/Buildings/TowerNormal")) as GameObject).GetComponent<BuildingStats>();
			break;
		case "TowerIce":
			unloadGUI();

			this.type = type;
			text = TooltipTexts.Instance.build_tower_ice;
			
			if(stats == null) stats = (Instantiate(Resources.Load("Prefabs/Buildings/TowerIce")) as GameObject).GetComponent<BuildingStats>();
			break;
		case "TowerFire":
			unloadGUI();

			this.type = type;
			text = TooltipTexts.Instance.build_tower_fire;
			
			if(stats == null) stats = (Instantiate(Resources.Load("Prefabs/Buildings/TowerFire")) as GameObject).GetComponent<BuildingStats>();
			break;
		case "Mine":
			this.type = type;
			text = TooltipTexts.Instance.build_mine;
			
			if(stats == null) stats = (Instantiate(Resources.Load("Prefabs/Buildings/Mine")) as GameObject).GetComponent<BuildingStats>();
			break;
		case "LumberMill":
			this.type = type;
		 	text = TooltipTexts.Instance.build_lumbermill;
			
			if(stats == null) stats = (Instantiate(Resources.Load("Prefabs/Buildings/LumberMill")) as GameObject).GetComponent<BuildingStats>();
			break;
		case "Bridge":
			this.type = type;
			text = TooltipTexts.Instance.build_bridge;
			break;
		case "NotAvailable":
			this.type = type;
			text = TooltipTexts.Instance.build_not_available;
			break;
		case "NotAvailableIsland":
			this.type = type;
			text = TooltipTexts.Instance.build_not_available;
			break;
		case "NotAvailableBridge":
			this.type = type;
			text = TooltipTexts.Instance.build_not_available;
			break;
		}

		drawGUI = state;
	}
	public void unloadGUI() {
		type = "";
		text = "";
		
		drawGUI = false;
		
		if(stats != null) Destroy(stats.gameObject);
	}

	void OnGUI() {
		if(drawGUI) {
			GUI.depth = -1000;
			GUI.Label(new Rect(mousePos.x + 8, mousePos.y - 75, 200, 100), text, tooltipStyle);

			switch(type) {
			case "TowerNormal":
				TooltipTower();
				break;
			case "TowerIce":
				TooltipTower();
				break;
			case "TowerFire":
				TooltipTower();
				break;
			case "Mine":
				TooltipMine();
				break;
			case "LumberMill":
				TooltipLumberMill();
				break;
			case "Bridge":
				TooltipBridge();
				break;
			case "NotAvailable":
				if(stats != null) DrawPricing(stats.goldCostPerLevel[0].ToString(), stats.stoneCostPerLevel[0].ToString(), stats.woodCostPerLevel[0].ToString());
				break;
			case "NotAvailableBridge":
				DrawPricing(BridgeManager.Instance.GoldCost().ToString(), BridgeManager.Instance.StoneCost().ToString(), BridgeManager.Instance.WoodCost().ToString());
				break;
			}
		}
	}

	private void DrawPricing(string goldCost, string stoneCost, string woodCost) {
		GUI.DrawTexture(new Rect(mousePos.x + 120, mousePos.y - 30, 24, 24), goldTexture); 
		GUI.Label(new Rect(mousePos.x + 139, mousePos.y - 30, 200, 100), goldCost, tooltipTextStyle);
		
		GUI.DrawTexture(new Rect(mousePos.x + 14, mousePos.y - 6, 24, 24), stoneTexture); 
		GUI.Label(new Rect(mousePos.x + 33, mousePos.y - 6, 200, 100), stoneCost, tooltipTextStyle);
		
		GUI.DrawTexture(new Rect(mousePos.x + 120, mousePos.y - 6, 24, 24), woodTexture); 
		GUI.Label(new Rect(mousePos.x + 139, mousePos.y - 6, 200, 100), woodCost, tooltipTextStyle);
	}

	private void TooltipTower() {
		DrawPricing(stats.goldCostPerLevel[0].ToString(), stats.stoneCostPerLevel[0].ToString(), stats.woodCostPerLevel[0].ToString());
	}

	private void TooltipMine() {
		DrawPricing(stats.goldCostPerLevel[0].ToString(), stats.stoneCostPerLevel[0].ToString(), stats.woodCostPerLevel[0].ToString());
	}

	private void TooltipLumberMill() {
		DrawPricing(stats.goldCostPerLevel[0].ToString(), stats.stoneCostPerLevel[0].ToString(), stats.woodCostPerLevel[0].ToString());
	}

	private void TooltipBridge() {
		DrawPricing(BridgeManager.Instance.GoldCost().ToString(), BridgeManager.Instance.StoneCost().ToString(), BridgeManager.Instance.WoodCost().ToString());
	}
}
