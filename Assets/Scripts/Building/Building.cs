using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
	public enum BuildingType{
		Tower = 1,
		WoodWorks = 2,
		Mine = 3
	};
	public enum Upgrade{
		one =1,
		two =2,
		three=3,
		four=4,
		five=5
	};

	public int upgradeLevel;
	public BuildingType type;
	public bool enemyInteractable;
	public PlayerData player;

	public int currentlevel = 1;
	public int timePerTick;
}
