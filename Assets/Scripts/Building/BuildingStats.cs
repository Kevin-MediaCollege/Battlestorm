using UnityEngine;
using System.Collections;

public class BuildingStats:MonoBehaviour {
	public int levels;
	public float sellRate;
	
	public int[] resourcesPerTick;

	public int[] goldCostPerLevel;
	public int[] stoneCostPerLevel;
	public int[] woodCostPerLevel;

	public float[] rangePerLevel;
	public float[] damagePerLevel;
	public float[] speedPerLevel;
}
