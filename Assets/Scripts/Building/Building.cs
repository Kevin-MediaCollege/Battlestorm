using UnityEngine;
using System.Collections;

public class Building:MonoBehaviour {
	public enum BuildingType {
		Tower = 1,
		WoodWorks = 2,
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

	public PlayerData playerData;
	public BuildingType type;
	public GameObject art;

	public bool interactable = false;

	public Upgrade currentLevel = Upgrade.zero;
	public Upgrade maxLevel = Upgrade.five;

	public int woodCostForNextLevel = 0;
	public int stoneCostForNextLevel = 0;

	public int woodSellPrice = 0;
	public int stoneSellPrice = 0;

	public int timePerTick = 0;

	protected void Start() {
		playerData = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerData>();
	}

	public virtual void SwitchLevel(Upgrade level) { }

	protected void UpdateArt(string path) {
		if(art != null)
			Destroy(art);
		
		art = Instantiate(Resources.Load(path + (int)currentLevel), transform.position, Quaternion.identity) as GameObject;
		art.transform.parent = this.transform;
	}
}
