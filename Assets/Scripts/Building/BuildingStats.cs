﻿using UnityEngine;
using System.Collections;

public class BuildingStats:MonoBehaviour {

	public int levels; // amount of levels the building has.
	public float sellRate; // percentage of selling the building.
	
	public int[] resourcesPerTick; // Amount of resources per tick for each Level.

	public int[] goldCostPerLevel; // Gold Cost for each Level.
	public int[] stoneCostPerLevel; // Stone Cost for each Level.
	public int[] woodCostPerLevel; // Wood Cost for each Level.

	public float[] rangePerLevel; // Towers Range for each Level.
	public float[] damagePerLevel; // Towers Damage for each Level.
	public float[] speedPerLevel; // Towers Speed for each Level

}
