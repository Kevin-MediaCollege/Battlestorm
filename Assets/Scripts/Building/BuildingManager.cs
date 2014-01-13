using UnityEngine;
using System.Collections;

public class BuildingManager:MonoBehaviour {
	public GameObject platform;

	public bool isUnlocked;

	private GameObject building;
	private GameObject position;

	void Start() {
		position = transform.FindChild("BuildingPosition").gameObject;
	}

	public void CreateBuilding(EBuildingType type) {
		platform.renderer.enabled = false;
		platform.GetComponent<BoxCollider>().enabled = false;

		building = Instantiate(Resources.Load("Prefabs/Buildings/" + type), position.transform.position, position.transform.rotation) as GameObject;
		building.transform.parent = this.transform;

		if(type == EBuildingType.Tower)
			building.transform.name = "Tower";
	}

	public void DestroyBuilding(Building building) {
		platform.renderer.enabled = true;
		platform.GetComponent<BoxCollider>().enabled = true;

		Destroy(building.gameObject);
	}
}
