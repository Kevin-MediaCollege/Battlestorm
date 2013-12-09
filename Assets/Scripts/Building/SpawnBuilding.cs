using UnityEngine;
using System.Collections;

public class SpawnBuilding:MonoBehaviour {
	private GameObject building;
	private Transform buildingPos;

	void Start() {
		buildingPos = transform.FindChild("BuildingPosition");
	}

	public void CreateBuilding(Building.BuildingType type) {
		switch(type) {
		case Building.BuildingType.Tower:
			building = Instantiate(Resources.Load("Prefabs/Test/Tower"), buildingPos.position, Quaternion.identity) as GameObject;
			break;
		case Building.BuildingType.WoodWorks:
			building = Instantiate(Resources.Load("Prefabs/Test/LumberMill"), buildingPos.position, Quaternion.identity) as GameObject;
			break;
		case Building.BuildingType.Mine:
			building = Instantiate(Resources.Load("Prefabs/Test/Mine"), buildingPos.position, Quaternion.identity) as GameObject;
			break;
		}
	}
}
