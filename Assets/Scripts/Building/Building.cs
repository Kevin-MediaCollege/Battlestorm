using UnityEngine;
using System.Collections;

public class Building:MonoBehaviour {
	public enum BuildingType {
		Tower = 1,
		LumberMill = 2,
		Mine = 3
	};

	public enum Upgrade {
		zero = 0,
		one = 1,
		two = 2,
		three = 3,
		four = 4,
		five = 5
	};
	
	public Upgrade maxLevel;
	public bool interactable;
	public int timePerTick;

	protected Upgrade currentLevel = Upgrade.zero;
	protected PlayerData playerData;
	protected GameObject art;

	protected string path = "Prefabs/Buildings/";

	protected int woodCostForNextLevel = 0;
	protected int stoneCostForNextLevel = 0;
	protected int woodSellPrice = 0;
	protected int stoneSellPrice = 0;

	public string Path { get { return path; } }
	public int WoodCostForNextLevel { get { return woodCostForNextLevel; } }
	public int StoneCostForNextLevel { get { return stoneCostForNextLevel; } }
	public int WoodSellPrice { get { return woodSellPrice; } }
	public int StoneSellPrice { get { return stoneSellPrice; } }
	public Upgrade CurrentLevel { get { return currentLevel; } }

	protected void Start() {
		playerData = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerData>();
	}

	public virtual void SwitchLevel(Upgrade level) { }

	protected void UpdateArt() {
		if(art != null)
			Destroy(art);
		
		art = Instantiate(Resources.Load(path + (int)currentLevel), transform.position, Quaternion.identity) as GameObject;
		art.transform.parent = this.transform;
	}
}
