using UnityEngine;
using System.Collections;

public class SpawnBuilding:MonoBehaviour {
	public enum BuildingType {
		Tower = 1,
		WoodWorks = 2,
		Mine = 3
	};

	public GameObject buildingObj;

	private Transform buildingPos;

	void Start () {
		buildingPos = transform.FindChild("BuildingPosition");
	}

	public void createBuilding(int type){
		Debug.Log(type);

		switch (type) {
		case 1:
			buildingObj = Instantiate (Resources.Load ("Prefabs/Test/Tower"), buildingPos.position, Quaternion.identity) as GameObject;
			buildingObj.transform.parent = this.transform;
			break;
		case 2:
			buildingObj = Instantiate (Resources.Load ("Prefabs/Test/LumberMill"), buildingPos.position, Quaternion.identity) as GameObject;
			buildingObj.transform.parent = this.transform;
			break;
		case 3:
			buildingObj = Instantiate (Resources.Load ("Prefabs/Test/Mine"), buildingPos.position, Quaternion.identity) as GameObject;
			buildingObj.transform.parent = this.transform;
			break;
		}
	}

}
