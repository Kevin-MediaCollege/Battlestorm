using UnityEngine;
using System.Collections;

/// <summary>
/// Building Types of Buildings.
/// </summary>
public enum EBuildingType {
	None = -1,
	Empty = 0,
	TowerNormal = 1,
	LumberMill = 2,
	Mine = 3,
	Bridge = 4,
	TowerIce = 5,
	TowerFire = 6
}

public class BuildingType:MonoBehaviour {
	// Reference to the Buildings Type.

	public EBuildingType type;

}