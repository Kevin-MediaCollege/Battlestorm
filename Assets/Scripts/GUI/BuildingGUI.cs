using UnityEngine;
using System.Collections;

public class BuildingGUI:MonoBehaviour {
	public Texture towerButton;
	public Texture lumberMillButton;
	public Texture mineButton;
	public Texture gold;
	public Texture buildingGUI;

	public PlayerData pData;

	public int BuildingCost = 50;

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
			Tooltipmanager.drawTooltip(currentTooltip,true);
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
							iData = hit.transform.GetComponent<IslandReference>().iData;
							buildingManager = hit.transform.gameObject.GetComponent<BuildingManager>();

							if(iData.isUnlocked)
								SelectBuilding(hit.transform, EBuildingType.Empty);
							break;
						case EBuildingType.Bridge:
							iData = hit.transform.GetComponent<IslandReference>().iData;
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
					if(pData.goldAmount >= BuildingCost) {
						if(GUI.Button(new Rect(position.x - 25, Screen.height + -position.y - 100, 75, 75), new GUIContent("", "Tower"), TowerButtonStyle)) {
							CreateBuilding(EBuildingType.Tower);
							pData.goldAmount -= BuildingCost;
						}
					} else {
						GUI.Button(new Rect(position.x - 25, Screen.height + -position.y - 100, 75, 75), new GUIContent("", "Tower"), NoMoneyTowerButtonStyle);
					}
				} else {
					GUI.Button(new Rect(position.x - 25, Screen.height + -position.y - 100, 75, 75), new GUIContent("", "NoBuilding"), noButtonStyle);
				}

				if(iData.canHaveLumberMill) {
					if(pData.goldAmount >= BuildingCost){
						if(GUI.Button(new Rect(position.x - 100, Screen.height + -position.y + 25, 75, 75), new GUIContent("", "LumberMill"), LumberMillButtonStyle)) {
							CreateBuilding(EBuildingType.LumberMill);
							pData.goldAmount -= BuildingCost;
						}
					} else {
						GUI.Button(new Rect(position.x - 100, Screen.height + -position.y + 25, 75, 75),new GUIContent("", "LumberMill"), NoMoneyLumberMillButtonStyle);
					}
				} else {
					GUI.Button(new Rect(position.x - 100, Screen.height + -position.y + 25, 75, 75), new GUIContent("", "NoBuilding"), noButtonStyle);
				}

				if(iData.canHaveMine) {
					if(pData.goldAmount >= BuildingCost) {
						if(GUI.Button(new Rect(position.x + 50, Screen.height + -position.y + 25, 75, 75), new GUIContent("", "Mine"), MineButtonStyle)) {
							CreateBuilding(EBuildingType.Mine);
							pData.goldAmount -= BuildingCost;
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
		if(building.currentLevel != building.maxLevel) {
			GUI.Label(new Rect(60, 63, 30, 20), "" + building.woodCost, styleBuy);
			GUI.Label(new Rect(60, 103, 30, 20), "" + building.stoneCost, styleBuy);
			
			if(GUI.Button(new Rect(32, 170, 75, 50), "", styleUpgradeButton)) {
				if(PlayerData.Instance.woodAmount >= building.woodCost) {
					if(PlayerData.Instance.stoneAmount >= building.stoneCost) {
						PlayerData.Instance.stoneAmount -= building.stoneCost;
						PlayerData.Instance.woodAmount -= building.woodCost;
						building.SwitchLevel(building.currentLevel + 1);
					}
				}
			}
		}

		GUI.Label(new Rect(325, 63, 30, 20),"" + building.woodSell, styleSell);
		GUI.Label(new Rect(325, 103, 30, 20),"" + building.stoneSell, styleSell);
		
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
		
		PlayerData.Instance.stoneAmount += building.stoneSell;
		PlayerData.Instance.woodAmount += building.woodSell;
		
		buildingManager.DestroyBuilding(building);
		
		DeselectBuilding();
	}
}
