using UnityEngine;
using System.Collections;

public class BuildingGUI:MonoBehaviour {
	public Texture2D buttonTower;
	public Texture2D buttonTowerNoMoney;
	public Texture2D buttonMine;
	public Texture2D buttonMineNoMoney;
	public Texture2D buttonLumberMill;
	public Texture2D buttonLumberMillNoMoney;
	public Texture2D buttonBridge;
	public Texture2D buttonBridgeNoMoney;

	public Texture backgroundTexture;

	public float showDelay;
	
	public GUIStyle styleText;
	public GUIStyle styleSell;
	public GUIStyle styleUpgrade;

	public GUIStyle styleNormal;
	public GUIStyle styleNoMoney;
	public GUIStyle styleNoBuilding;

	private Transform target;
	private Vector3 targetPosition;

	private BuildingManager buildingManager;
	private EBuildingType selectedBuilding;
	private Tooltip tooltipManager;
	private IslandData islandData;
	private Bridge bridgeManager;
	private Building building;

	private string tooltip;

	void Start() {
		tooltipManager = GetComponent<Tooltip>();
		Deselect();
	}

	void Update() {
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
				if(!hit.transform.gameObject.CompareTag("Enemy")) {
					switch(hit.transform.gameObject.GetComponent<BuildingType>().type) {
					case EBuildingType.Empty:
						SelectEmpty(hit);
						break;
					case EBuildingType.Tower:
						SelectTower(hit);
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
		if(target != null) {
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
		styleNormal.normal.background = buttonTower;
		styleNoMoney.normal.background = buttonTowerNoMoney;

		if(canHaveTower) {
			BuildingStats stats = (Instantiate(Resources.Load("Prefabs/Buildings/Tower")) as GameObject).GetComponent<BuildingStats>();
			
			if(PlayerData.Instance.goldAmount >= stats.goldCostPerLevel[0] && PlayerData.Instance.stoneAmount >= stats.stoneCostPerLevel[0] && PlayerData.Instance.woodAmount >= stats.woodCostPerLevel[0]) {
				if(GUI.Button(new Rect(targetPosition.x - 25, Screen.height + -targetPosition.y - 100, 75, 75), new GUIContent("", "Tower"), styleNormal)) {
					Create(EBuildingType.Tower);
					
					PlayerData.Instance.goldAmount -= stats.goldCostPerLevel[0];
					PlayerData.Instance.stoneAmount -= stats.stoneCostPerLevel[0];
					PlayerData.Instance.woodAmount -= stats.woodCostPerLevel[0];
				}
			} else {
				GUI.Button(new Rect(targetPosition.x - 25, Screen.height + -targetPosition.y - 100, 75, 75), new GUIContent("", "Tower"), styleNoMoney);
			}
			
			Destroy(stats.gameObject);
		} else {
			GUI.Button(new Rect(targetPosition.x - 25, Screen.height + -targetPosition.y - 100, 75, 75), new GUIContent("", "NotAvailable"), styleNoBuilding);
		}
	}
	
	private void DrawMineButton(bool canHaveMine) {
		styleNormal.normal.background = buttonMine;
		styleNoMoney.normal.background = buttonMineNoMoney;

		if(canHaveMine) {
			BuildingStats stats = (Instantiate(Resources.Load("Prefabs/Buildings/Mine")) as GameObject).GetComponent<BuildingStats>();
		
			if(PlayerData.Instance.goldAmount >= stats.goldCostPerLevel[0] && PlayerData.Instance.stoneAmount >= stats.stoneCostPerLevel[0] && PlayerData.Instance.woodAmount >= stats.woodCostPerLevel[0]) {
				if(GUI.Button(new Rect(targetPosition.x + 50, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "Mine"), styleNormal)) {
					Create(EBuildingType.Mine);
					
					PlayerData.Instance.goldAmount -= stats.goldCostPerLevel[0];
					PlayerData.Instance.stoneAmount -= stats.stoneCostPerLevel[0];
					PlayerData.Instance.woodAmount -= stats.woodCostPerLevel[0];
				}
			} else {
				GUI.Button(new Rect(targetPosition.x + 50, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "Mine"), styleNoMoney);
			}
			
			Destroy(stats.gameObject);
		} else {
			GUI.Button(new Rect(targetPosition.x + 50, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "NotAvailable"), styleNoBuilding);
		}
	}
	
	private void DrawLumberMillButton(bool canHaveLumberMill) {
		styleNormal.normal.background = buttonLumberMill;
		styleNoMoney.normal.background = buttonLumberMillNoMoney;

		if(canHaveLumberMill) {
			BuildingStats stats = (Instantiate(Resources.Load("Prefabs/Buildings/LumberMill")) as GameObject).GetComponent<BuildingStats>();
		
			if(PlayerData.Instance.goldAmount >= stats.goldCostPerLevel[0] && PlayerData.Instance.stoneAmount >= stats.stoneCostPerLevel[0] && PlayerData.Instance.woodAmount >= stats.woodCostPerLevel[0]) {
				if(GUI.Button(new Rect(targetPosition.x - 100, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "LumberMill"), styleNormal)) {
					Create(EBuildingType.LumberMill);
					
					PlayerData.Instance.goldAmount -= stats.goldCostPerLevel[0];
					PlayerData.Instance.stoneAmount -= stats.stoneCostPerLevel[0];
					PlayerData.Instance.woodAmount -= stats.woodCostPerLevel[0];
				}
			} else {
				GUI.Button(new Rect(targetPosition.x - 100, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "LumberMill"), styleNoMoney);
			}
			
			Destroy(stats.gameObject);
		} else {
			GUI.Button(new Rect(targetPosition.x - 100, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "NotAvailable"), styleNoBuilding);
		}
	}

	private void DrawUpgradeGUI() {
		GUI.BeginGroup(new Rect(targetPosition.x - 200, Screen.height + -targetPosition.y - 150, 400, 250));
		GUI.DrawTexture(new Rect(0, 0, 400, 250), backgroundTexture);

		if(building.currentLevel != building.stats.levels) {
			bool enoughGold = PlayerData.Instance.goldAmount >= building.stats.goldCostPerLevel[building.currentLevel - 1];
			bool enoughStone = PlayerData.Instance.stoneAmount >= building.stats.stoneCostPerLevel[building.currentLevel - 1];
			bool enoughWood = PlayerData.Instance.woodAmount >= building.stats.woodCostPerLevel[building.currentLevel - 1];
			
			styleText.normal.textColor = enoughWood ? Color.green : Color.red;
			GUI.Label(new Rect(60, 37, 30, 20), building.stats.woodCostPerLevel[building.currentLevel - 1].ToString(), styleText);
			
			styleText.normal.textColor = enoughStone ? Color.green : Color.red;
			GUI.Label(new Rect(60, 77, 30, 20), building.stats.stoneCostPerLevel[building.currentLevel - 1].ToString(), styleText);
			
			styleText.normal.textColor = enoughGold ? Color.green : Color.red;
			GUI.Label(new Rect(60, 110, 30, 20), building.stats.goldCostPerLevel[building.currentLevel - 1].ToString(), styleText);

			if(GUI.Button(new Rect(28, 195, 80.83f, 26.66f), "", styleUpgrade)) {
				if(PlayerData.Instance.goldAmount >= building.stats.goldCostPerLevel[building.currentLevel - 1] && PlayerData.Instance.woodAmount >= building.stats.woodCostPerLevel[building.currentLevel - 1] && PlayerData.Instance.stoneAmount >= building.stats.stoneCostPerLevel[building.currentLevel]) {
					PlayerData.Instance.goldAmount -= building.stats.goldCostPerLevel[building.currentLevel - 1];
					PlayerData.Instance.stoneAmount -= building.stats.stoneCostPerLevel[building.currentLevel - 1];
					PlayerData.Instance.woodAmount -= building.stats.woodCostPerLevel[building.currentLevel - 1];
					building.SwitchLevel(building.currentLevel + 1);
				}
			}
		}
		
		styleText.normal.textColor = Color.white;
		styleText.alignment = TextAnchor.UpperCenter;

		if(building.GetComponent<BuildingType>().type.ToString() == "Tower") {
			Tower tower = building.GetComponent<Tower>();
			
			GUI.Label(new Rect(200, 0, 1, 20), tower.towerType.ToString() + " " + building.GetComponent<BuildingType>().type.ToString(), styleText);
			GUI.Label(new Rect(200, 50, 1, 20), "Damage: " + tower.damage, styleText);
			GUI.Label(new Rect(200, 75, 1, 20), "Range: " + tower.maxRange, styleText);
			GUI.Label(new Rect(200, 100, 1, 20), "Speed: " + tower.tickDelay, styleText);
		} else {
			BuildingStats stats = building.GetComponent<BuildingStats>();
			
			GUI.Label(new Rect(200, 0, 1, 20), building.GetComponent<BuildingType>().type.ToString(), styleText);
			GUI.Label(new Rect(200, 50, 1, 20), "Resources: " + stats.resourcesPerTick[building.currentLevel - 1], styleText);
		}
		
		GUI.Label(new Rect(200, 25, 1, 20), "Level: " + building.currentLevel, styleText);
		
		styleText.normal.textColor = Color.green;
		styleText.alignment = TextAnchor.UpperLeft;
		
		GUI.Label(new Rect(325, 37, 30, 20), building.stats.woodSellPerLevel[building.currentLevel - 1].ToString(), styleText);
		GUI.Label(new Rect(325, 77, 30, 20), building.stats.stoneSellPerLevel[building.currentLevel - 1].ToString(), styleText);
		GUI.Label(new Rect(325, 110, 30, 20), building.stats.goldSellPerLevel[building.currentLevel - 1].ToString(), styleText);
		
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
				GUI.Button(new Rect(0, 25, 75, 75), new GUIContent("", "NotAvailable"), bridgeStyle);
			}
		GUI.EndGroup();
	}

	private void SelectEmpty(RaycastHit hit) {
		islandData = hit.transform.parent.GetComponent<IslandReference>().iData;
		buildingManager = hit.transform.parent.gameObject.GetComponent<BuildingManager>();
		
		if(islandData.isUnlocked)
			Select(hit.transform, EBuildingType.Empty);
	}

	private void SelectTower(RaycastHit hit) {
		building = hit.transform.gameObject.GetComponent<Tower>();

		Select(hit.transform, EBuildingType.Tower);
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

		building = null;
		target = null;
	}

	private void Create(EBuildingType type) {
		buildingManager.CreateBuilding(type);
		
		Deselect();
	}

	void DestroyBuilding() {
		buildingManager = building.transform.parent.GetComponent<BuildingManager>();
		
		PlayerData.Instance.stoneAmount += building.stats.stoneSellPerLevel[building.currentLevel - 1];
		PlayerData.Instance.woodAmount += building.stats.woodSellPerLevel[building.currentLevel - 1];
		
		buildingManager.DestroyBuilding(building);
		
		Deselect();
	}
}