using UnityEngine;
using System.Collections;

public class Tooltip:MonoBehaviour {
	public GUIStyle tooltipStyle;

	private Vector2 mousePos;
	
	private string text;

	private bool drawGUI;
	
	void Update() {
		mousePos = new Vector2(Input.mousePosition.x,(Screen.height - Input.mousePosition.y));
	}

	public void drawTooltip(string type, bool state){
		switch(type) {
		case "Tower":
			text = TooltipTexts.Instance.build_tower;
			break;
		case "Mine":
			text = TooltipTexts.Instance.build_mine;
			break;
		case "LumberMill":
			text = TooltipTexts.Instance.build_lumbermill;
			break;
		case "Bridge":
			text = TooltipTexts.Instance.build_bridge;
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
	}

	void OnGUI() {
		if(drawGUI) {
			GUI.depth = -1000;
			GUI.Label(new Rect(mousePos.x + 8, mousePos.y - 75, 200, 100), text, tooltipStyle);
		}
	}
}
