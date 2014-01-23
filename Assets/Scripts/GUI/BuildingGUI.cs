using UnityEngine;
using System.Collections;

public class BuildingGUI:MonoBehaviour {
	public Texture2D buttonTower;
	public Texture2D buttonTowerNoMoney;
	public Texture2D buttonTowerIce;
	public Texture2D buttonTowerIceNoMoney;
	public Texture2D buttonTowerFire;
	public Texture2D buttonTowerFireNoMoney;
	public Texture2D buttonMine;
	public Texture2D buttonMineNoMoney;
	public Texture2D buttonLumberMill;
	public Texture2D buttonLumberMillNoMoney;
	public Texture2D buttonBridge;
	public Texture2D buttonBridgeNoMoney;

	public Texture2D buttonNotAvailable;

	public Texture backgroundTexture;

	public float showDelay;
	
	public GUIStyle styleText;
	public GUIStyle styleSell;
	public GUIStyle styleUpgrade;

	public GUIStyle buttonStyle;

	private Transform target;
	private Vector3 targetPosition;

	private BuildingManager buildingManager;
	private EBuildingType selectedBuilding;
	private Tooltip tooltipManager;
	private IslandData islandData;
	private Bridge bridgeManager;
	private Building building;

	private string tooltip;

	private bool draw;
	private bool subMenu;

	void Start() {
		tooltipManager = GetComponent<Tooltip>();
		Deselect();
	}

