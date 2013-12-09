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
	public enum stoneSell{
		Price1 = 0,
		Price2 = 0,
		Price3 = 10,
		Price4 = 10,
		Price5 = 210,
		Price6 = 500
	}
	public enum woodSell{
		Price1 = 10,
		Price2 = 50,
		Price3 = 150,
		Price4 = 300,
		Price5 = 600,
		Price6 = 1000
	}
	public resourcesPerUpgrade resourcesPerTick;

	public int woodcostfornextlevel;
	public int stonecostfornextlevel;
	public int stonesellprice;
	public int woodsellprice;
	public PlayerData player;
	void Start () {
		currentlevel = 1;
		timePerTick = 3;
		player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerData>();
		woodcostfornextlevel = (int)woodcostPerUpgrade.Upgrade2;
		stonecostfornextlevel= (int)stonecostPerUpgrade.Upgrade2;
		enemyInteractable = false;
		resourcesPerTick = resourcesPerUpgrade.Upgrade1;
		StartCoroutine("MineTick");
		changeArt(currentlevel);
		stonesellprice = (int)stoneSell.Price1;
		woodsellprice = (int)woodSell.Price1;
	}
	
	IEnumerator MineTick(){
		yield return new WaitForSeconds(timePerTick);
		player.woodAmount += resourcesPerTick;
		GameObject popuptext = Instantiate(Resources.Load("Prefabs/WoodResourceText"),transform.position,Quaternion.identity) as GameObject;
		TextMesh textpop = popuptext.GetComponent<TextMesh>();
		textpop.text = "" + (int)resourcesPerTick;
		textpop.color = new Color(0.6f,0.2f,0);
		textpop.transform.parent = this.transform;
		StartCoroutine("MineTick");
	}

	public void SwitchLevel(int upgradelevel){
		currentlevel++;
		switch(currentlevel){
		case 1:
			resourcesPerTick = resourcesPerUpgrade.Upgrade1;
			changeArt(1);
			woodcostfornextlevel = (int)woodcostPerUpgrade.Upgrade2;
			stonecostfornextlevel = (int)stonecostPerUpgrade.Upgrade2;
			stonesellprice = (int)stoneSell.Price2;
			woodsellprice = (int)woodSell.Price2;
			break;
		case 2:
			resourcesPerTick = resourcesPerUpgrade.Upgrade2;
			changeArt(2);
			woodcostfornextlevel = (int)woodcostPerUpgrade.Upgrade3;
			stonecostfornextlevel = (int)stonecostPerUpgrade.Upgrade3;
			stonesellprice = (int)stoneSell.Price3;
			woodsellprice = (int)woodSell.Price3;
			break;
		case 3:
			resourcesPerTick = resourcesPerUpgrade.Upgrade3;
			changeArt(3);
			woodcostfornextlevel = (int)woodcostPerUpgrade.Upgrade4;
			stonecostfornextlevel = (int)stonecostPerUpgrade.Upgrade4;
			stonesellprice = (int)stoneSell.Price4;
			woodsellprice = (int)woodSell.Price4;
			break;
		case 4:
			resourcesPerTick = resourcesPerUpgrade.Upgrade4;
			changeArt(4);
			woodcostfornextlevel = (int)woodcostPerUpgrade.Upgrade5;
			stonecostfornextlevel = (int)stonecostPerUpgrade.Upgrade5;
			stonesellprice = (int)stoneSell.Price5;
			woodsellprice = (int)woodSell.Price5;
			break;
		case 5:
			resourcesPerTick = resourcesPerUpgrade.Upgrade5;
			changeArt(5);
			woodcostfornextlevel = 999999999;
			stonecostfornextlevel = 999999999;
			stonesellprice = (int)stoneSell.Price6;
			woodsellprice = (int)woodSell.Price6;
			break;
		}
	}

	void changeArt(int upgrade){
		if(art != null){
			Destroy(art);
		}
		switch(upgrade){
		case 1:
			Debug.Log(upgrade);

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
