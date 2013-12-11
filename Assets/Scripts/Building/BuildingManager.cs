using UnityEngine;
using System.Collections;

public class BuildingManager:MonoBehaviour {
	private GameObject building;
	private Vector3 position;

	public bool isUnlocked;
	void Start() {
		position = transform.FindChild("BuildingPosition").position;
	}

	public void CreateBuilding(EBuildingType type) {
		building = Instantiate(Resources.Load("Prefabs/Buildings/" + type), position, Quaternion.identity) as GameObject;
		building.transform.parent = this.transform;
	}

	public void DestroyBuilding(Building building) {
		Destroy(building.gameObject);
	}
}
