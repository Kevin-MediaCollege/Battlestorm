using UnityEngine;
using System.Collections;

public class LumberMill : Building {
	public enum resourcesPerUpgrade{
		Upgrade1 = 10,
		Upgrade2 = 15,
		Upgrade3 = 25,
		Upgrade4 = 40,
		Upgrade5 = 60
	};
	private enum woodcostPerUpgrade{
		Upgrade2 = 250,
		Upgrade3 = 600,
		Upgrade4 = 1200,
		Upgrade5 = 3000
	};
	private enum stonecostPerUpgrade{
		Upgrade2 = 0,
		Upgrade3 = 0,
		Upgrade4 = 200,
		Upgrade5 = 3000
	};
	private PlayerData player;
	public int currentlevel;
	public int timePerTick;
	public resourcesPerUpgrade resourcesPerTick;
	public GameObject art;

	public int woodcostfornextlevel;
	public int stonecostfornextlevel;
	void Start () {
		woodcostfornextlevel = (int)woodcostPerUpgrade.Upgrade2;
		stonecostfornextlevel= (int)stonecostPerUpgrade.Upgrade2;
		player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerData>();
		currentlevel = 1;
		timePerTick = 3;
		upgradeLevel = 1;
		enemyInteractable = false;
		resourcesPerTick = resourcesPerUpgrade.Upgrade1;
		StartCoroutine("MineTick");
		changeArt(currentlevel);
	}
	
	IEnumerator MineTick(){
		yield return new WaitForSeconds(timePerTick);
		player.woodAmount += resourcesPerTick;
		StartCoroutine("MineTick");
	}

	public void SwitchLevel(int upgradelevel){
		currentlevel++;
		switch(upgradelevel){
		case 1:
			resourcesPerTick = resourcesPerUpgrade.Upgrade1;
			changeArt(1);
			woodcostfornextlevel = (int)woodcostPerUpgrade.Upgrade2;
			stonecostfornextlevel = (int)stonecostPerUpgrade.Upgrade2;
			break;
		case 2:
			resourcesPerTick = resourcesPerUpgrade.Upgrade2;
			changeArt(2);
			woodcostfornextlevel = (int)woodcostPerUpgrade.Upgrade3;
			stonecostfornextlevel = (int)stonecostPerUpgrade.Upgrade3;
			break;
		case 3:
			resourcesPerTick = resourcesPerUpgrade.Upgrade3;
			changeArt(3);
			woodcostfornextlevel = (int)woodcostPerUpgrade.Upgrade4;
			stonecostfornextlevel = (int)stonecostPerUpgrade.Upgrade4;
			break;
		case 4:
			resourcesPerTick = resourcesPerUpgrade.Upgrade4;
			changeArt(4);
			woodcostfornextlevel = (int)woodcostPerUpgrade.Upgrade5;
			stonecostfornextlevel = (int)stonecostPerUpgrade.Upgrade5;
			break;
		case 5:
			resourcesPerTick = resourcesPerUpgrade.Upgrade5;
			changeArt(5);
			woodcostfornextlevel = 999999999;
			stonecostfornextlevel = 999999999;
			break;
		}
	}

	void changeArt(int upgrade){
		if(art != null){
			Destroy(art);
		}
		switch(upgrade){
		case 1:
			art = Instantiate(Resources.Load("Prefabs/Test/LumberMill/Mill"),transform.position,Quaternion.identity) as GameObject;
			break;
		case 2:
			art = Instantiate(Resources.Load("Prefabs/Test/LumberMill/Mill2"),transform.position,Quaternion.identity) as GameObject;
			break;
		case 3:
			art = Instantiate(Resources.Load("Prefabs/Test/LumberMill/Mill3"),transform.position,Quaternion.identity) as GameObject;
			break;
		case 4:
			art = Instantiate(Resources.Load("Prefabs/Test/LumberMill/Mill4"),transform.position,Quaternion.identity) as GameObject;
			break;
		case 5:
			art = Instantiate(Resources.Load("Prefabs/Test/LumberMill/Mill5"),transform.position,Quaternion.identity) as GameObject;
			break;
		}
		art.transform.parent = this.transform;
	}
}
