using UnityEngine;
using System.Collections;

public class BuildingStats:MonoBehaviour {
	public int levels;
	public float sellRate;
	
	public int[] resourcesPerTick;

	public int[] goldCostPerLevel;
	public int[] stoneCostPerLevel;
	public int[] woodCostPerLevel;

	public int[] rangePerLevel;
	public int[] damagePerLevel;
	public int[] speedPerLevel;
}
