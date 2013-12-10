using UnityEngine;
using System.Collections;

public class BuildingManager:MonoBehaviour {
	private GameObject building;
	private Transform buildingPos;

	void Start() {
		buildingPos = transform.FindChild("BuildingPosition");
	}

	public void CreateBuilding(Building.BuildingType type) {
		switch(type) {
		case Building.BuildingType.Tower:
			building = Instantiate(Resources.Load("Prefabs/Buildings/Tower"), buildingPos.position, Quaternion.identity) as GameObject;
			break;
		case Building.BuildingType.LumberMill:
			building = Instantiate(Resources.Load("Prefabs/Buildings/LumberMill"), buildingPos.position, Quaternion.identity) as GameObject;
			break;
		case Building.BuildingType.Mine:
			building = Instantiate(Resources.Load("Prefabs/Buildings/Mine"), buildingPos.position, Quaternion.identity) as GameObject;
			break;
		}

		building.transform.parent = this.transform;
	}

	public void DestroyBuilding(Building building) {
		Destroy(building.gameObject);
	}
}
