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
		case "TowerNormalNotAvailable":
			unloadGUI();

			this.type = "TowerNormal";
			text = TooltipTexts.Instance.build_tower_normal;
			SetStats();
			break;
		case "TowerIce":
		case "TowerIceNotAvailable":
			unloadGUI();

			this.type = "TowerIce";
			text = TooltipTexts.Instance.build_tower_ice;
			SetStats();
			break;
		case "TowerFire":
		case "TowerFireNotAvailable":
			unloadGUI();

			this.type = "TowerFire";
			text = TooltipTexts.Instance.build_tower_fire;
			SetStats();
			break;
		case "Mine":
		case "MineNotAvailable":
			this.type = "Mine";
			text = TooltipTexts.Instance.build_mine;
			SetStats();
			break;
		case "LumberMill":
		case "LumberMillNotAvailable":
			this.type = "LumberMill";
		 	text = TooltipTexts.Instance.build_lumbermill;
			SetStats();
			break;
		case "Bridge":
		case "BridgeNotAvailable":
			this.type = "Bridge";
			text = TooltipTexts.Instance.build_bridge;
			break;
		case "NotAvailableIsland":
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
		stats = null;
	}

	void OnGUI() {
		if(drawGUI) {
			GUI.depth = -1000;
			GUI.Label(new Rect(mousePos.x + 8, mousePos.y - 75, 200, 100), text, tooltipStyle);

			switch(type) {
			case "TowerNormal":
			case "TowerIce":
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
			case "NotAvailableBridge":
				DrawPricing(BridgeManager.Instance.GoldCost().ToString(), BridgeManager.Instance.StoneCost().ToString(), BridgeManager.Instance.WoodCost().ToString());
				break;
			}
		}
	}

	private void DrawPricing(string goldCost, string stoneCost, string woodCost) {
		tooltipTextStyle.normal.textColor = (PlayerData.Instance.goldAmount >= System.Int32.Parse(goldCost)) ? Color.green : Color.red; 
		GUI.DrawTexture(new Rect(mousePos.x + 111, mousePos.y - 32, 24, 24), goldTexture); 
		GUI.Label(new Rect(mousePos.x + 130, mousePos.y - 32, 200, 100), goldCost, tooltipTextStyle);

		tooltipTextStyle.normal.textColor = (PlayerData.Instance.stoneAmount >= System.Int32.Parse(stoneCost)) ? Color.green : Color.red; 
		GUI.DrawTexture(new Rect(mousePos.x + 16, mousePos.y - 8, 24, 24), stoneTexture); 
		GUI.Label(new Rect(mousePos.x + 35, mousePos.y - 8, 200, 100), stoneCost, tooltipTextStyle);

		tooltipTextStyle.normal.textColor = (PlayerData.Instance.woodAmount >= System.Int32.Parse(woodCost)) ? Color.green : Color.red; 
		GUI.DrawTexture(new Rect(mousePos.x + 111, mousePos.y - 8, 24, 24), woodTexture); 
		GUI.Label(new Rect(mousePos.x + 130, mousePos.y - 8, 200, 100), woodCost, tooltipTextStyle);

		tooltipStyle.normal.textColor = Color.yellow;
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

	private void SetStats() {
		if(stats == null) stats = GameObject.Find(type).GetComponent<BuildingStats>();
	}
}
