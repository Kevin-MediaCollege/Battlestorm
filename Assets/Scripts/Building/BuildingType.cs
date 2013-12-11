using UnityEngine;
using System.Collections;

public enum EBuildingType {
	None = -1,
	Empty = 0,
	Tower = 1,
	LumberMill = 2,
	Mine = 3
}

public class BuildingType:MonoBehaviour {
	public EBuildingType type;
}