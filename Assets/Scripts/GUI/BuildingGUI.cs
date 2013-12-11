using UnityEngine;
using System.Collections;

public class BuildingGUI:MonoBehaviour {
	public Texture towerButton;
	public Texture lumberMillButton;
	public Texture mineButton;
	public Texture stone;
	public Texture wood;
	public Texture gold;

	public float delay;

	public GUIStyle styleSell;
	public GUIStyle styleBuy;

	private GameObject selectionParticle;
	private Transform target;
	private Vector3 position;

	private BuildingManager buildingManager;
	private EBuildingType selectedBuilding;
	private Building building;

	void Start() {
		DeselectBuilding();
	}

	void Update() {
		if(target != null)
			position = Camera.main.WorldToScreenPoint(target.position);

		if(Input.GetMouseButton(1))
			DeselectBuilding();

		if(Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit, 100)) {
				if(selectedBuilding == EBuildingType.None) {
					switch(hit.transform.gameObject.GetComponent<BuildingType>().type) {
					case EBuildingType.Tower:
						building = hit.transform.gameObject.GetComponent<Tower>();
						SelectBuilding(hit.transform, EBuildingType.Tower);

						break;
					case EBuildingType.LumberMill:
						building = hit.transform.gameObject.GetComponent<LumberMill>();
						SelectBuilding(hit.transform, EBuildingType.LumberMill);

						break;
					case EBuildingType.Mine:
						building = hit.transform.gameObject.GetComponent<Mine>();
						SelectBuilding(hit.transform, EBuildingType.Mine);

						break;
					case EBuildingType.Empty:
						buildingManager = hit.transform.gameObject.GetComponent<BuildingManager>();
						SelectBuilding(hit.transform, EBuildingType.Empty);

						break;
					}
				}
			}
		}
	}

	void OnGUI() {
		if(selectedBuilding == EBuildingType.Empty) {
			GUI.backgroundColor = Color.clear;
			
			if(GUI.Button(new Rect(position.x - 25, Screen.height + -position.y - 100, 75, 75), towerButton)) {
				CreateBuilding(EBuildingType.Tower);
			} else if(GUI.Button(new Rect(position.x - 100, Screen.height + -position.y + 25, 75, 75), lumberMillButton)) {
				CreateBuilding(EBuildingType.LumberMill);
			} else if(GUI.Button(new Rect(position.x + 50, Screen.height + -position.y + 25, 75, 75), mineButton)) {
				CreateBuilding(EBuildingType.Mine);
			}
		} else if(selectedBuilding != EBuildingType.None && selectedBuilding != EBuildingType.None) {
			GUI.BeginGroup(new Rect(position.x - 100, Screen.height + -position.y - 150, 200, 150));
			GUI.Box(new Rect(0, 0, 200, 150), building.name);
			GUI.Label(new Rect(5, 0, 200, 20), "" + building.name + ": ");
			GUI.Box(new Rect(140, 30, 60, 120), "");
			
			OpenPanel();
			GUI.EndGroup();
		}
	}
	
	void OpenPanel() {
		GUI.Label(new Rect(85, 25, 200, 20), "LVL: " + (int)building.currentLevel);
		
		if(building.currentLevel != building.maxLevel) {
			GUI.DrawTexture(new Rect(0, 50, 20, 20), wood);
			GUI.DrawTexture(new Rect(0, 70, 20, 20), stone);
			
			GUI.Label(new Rect(25, 50, 30, 20), "-" + building.woodCost, styleBuy);
			GUI.Label(new Rect(25, 72, 30, 20), "-" + building.stoneCost, styleBuy);
			GUI.Label(new Rect(18, 27, 30, 20), "Cost");
			
			if(GUI.Button(new Rect(5, 95, 50, 50), "Upgrade")) {
				if(PlayerData.woodAmount >= building.woodCost) {
					if(PlayerData.stoneAmount >= building.stoneCost) {
						building.SwitchLevel(building.currentLevel + 1);
					}
				}
			}
		}
		
		GUI.DrawTexture(new Rect(140, 50, 20, 20), wood);
		GUI.DrawTexture(new Rect(140, 70, 20, 20), stone);
		GUI.Label(new Rect(165, 50, 30, 20), "+" + building.woodSell, styleSell);
		GUI.Label(new Rect(165, 72, 30, 20), "+" + building.stoneSell, styleSell);
		
		if(GUI.Button(new Rect(145, 95, 50, 50), "Sell")) {
			DestroyBuilding();
		}
	}

	IEnumerator SlightDelay() {
		yield return new WaitForSeconds(delay);
	}

	void SelectBuilding(Transform target, EBuildingType type) {
		this.target = target;

		selectionParticle = Instantiate(Resources.Load("Particles/SelectionParticle"), target.position, Quaternion.identity) as GameObject;
		position = Camera.main.WorldToScreenPoint(target.position);
		selectedBuilding = type;

		StartCoroutine("SlightDelay");
	}

	void DeselectBuilding() {
		if(selectionParticle != null)
			Destroy(selectionParticle);

		selectedBuilding = EBuildingType.None;
		buildingManager = null;
		building = null;
		target = null;
	}

	void CreateBuilding(EBuildingType type) {
		buildingManager.CreateBuilding(type);

		DeselectBuilding();
	}

	void DestroyBuilding() {
		buildingManager = building.transform.parent.GetComponent<BuildingManager>();
		
		PlayerData.stoneAmount += building.stoneSell;
		PlayerData.woodAmount += building.woodSell;
		
		buildingManager.DestroyBuilding(building);
		
		DeselectBuilding();
	}
}
