using UnityEngine;
using System.Collections;

public class Tooltip : MonoBehaviour {
	string tooltipType;
	string tooltipText;
	private bool drawgui;
	public Vector2 mousePos;

	public GUIStyle tooltipStyle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		mousePos = new Vector2(Input.mousePosition.x,(Screen.height - Input.mousePosition.y));
	}
	public void drawTooltip(string text,bool state){
		tooltipType = text;
		drawgui = state;

		switch(tooltipType){
			case "Tower":
				tooltipText = "Builds a new Tower";
			break;
			case "Mine":
				tooltipText = "Builds a new Mine";
			break;
			case "LumberMill":
				tooltipText = "Builds a new LumberMill";
			break;
			case "NoBuilding":
				tooltipText = "You can't build this building here";
			break;
		}
	}
	public void unloadGUI(){
		drawgui = false;
		tooltipType = "";
	}

	void OnGUI(){
		if(drawgui){
		GUI.Label(new Rect(mousePos.x + 8,mousePos.y - 75,200,100),tooltipText,tooltipStyle);
		}
	}
}