	void Update() {
		if(target != null && !target.renderer.isVisible) {
			draw = false;
		} else {
			draw = true;
		}

		if(tooltip != null && tooltip != ""){
			tooltipManager.drawTooltip(tooltip, true);
		} else {
			tooltipManager.unloadGUI();
		}

		if(target != null)
			targetPosition = Camera.main.WorldToScreenPoint(target.position);
		
		if(Input.GetMouseButton(1))
			Deselect();
		
		if(Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, 100) && selectedBuilding == EBuildingType.None) {
				if(!hit.transform.gameObject.CompareTag("Enemy") && !hit.transform.gameObject.CompareTag("Untagged")) {
					switch(hit.transform.gameObject.GetComponent<BuildingType>().type) {
					case EBuildingType.Empty:
						SelectEmpty(hit);
						break;
					case EBuildingType.TowerNormal:
						SelectTower(hit, hit.transform.gameObject.GetComponent<BuildingType>().type);
						break;
					case EBuildingType.TowerIce:
						SelectTower(hit, hit.transform.gameObject.GetComponent<BuildingType>().type);
						break;
					case EBuildingType.TowerFire:
						SelectTower(hit, hit.transform.gameObject.GetComponent<BuildingType>().type);
						break;
					case EBuildingType.Mine:
						SelectMine(hit);
						break;
					case EBuildingType.LumberMill:
						SelectLumberMill(hit);
						break;
					case EBuildingType.Bridge:
						SelectBridge(hit);
						break;
					}
				}
			}
		}
	}

	void OnGUI() {
		if(target != null && draw) {
			if(selectedBuilding == EBuildingType.Empty) {
				DrawTowerButton(islandData.canHaveTower);
				DrawMineButton(islandData.canHaveMine);
				DrawLumberMillButton(islandData.canHaveLumberMill);
			} else if(selectedBuilding == EBuildingType.Bridge) {
				DrawBridgeGUI();
			} else if(selectedBuilding != EBuildingType.Empty && selectedBuilding != EBuildingType.None) {
				DrawUpgradeGUI();
			}
		}
		
		tooltip = GUI.tooltip;
	}

	IEnumerator ShowDelay() {
		yield return new WaitForSeconds(showDelay);
	}

	private void DrawTowerButton(bool canHaveTower) {
		if(canHaveTower) {
			buttonStyle.normal.background = buttonTower;

			if(GUI.Button(new Rect(targetPosition.x - 29.9f, Screen.height + -targetPosition.y - 100, 84.4f, 84.4f), new GUIContent("", "TowerMenu"), buttonStyle)) {
				subMenu = !subMenu;
			}

			if(subMenu) {
				DrawTowerNormalSubButton();
				DrawTowerIceSubButton();
				DrawTowerFireSubButton();
			}
		} else {
			buttonStyle.normal.background = buttonNotAvailable;

			GUI.Button(new Rect(targetPosition.x - 25, Screen.height + -targetPosition.y - 100, 75, 75), new GUIContent("", "NotAvailableIsland"), buttonStyle);
		}
	}

	private void DrawTowerNormalSubButton() {
		BuildingStats stats = GameObject.Find("TowerNormal").GetComponent<BuildingStats>();
		DisableRenderer(stats.gameObject);
		
		if(PlayerData.Instance.goldAmount >= stats.goldCostPerLevel[0] && PlayerData.Instance.stoneAmount >= stats.stoneCostPerLevel[0] && PlayerData.Instance.woodAmount >= stats.woodCostPerLevel[0]) {
			buttonStyle.normal.background = buttonTower;
			
			if(GUI.Button(new Rect(targetPosition.x - 10.94f, Screen.height + -targetPosition.y - 150, 46.9f, 46.9f), new GUIContent("", "TowerNormal"), buttonStyle)) {
				Create(EBuildingType.TowerNormal);
				
				PlayerData.Instance.goldAmount -= stats.goldCostPerLevel[0];
				PlayerData.Instance.stoneAmount -= stats.stoneCostPerLevel[0];
				PlayerData.Instance.woodAmount -= stats.woodCostPerLevel[0];
			}
		} else {
			buttonStyle.normal.background = buttonTowerNoMoney;
			
			GUI.Button(new Rect(targetPosition.x - 10.94f, Screen.height + -targetPosition.y - 150, 46.9f, 46.9f), new GUIContent("", "TowerNormalNotAvailable"), buttonStyle);
		}
	}

	private void DrawTowerIceSubButton() {
		BuildingStats stats = GameObject.Find("TowerIce").GetComponent<BuildingStats>();
		DisableRenderer(stats.gameObject);
		
		if(PlayerData.Instance.goldAmount >= stats.goldCostPerLevel[0] && PlayerData.Instance.stoneAmount >= stats.stoneCostPerLevel[0] && PlayerData.Instance.woodAmount >= stats.woodCostPerLevel[0]) {
			buttonStyle.normal.background = buttonTowerIce;
			
			if(GUI.Button(new Rect(targetPosition.x + 37.5f, Screen.height + -targetPosition.y - 137.5f, 37.5f, 37.5f), new GUIContent("", "TowerIce"), buttonStyle)) {
				Create(EBuildingType.TowerIce);
				
				PlayerData.Instance.goldAmount -= stats.goldCostPerLevel[0];
				PlayerData.Instance.stoneAmount -= stats.stoneCostPerLevel[0];
				PlayerData.Instance.woodAmount -= stats.woodCostPerLevel[0];
			}
		} else {
			buttonStyle.normal.background = buttonTowerIceNoMoney;
			
			GUI.Button(new Rect(targetPosition.x + 37.5f, Screen.height + -targetPosition.y - 137.5f, 37.5f, 37.5f), new GUIContent("", "TowerIceNotAvailable"), buttonStyle);
		}
	}

	private void DrawTowerFireSubButton() {
		BuildingStats stats = GameObject.Find("TowerFire").GetComponent<BuildingStats>();
		DisableRenderer(stats.gameObject);
		
		if(PlayerData.Instance.goldAmount >= stats.goldCostPerLevel[0] && PlayerData.Instance.stoneAmount >= stats.stoneCostPerLevel[0] && PlayerData.Instance.woodAmount >= stats.woodCostPerLevel[0]) {
			buttonStyle.normal.background = buttonTowerFire;
			
			if(GUI.Button(new Rect(targetPosition.x - 50, Screen.height + -targetPosition.y - 137.5f, 37.5f, 37.5f), new GUIContent("", "TowerFire"), buttonStyle)) {
				Create(EBuildingType.TowerFire);
				
				PlayerData.Instance.goldAmount -= stats.goldCostPerLevel[0];
				PlayerData.Instance.stoneAmount -= stats.stoneCostPerLevel[0];
				PlayerData.Instance.woodAmount -= stats.woodCostPerLevel[0];
			}
		} else {
			buttonStyle.normal.background = buttonTowerFireNoMoney;
			
			GUI.Button(new Rect(targetPosition.x - 50, Screen.height + -targetPosition.y - 137.5f, 37.5f, 37.5f), new GUIContent("", "TowerFireNotAvailable"), buttonStyle);
		}
	}
	
	private void DrawMineButton(bool canHaveMine) {
		if(canHaveMine) {
			BuildingStats stats = GameObject.Find("Mine").GetComponent<BuildingStats>();
			DisableRenderer(stats.gameObject);
		
			if(PlayerData.Instance.goldAmount >= stats.goldCostPerLevel[0] && PlayerData.Instance.stoneAmount >= stats.stoneCostPerLevel[0] && PlayerData.Instance.woodAmount >= stats.woodCostPerLevel[0]) {
				buttonStyle.normal.background = buttonMine;

				if(GUI.Button(new Rect(targetPosition.x + 50, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "Mine"), buttonStyle)) {
					Create(EBuildingType.Mine);
					
					PlayerData.Instance.goldAmount -= stats.goldCostPerLevel[0];
					PlayerData.Instance.stoneAmount -= stats.stoneCostPerLevel[0];
					PlayerData.Instance.woodAmount -= stats.woodCostPerLevel[0];
				}
			} else {
				buttonStyle.normal.background = buttonMineNoMoney;

				GUI.Button(new Rect(targetPosition.x + 50, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "MineNotAvailable"), buttonStyle);
			}
		} else {
			buttonStyle.normal.background = buttonNotAvailable;

			GUI.Button(new Rect(targetPosition.x + 50, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "NotAvailableIsland"), buttonStyle);
		}
	}
	
	private void DrawLumberMillButton(bool canHaveLumberMill) {
		if(canHaveLumberMill) {
			BuildingStats stats = GameObject.Find("LumberMill").GetComponent<BuildingStats>();
			DisableRenderer(stats.gameObject);
		
			if(PlayerData.Instance.goldAmount >= stats.goldCostPerLevel[0] && PlayerData.Instance.stoneAmount >= stats.stoneCostPerLevel[0] && PlayerData.Instance.woodAmount >= stats.woodCostPerLevel[0]) {
				buttonStyle.normal.background = buttonLumberMill;

				if(GUI.Button(new Rect(targetPosition.x - 100, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "LumberMill"), buttonStyle)) {
					Create(EBuildingType.LumberMill);
					
					PlayerData.Instance.goldAmount -= stats.goldCostPerLevel[0];
					PlayerData.Instance.stoneAmount -= stats.stoneCostPerLevel[0];
					PlayerData.Instance.woodAmount -= stats.woodCostPerLevel[0];
				}
			} else {
				buttonStyle.normal.background = buttonLumberMillNoMoney;

				GUI.Button(new Rect(targetPosition.x - 100, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "LumberMillNotAvailable"), buttonStyle);
			}
		} else {
			buttonStyle.normal.background = buttonNotAvailable;

			GUI.Button(new Rect(targetPosition.x - 100, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "NotAvailableIsland"), buttonStyle);
		}
	}

	private void DrawUpgradeGUI() {
		GUI.BeginGroup(new Rect(targetPosition.x - 200, Screen.height + -targetPosition.y - 150, 400, 250));
		GUI.DrawTexture(new Rect(0, 0, 400, 250), backgroundTexture);

		if(building.currentLevel != building.stats.levels) {
			bool enoughGold = PlayerData.Instance.goldAmount >= building.stats.goldCostPerLevel[building.currentLevel -1];
			bool enoughStone = PlayerData.Instance.stoneAmount >= building.stats.stoneCostPerLevel[building.currentLevel -1];
			bool enoughWood = PlayerData.Instance.woodAmount >= building.stats.woodCostPerLevel[building.currentLevel -1];
			
			styleText.normal.textColor = enoughWood ? Color.green : Color.red;
			GUI.Label(new Rect(55, 37, 30, 20), building.stats.woodCostPerLevel[building.currentLevel - 1].ToString(), styleText);
			
			styleText.normal.textColor = enoughStone ? Color.green : Color.red;
			GUI.Label(new Rect(55, 77, 30, 20), building.stats.stoneCostPerLevel[building.currentLevel - 1].ToString(), styleText);
			
			styleText.normal.textColor = enoughGold ? Color.green : Color.red;
			GUI.Label(new Rect(55, 110, 30, 20), building.stats.goldCostPerLevel[building.currentLevel - 1].ToString(), styleText);

			if(GUI.Button(new Rect(28, 195, 80.83f, 26.66f), "", styleUpgrade)) {
				if(PlayerData.Instance.goldAmount >= building.stats.goldCostPerLevel[building.currentLevel - 1] && PlayerData.Instance.woodAmount >= building.stats.woodCostPerLevel[building.currentLevel - 1] && PlayerData.Instance.stoneAmount >= building.stats.stoneCostPerLevel[building.currentLevel - 1]) {
					PlayerData.Instance.goldAmount -= building.stats.goldCostPerLevel[building.currentLevel - 1];
					PlayerData.Instance.stoneAmount -= building.stats.stoneCostPerLevel[building.currentLevel - 1];
					PlayerData.Instance.woodAmount -= building.stats.woodCostPerLevel[building.currentLevel - 1];
					building.SwitchLevel(building.currentLevel + 1);
				}
			}
		}
		
		styleText.normal.textColor = Color.white;
		styleText.alignment = TextAnchor.UpperCenter;

		if(building.GetComponent<BuildingType>().type.ToString() == "TowerNormal" || building.GetComponent<BuildingType>().type.ToString() == "TowerIce" || building.GetComponent<BuildingType>().type.ToString() == "TowerFire") {
			Tower tower = building.GetComponent<Tower>();
			
			GUI.Label(new Rect(200, 0, 1, 20), tower.towerType.ToString() + " Tower", styleText);
			GUI.Label(new Rect(200, 50, 1, 20), "Damage: " + tower.stats.damagePerLevel[tower.currentLevel - 1].ToString(), styleText);
			GUI.Label(new Rect(200, 75, 1, 20), "Range: " + tower.stats.rangePerLevel[tower.currentLevel - 1].ToString(), styleText);
			GUI.Label(new Rect(200, 100, 1, 20), "Speed: " + (60 / tower.stats.speedPerLevel[tower.currentLevel - 1]).ToString(), styleText);

			if(tower.towerType == Tower.TowerType.Fire) {
				GUI.Label(new Rect(200, 130, 1, 20), "Fire damage\nover time", styleText);
			} else if(tower.towerType == Tower.TowerType.Ice) {
				GUI.Label(new Rect(200, 130, 1, 20), "Freezes\nenemies", styleText);
			}
		} else {
			BuildingStats stats = building.GetComponent<BuildingStats>();
			
			GUI.Label(new Rect(200, 0, 1, 20), building.GetComponent<BuildingType>().type.ToString(), styleText);
			GUI.Label(new Rect(200, 50, 1, 20), "Resources: " + stats.resourcesPerTick[building.currentLevel - 1], styleText);
		}
		
		GUI.Label(new Rect(200, 25, 1, 20), "Level " + building.currentLevel, styleText);
		
		styleText.normal.textColor = Color.green;
		styleText.alignment = TextAnchor.UpperLeft;
		if(building.currentLevel == 1){
		GUI.Label(new Rect(320, 37, 30, 20), (Mathf.FloorToInt(building.stats.woodCostPerLevel[building.currentLevel - 1] * building.stats.sellRate)).ToString(), styleText);
		GUI.Label(new Rect(320, 77, 30, 20), (Mathf.FloorToInt(building.stats.stoneCostPerLevel[building.currentLevel - 1] * building.stats.sellRate)).ToString(), styleText);
		GUI.Label(new Rect(320, 110, 30, 20), (Mathf.FloorToInt(building.stats.goldCostPerLevel[building.currentLevel - 1] * building.stats.sellRate)).ToString(), styleText);
		}else{
			GUI.Label(new Rect(320, 37, 30, 20), (Mathf.FloorToInt(building.stats.woodCostPerLevel[building.currentLevel - 2] * building.stats.sellRate)).ToString(), styleText);
			GUI.Label(new Rect(320, 77, 30, 20), (Mathf.FloorToInt(building.stats.stoneCostPerLevel[building.currentLevel - 2] * building.stats.sellRate)).ToString(), styleText);
			GUI.Label(new Rect(320, 110, 30, 20), (Mathf.FloorToInt(building.stats.goldCostPerLevel[building.currentLevel - 2] * building.stats.sellRate)).ToString(), styleText);
		}

		if(GUI.Button(new Rect(291, 195, 80.83f, 26.66f), "", styleSell))
			DestroyBuilding();

		GUI.EndGroup();
	}

	private void DrawBridgeGUI() {
		styleText.normal.textColor = Color.black;
		
		GUIStyle bridgeStyle = new GUIStyle();

		GUI.BeginGroup(new Rect(targetPosition.x - 37.5f, Screen.height + -targetPosition.y - 70, 75, 140));
			if(PlayerData.Instance.goldAmount >= BridgeManager.Instance.GoldCost() && PlayerData.Instance.stoneAmount >= BridgeManager.Instance.StoneCost() && PlayerData.Instance.woodAmount >= BridgeManager.Instance.WoodCost()) {
				bridgeStyle.normal.background = buttonBridge;
				if(GUI.Button(new Rect(0, 65, 75, 75), new GUIContent("", "Bridge"), bridgeStyle)) {
					bridgeManager.Build();
					Deselect();
				}
			} else {
				bridgeStyle.normal.background = buttonBridgeNoMoney;
				GUI.Button(new Rect(0, 25, 75, 75), new GUIContent("", "BridgeNotAvailable"), bridgeStyle);
			}
		GUI.EndGroup();
	}

	private void SelectEmpty(RaycastHit hit) {
		islandData = hit.transform.parent.GetComponent<IslandReference>().iData;
		buildingManager = hit.transform.parent.gameObject.GetComponent<BuildingManager>();
		
		if(islandData.isUnlocked)
			Select(hit.transform, EBuildingType.Empty);
	}

	private void SelectTower(RaycastHit hit, EBuildingType type) {
		building = hit.transform.gameObject.GetComponent<Tower>();

		Select(hit.transform, type);
	}

	private void SelectMine(RaycastHit hit) {
		building = hit.transform.gameObject.GetComponent<Mine>();
		Select(hit.transform, EBuildingType.Mine);
	}

	private void SelectLumberMill(RaycastHit hit) {
		building = hit.transform.gameObject.GetComponent<LumberMill>();
		Select(hit.transform, EBuildingType.LumberMill);
	}

	private void SelectBridge(RaycastHit hit) {
		islandData = hit.transform.GetComponent<IslandReference>().iData;
		bridgeManager = hit.transform.parent.gameObject.GetComponent<Bridge>();
		
		if(islandData.isUnlocked && !bridgeManager.isMade)
			Select(hit.transform, EBuildingType.Bridge);
	}
	
	private void Select(Transform target, EBuildingType type) {
		this.target = target;

		targetPosition = Camera.main.WorldToScreenPoint(target.position);
		selectedBuilding = type;
		
		StartCoroutine("ShowDelay");
	}
	
	private void Deselect() {
		selectedBuilding = EBuildingType.None;

		subMenu = false;
		building = null;
		target = null;
	}

	private void Create(EBuildingType type) {
		buildingManager.CreateBuilding(type);

		Deselect();
	}

	void DestroyBuilding() {
		buildingManager = building.transform.parent.GetComponent<BuildingManager>();
		if(building.currentLevel == 1){
			PlayerData.Instance.goldAmount += Mathf.FloorToInt(building.stats.goldCostPerLevel[building.currentLevel - 1] * building.stats.sellRate);
			PlayerData.Instance.stoneAmount += Mathf.FloorToInt(building.stats.stoneCostPerLevel[building.currentLevel - 1] * building.stats.sellRate);
			PlayerData.Instance.woodAmount += Mathf.FloorToInt(building.stats.woodCostPerLevel[building.currentLevel - 1] * building.stats.sellRate);
		}else{
			PlayerData.Instance.goldAmount += Mathf.FloorToInt(building.stats.goldCostPerLevel[building.currentLevel - 2] * building.stats.sellRate);
			PlayerData.Instance.stoneAmount += Mathf.FloorToInt(building.stats.stoneCostPerLevel[building.currentLevel - 2] * building.stats.sellRate);
			PlayerData.Instance.woodAmount += Mathf.FloorToInt(building.stats.woodCostPerLevel[building.currentLevel - 2] * building.stats.sellRate);
		}
		buildingManager.DestroyBuilding(building);
		
		Deselect();
	}

	private void DisableRenderer(GameObject go) {
		Renderer[] children = go.GetComponentsInChildren<Renderer> ();
		
		for (int i = 0; i < children.Length; i++)
			children [i].enabled = false;
	}
}