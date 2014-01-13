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
	private IslandData islandData;
	private Bridge bridgeManager;
	private Building building;

	private int cost = 50;

	void Start() {
		Deselect();
	}

	void Update() {
		if(target != null)
			targetPosition = Camera.main.WorldToScreenPoint(target.position);
		
		if(Input.GetMouseButton(1))
			Deselect();
		
		if(Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, 100) && selectedBuilding == EBuildingType.None) {
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
	}

	IEnumerator ShowDelay() {
		yield return new WaitForSeconds(showDelay);
	}

	private void DrawTowerButton(bool canHaveTower) {
		styleNormal.normal.background = buttonTower;
		styleNoMoney.normal.background = buttonTowerNoMoney;

		if(canHaveTower) {
			if(PlayerData.Instance.goldAmount >= cost) {
				if(GUI.Button(new Rect(targetPosition.x - 25, Screen.height + -targetPosition.y - 100, 75, 75), new GUIContent("", "Tower"), styleNormal)) {
					Create(EBuildingType.Tower);
					PlayerData.Instance.goldAmount -= cost;
				}
			} else {
				GUI.Button(new Rect(targetPosition.x - 25, Screen.height + -targetPosition.y - 100, 75, 75), new GUIContent("", "Tower"), styleNoMoney);
			}
		} else {
			GUI.Button(new Rect(targetPosition.x - 25, Screen.height + -targetPosition.y - 100, 75, 75), new GUIContent("", "NoBuilding"), styleNoBuilding);
		}
	}
	
	private void DrawMineButton(bool canHaveMine) {
		styleNormal.normal.background = buttonMine;
		styleNoMoney.normal.background = buttonMineNoMoney;

		if(canHaveMine) {
			if(PlayerData.Instance.goldAmount >=  cost) {
				if(GUI.Button(new Rect(targetPosition.x + 50, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "Mine"), styleNormal)) {
					Create(EBuildingType.Mine);
					PlayerData.Instance.goldAmount -= cost;
				}
			} else {
				GUI.Button(new Rect(targetPosition.x + 50, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "Mine"), styleNoMoney);
			}
		} else {
			GUI.Button(new Rect(targetPosition.x + 50, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "NoBuilding"), styleNoBuilding);
		}
	}
	
	private void DrawLumberMillButton(bool canHaveLumberMill) {
		styleNormal.normal.background = buttonLumberMill;
		styleNoMoney.normal.background = buttonLumberMillNoMoney;

		if(canHaveLumberMill) {
			if(PlayerData.Instance.goldAmount >= cost) {
				if(GUI.Button(new Rect(targetPosition.x - 100, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "LumberMill"), styleNormal)) {
					Create(EBuildingType.LumberMill);
					PlayerData.Instance.goldAmount -= cost;
				}
			} else {
				GUI.Button(new Rect(targetPosition.x - 100, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "LumberMill"), styleNoMoney);
			}
		} else {
			GUI.Button(new Rect(targetPosition.x - 100, Screen.height + -targetPosition.y + 25, 75, 75), new GUIContent("", "NoBuilding"), styleNoBuilding);
		}
	}

	private void DrawUpgradeGUI() {
		GUI.BeginGroup(new Rect(targetPosition.x - 200, Screen.height + -targetPosition.y - 150, 400, 250));
		GUI.DrawTexture(new Rect(0, 0, 400, 250), backgroundTexture);

		if(building.currentLevel != building.stats.levels) {
			styleText.normal.textColor = Color.red;

			GUI.Label(new Rect(60, 63, 30, 20), "" + building.stats.woodCostPerLevel[building.currentLevel - 1], styleText);
			GUI.Label(new Rect(60, 103, 30, 20), "" + building.stats.stoneCostPerLevel[building.currentLevel - 1], styleText);

			if(GUI.Button(new Rect(28, 195, 80.83f, 26.66f), "", styleUpgrade)) {
				if(PlayerData.Instance.woodAmount >= building.stats.woodCostPerLevel[building.currentLevel - 1] && PlayerData.Instance.stoneAmount >= building.stats.stoneCostPerLevel[building.currentLevel]) {
					PlayerData.Instance.stoneAmount -= building.stats.stoneCostPerLevel[building.currentLevel - 1];
					PlayerData.Instance.woodAmount -= building.stats.woodCostPerLevel[building.currentLevel - 1];
					building.SwitchLevel(building.currentLevel + 1);
				}
			}
		}

		styleText.normal.textColor = Color.green;

		GUI.Label(new Rect(325, 63, 30, 20),"" + building.stats.woodSellPerLevel[building.currentLevel - 1], styleText);
		GUI.Label(new Rect(325, 103, 30, 20),"" + building.stats.stoneSellPerLevel[building.currentLevel - 1], styleText);
		
		if(GUI.Button(new Rect(291, 195, 80.83f, 26.66f), "", styleSell))
			DestroyBuilding();

		GUI.EndGroup();
	}

	private void DrawBridgeGUI() {
		styleText.normal.textColor = Color.black;

		GUI.BeginGroup(new Rect(targetPosition.x - 37.5f, Screen.height + -targetPosition.y - 25, 75, 130));
			GUI.Label(new Rect(26, 0, 37.5f, 20), bridgeManager.buildCost.ToString(), styleText);

			if(GUI.Button(new Rect(0, 25, 75, 75), buttonBridge, new GUIStyle())) {
				if(PlayerData.Instance.goldAmount >= bridgeManager.buildCost) {
					PlayerData.Instance.goldAmount -= bridgeManager.buildCost;
					bridgeManager.Build();
					Deselect();
				}
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

	/*


	public Texture towerButton;
	public Texture lumberMillButton;
	public Texture mineButton;
	public Texture gold;
	public Texture buildingGUI;

	public PlayerData pData;

	public float delay;

	public GUIStyle styleSell;
	public GUIStyle styleBuy;
	public GUIStyle styleSellButton;
	public GUIStyle styleUpgradeButton;
	
	public GUIStyle noButtonStyle;
	public GUIStyle TowerButtonStyle;
	public GUIStyle LumberMillButtonStyle;
	public GUIStyle MineButtonStyle;
	public GUIStyle NoMoneyTowerButtonStyle;
	public GUIStyle NoMoneyLumberMillButtonStyle;
	public GUIStyle NoMoneyMineButtonStyle;
	
	private string currentTooltip;

	private GameObject selectionParticle;
	private Transform target;
	private Vector3 position;

	private BuildingManager buildingManager;
	private Bridge bridgeManager;
	private EBuildingType selectedBuilding;
	private Building building;
	private Tooltip Tooltipmanager;
	private IslandData iData;
	
	void Start() {
		Tooltipmanager = GetComponent<Tooltip>();
		DeselectBuilding();
	}

	void Update() {
		if(currentTooltip != null && currentTooltip != ""){
			Tooltipmanager.drawTooltip(currentTooltip, true);
		} else {
			Tooltipmanager.unloadGUI();
		}

		if(target != null)
			position = Camera.main.WorldToScreenPoint(target.position);

		if(Input.GetMouseButton(1) || Input.GetAxisRaw("ControllerB") != 0)
			DeselectBuilding();

		if(Input.GetMouseButtonDown(0) || Input.GetAxisRaw("ControllerA") != 0) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit, 100)) {
				if(hit.collider.tag != "CameraCollider") {
					if(selectedBuilding == EBuildingType.None) {
						switch(hit.transform.gameObject.GetComponent<BuildingType>().type) {
						case EBuildingType.Tower:
							building = hit.transform.parent.gameObject.GetComponent<Tower>();
							SelectBuilding(hit.transform, EBuildingType.Tower);

							break;
						case EBuildingType.LumberMill:
							building = hit.transform.parent.gameObject.GetComponent<LumberMill>();
							SelectBuilding(hit.transform, EBuildingType.LumberMill);

							break;
						case EBuildingType.Mine:
							building = hit.transform.parent.gameObject.GetComponent<Mine>();
							SelectBuilding(hit.transform, EBuildingType.Mine);

							break;
						case EBuildingType.Empty:
							iData = hit.transform.parent.GetComponent<IslandReference>().iData;
							buildingManager = hit.transform.parent.gameObject.GetComponent<BuildingManager>();

							if(iData.isUnlocked)
								SelectBuilding(hit.transform, EBuildingType.Empty);
							break;
						case EBuildingType.Bridge:
							iData = hit.transform.parent.GetComponent<IslandReference>().iData;
							bridgeManager = hit.transform.parent.gameObject.GetComponent<Bridge>();

							if(iData.isUnlocked && !bridgeManager.isMade)
								SelectBuilding(hit.transform, EBuildingType.Bridge);

							break;
						}
					}
				}
			}
		}
	}

	void OnGUI() {
		if(target != null) {
			if(selectedBuilding == EBuildingType.Empty) {
				if(iData.canHaveTower) {
					if(pData.goldAmount >= building.stats.goldCostPerLevel[building.currentLevel - 1]) {
						if(GUI.Button(new Rect(position.x - 25, Screen.height + -position.y - 100, 75, 75), new GUIContent("", "Tower"), TowerButtonStyle)) {
							CreateBuilding(EBuildingType.Tower);
							pData.goldAmount -= building.stats.goldCostPerLevel[building.currentLevel - 1];
						}
					} else {
						GUI.Button(new Rect(position.x - 25, Screen.height + -position.y - 100, 75, 75), new GUIContent("", "Tower"), NoMoneyTowerButtonStyle);
					}
				} else {
					GUI.Button(new Rect(position.x - 25, Screen.height + -position.y - 100, 75, 75), new GUIContent("", "NoBuilding"), noButtonStyle);
				}

				if(iData.canHaveLumberMill) {
					if(pData.goldAmount >= building.stats.goldCostPerLevel[building.currentLevel - 1]){
						if(GUI.Button(new Rect(position.x - 100, Screen.height + -position.y + 25, 75, 75), new GUIContent("", "LumberMill"), LumberMillButtonStyle)) {
							CreateBuilding(EBuildingType.LumberMill);
							pData.goldAmount -= building.stats.goldCostPerLevel[building.currentLevel - 1];
						}
					} else {
						GUI.Button(new Rect(position.x - 100, Screen.height + -position.y + 25, 75, 75),new GUIContent("", "LumberMill"), NoMoneyLumberMillButtonStyle);
					}
				} else {
					GUI.Button(new Rect(position.x - 100, Screen.height + -position.y + 25, 75, 75), new GUIContent("", "NoBuilding"), noButtonStyle);
				}

				if(iData.canHaveMine) {
					if(pData.goldAmount >= building.stats.goldCostPerLevel[building.currentLevel - 1]) {
						if(GUI.Button(new Rect(position.x + 50, Screen.height + -position.y + 25, 75, 75), new GUIContent("", "Mine"), MineButtonStyle)) {
							CreateBuilding(EBuildingType.Mine);
							pData.goldAmount -= building.stats.goldCostPerLevel[building.currentLevel - 1];
						}
					} else {
						GUI.Button(new Rect(position.x + 50, Screen.height + -position.y + 25, 75, 75), new GUIContent("", "Mine"), NoMoneyMineButtonStyle);
					}
				} else {
					GUI.Button(new Rect(position.x + 50, Screen.height + -position.y + 25, 75, 75), new GUIContent("", "NoBuilding"), noButtonStyle);
				}
			} else if(selectedBuilding == EBuildingType.Bridge) {
				GUI.BeginGroup(new Rect(position.x - 100, Screen.height + -position.y - 150, 200, 150));
					GUI.Box(new Rect(0, 0, 200, 150),"" +  bridgeManager.buildCost);

					if(GUI.Button(new Rect(50, 95, 100, 50), "Build Bridge")) {
						if(pData.goldAmount >= bridgeManager.buildCost) {
							pData.goldAmount -= bridgeManager.buildCost;
							bridgeManager.Build();
							DeselectBuilding();
						}
					}

				GUI.EndGroup();
			} else if(selectedBuilding != EBuildingType.Empty && selectedBuilding != EBuildingType.None) {
				GUI.BeginGroup(new Rect(position.x - 200, Screen.height + -position.y - 150, 400, 250));
					GUI.DrawTexture(new Rect(0, 0, 400, 250), buildingGUI);
		
					OpenPanel();
				GUI.EndGroup();
			}
		}
		
		currentTooltip = GUI.tooltip;
	}

	void OpenPanel() {
		Debug.Log(building);
		if(building.currentLevel != building.stats.levels) {
			GUI.Label(new Rect(60, 63, 30, 20), "" + building.stats.woodCostPerLevel[building.currentLevel], styleBuy);
			GUI.Label(new Rect(60, 103, 30, 20), "" + building.stats.stoneCostPerLevel[building.currentLevel], styleBuy);
			
			if(GUI.Button(new Rect(32, 170, 75, 50), "", styleUpgradeButton)) {
				if(PlayerData.Instance.woodAmount >= building.stats.woodCostPerLevel[building.currentLevel]) {
					if(PlayerData.Instance.stoneAmount >= building.stats.stoneCostPerLevel[building.currentLevel]) {
						PlayerData.Instance.stoneAmount -= building.stats.stoneCostPerLevel[building.currentLevel];
						PlayerData.Instance.woodAmount -= building.stats.woodCostPerLevel[building.currentLevel];
						building.SwitchLevel(building.currentLevel + 1);
					}
				}
			}
		}

		GUI.Label(new Rect(325, 63, 30, 20),"" + building.stats.woodSellPerLevel[building.currentLevel], styleSell);
		GUI.Label(new Rect(325, 103, 30, 20),"" + building.stats.stoneSellPerLevel[building.currentLevel], styleSell);
		
		if(GUI.Button(new Rect(292, 170, 75, 50), "", styleSellButton))
			DestroyBuilding();
	}

	IEnumerator Delay() {
		yield return new WaitForSeconds(delay);
	}

	void SelectBuilding(Transform target, EBuildingType type) {
		this.target = target;

		selectionParticle = Instantiate(Resources.Load("Particles/SelectionParticle"), target.position, Quaternion.identity) as GameObject;
		position = Camera.main.WorldToScreenPoint(target.position);
		selectedBuilding = type;

		StartCoroutine("Delay");
	}

	void DeselectBuilding() {
		if(selectionParticle != null)
			Destroy(selectionParticle);

		selectedBuilding = EBuildingType.None;
		buildingManager = null;
		bridgeManager = null;
		building = null;
		target = null;
	}

	void CreateBuilding(EBuildingType type) {
		buildingManager.CreateBuilding(type);

		DeselectBuilding();
	}

	void DestroyBuilding() {
		buildingManager = building.transform.parent.GetComponent<BuildingManager>();

		PlayerData.Instance.stoneAmount += building.stats.stoneSellPerLevel[building.currentLevel];
		PlayerData.Instance.woodAmount += building.stats.woodSellPerLevel[building.currentLevel];
		
		buildingManager.DestroyBuilding(building);
		
		DeselectBuilding();
	}
}
*/
