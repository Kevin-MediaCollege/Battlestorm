using UnityEngine;
using System.Collections;

public class GameGUI:MonoBehaviour {
	private Building building;
	private PlayerData playerData;
	private BuildingManager manager;
	
	private bool pressed;
	
	private Vector3 guiPosition;
	private Transform target;

	public Texture lumberMillButton;
	public Texture towerButton;
	public Texture mineButton;
	
	public Texture stone;
	public Texture wood;
	public Texture gold;
	
	private string selectedBuilding;
	private string buildingName;
	
	public GUIStyle sellStyle;
	public GUIStyle buyStyle;

	public GameObject selectionParticle = null;

	void Start(){
		playerData = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerData>();

		Deselect();
	}
	
	void Update() {
		if(target != null)
			guiPosition = Camera.main.WorldToScreenPoint(target.position);

		if(Input.GetMouseButton(0)) {
			pressed = true;
		} else {
			pressed = false;
		}

		pressed = false;
		
		if(Input.GetMouseButton(1))
			Deselect();
		
		if(Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit, 100)) {
				if(selectedBuilding == "") {
					switch(hit.transform.gameObject.tag) {
					case "EmptyPlot":
						target = hit.transform;
						manager = target.gameObject.GetComponent<BuildingManager>();

						createSelectionParticle();

						break;
					case "LumberMill":
						target = hit.transform;
						building = target.gameObject.GetComponent<LumberMill>();

						createSelectionParticle();

						buildingName = "Lumber Mill";
						break;
					case "Mine":
						target = hit.transform;
						building = target.gameObject.GetComponent<Mine>();

						createSelectionParticle();

						buildingName = "Mine";
						break;
					}

					if(target != null)
						selectBuilding(target);
				}
			}
		}
	}

	void createSelectionParticle(){
		selectionParticle = Instantiate(Resources.Load("Particles/SelectionParticle"),target.position,Quaternion.identity)as GameObject;
	}

	void selectBuilding(Transform target) {
		pressed = true;

		guiPosition = Camera.main.WorldToScreenPoint(target.position);
		selectedBuilding = target.gameObject.tag;
		
		StartCoroutine("SlightDelay");
	}
	
	IEnumerator SlightDelay() {
		yield return new WaitForSeconds(0.02f);
	}
	
	void OnGUI() {
		if(selectedBuilding == "EmptyPlot") {
			GUI.backgroundColor = Color.clear;

			if(GUI.Button(new Rect(guiPosition.x - 25, Screen.height + -guiPosition.y - 100, 50, 50), towerButton)) {
				CreateBuilding(Building.Type.Tower);
			} else if(GUI.Button(new Rect(guiPosition.x - 100, Screen.height + -guiPosition.y + 25, 50, 50), lumberMillButton)) {
				CreateBuilding(Building.Type.LumberMill);
			} else if (GUI.Button(new Rect(guiPosition.x + 50, Screen.height + -guiPosition.y + 25, 50, 50), mineButton)) {
				CreateBuilding(Building.Type.Mine);
			}
		} else if(selectedBuilding != "EmptyPlot" && selectedBuilding != "" && selectedBuilding != null) {
			GUI.BeginGroup(new Rect(guiPosition.x - 100, Screen.height + -guiPosition.y - 150, 200, 150));
			GUI.Box(new Rect(0, 0, 200, 150), buildingName);
			GUI.Label(new Rect(5, 0, 200, 20), "" + building.name + ": ");
			GUI.Box(new Rect(140, 30, 60, 120), "");
			
			OpenPanel();
			GUI.EndGroup();
		}
	}
	
	void OpenPanel() {
		if(building == null)
			return;

		GUI.Label(new Rect(85, 25, 200, 20), "LVL: " + (int)building.currentLevel);

		if(building.currentLevel != building.maxLevel) {
			GUI.DrawTexture(new Rect(0, 50, 20, 20), wood);
			GUI.DrawTexture(new Rect(0, 70, 20, 20), stone);
			
			GUI.Label(new Rect(25, 50, 30, 20), "-" + building.woodCost, buyStyle);
			GUI.Label(new Rect(25, 72, 30, 20), "-" + building.stoneCost, buyStyle);
			GUI.Label(new Rect(18, 27, 30, 20), "Cost");
			
			if(GUI.Button(new Rect(5, 95, 50, 50), "Upgrade")) {
				if(playerData.woodAmount >= building.woodCost) {
					if(playerData.stoneAmount >= building.stoneCost) {
						building.SwitchLevel(building.currentLevel + 1);
					}
				}
			}
		}
		
		GUI.DrawTexture(new Rect(140, 50, 20, 20), wood);
		GUI.DrawTexture(new Rect(140, 70, 20, 20), stone);
		GUI.Label(new Rect(165, 50, 30, 20), "+" + building.woodSell, sellStyle);
		GUI.Label(new Rect(165, 72, 30, 20), "+" + building.stoneSell, sellStyle);
		
		if(GUI.Button(new Rect(145, 95, 50, 50), "Sell")) {
			DestroySelectedBuilding();
		}
	}
	
	void CreateBuilding(Building.Type type) {
		manager.CreateBuilding(type);
		manager.tag = "Untagged";
		
		Deselect();
	}

	void DestroySelectedBuilding() {
		manager = building.transform.parent.GetComponent<BuildingManager>();

		playerData.stoneAmount += building.stoneSell;
		playerData.woodAmount += building.woodSell;

		manager.DestroyBuilding(building);
		manager.tag = "EmptyPlot";

		Deselect();
	}

	void Deselect() {
		selectedBuilding = "";
		if(selectionParticle != null){
			SelectionParticle sParticle = selectionParticle.GetComponentInChildren<SelectionParticle>();
			sParticle.lightobj.intensity = 0;
			sParticle.particleobj.Stop();
		
		}
		target = null;
		manager = null;
		building = null;
	}
}
