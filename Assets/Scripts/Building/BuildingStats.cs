using UnityEngine;
using System.Collections;

public class BuildingStats:MonoBehaviour {
	public int levels;
	public float sellRate;
	
	public float[] resourcesPerTick;

	public float[] goldCostPerLevel;
	public float[] stoneCostPerLevel;
	public float[] woodCostPerLevel;

	public int[] rangePerLevel;
	public int[] damagePerLevel;
	public float[] speedPerLevel;
}
