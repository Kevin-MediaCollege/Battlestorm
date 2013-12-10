using UnityEngine;
using System.Collections;

public class Tower:Building {	
	public enum StoneSell {
		Price1 = 0,
		Price2 = 0,
		Price3 = 10,
		Price4 = 10,
		Price5 = 210,
		Price6 = 500
	};
	
	public enum WoodSell {
		Price1 = 10,
		Price2 = 50,
		Price3 = 150,
		Price4 = 300,
		Price5 = 600,
		Price6 = 1000
	};

	private enum WoodCostPerUpgrade {
		Upgrade2 = 0,
		Upgrade3 = 300,
		Upgrade4 = 500,
		Upgrade5 = 3000
	};
	
	private enum StoneCostPerUpgrade {
		Upgrade2 = 250,
		Upgrade3 = 600,
		Upgrade4 = 1200,
		Upgrade5 = 3000
	};

	public static string name = "Tower";

	public float damage;
	public float maxRange;

	private GameObject target;
	
	void Start() {
		base.Start();

		path += "Tower/Tower";

		woodCostForNextLevel = (int)WoodCostPerUpgrade.Upgrade2;
		stoneCostForNextLevel= (int)StoneCostPerUpgrade.Upgrade2;
		woodSellPrice = (int)WoodSell.Price1;
		stoneSellPrice = (int)StoneSell.Price1;
		
		interactable = false;
		currentLevel = Upgrade.one;
		
		UpdateArt();
		StartCoroutine("tick");
	}

	IEnumerator tick() {
		while(true) {
			yield return new WaitForSeconds(timePerTick);
			
			if(target == null || Vector3.Distance(transform.position, target.transform.position) > maxRange)
				SearchForNewTarget();

			if(target != null)
				Fire();
		}
	}

	void SearchForNewTarget() {
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float closestDistance = 0;

		foreach(GameObject go in targets) {
			if(closest == null) {
				closest = go;
				closestDistance = Vector3.Distance(transform.position, go.transform.position);
			}

			if(closest != go)
				continue;

			if(Vector3.Distance(go.transform.position, closest.transform.position) < closestDistance)
				closest = go;
		}

		target = closest;
	}

	void Fire() {
		GameObject projectile = Instantiate(Resources.Load("Prefabs/Projectile/Projectile"), transform.position, Quaternion.identity) as GameObject;
		//projectile.GetComponent<Projectile>().Target = target.transform;

		target.gameObject.GetComponent<Enemy>().Damage(damage);
	}
	
	public void SwitchLevel(Upgrade level) {
		if(level > maxLevel)
			return;
		
		currentLevel = level;
		
		switch(level){
		case Upgrade.one:
			woodCostForNextLevel = (int)WoodCostPerUpgrade.Upgrade2;
			stoneCostForNextLevel = (int)StoneCostPerUpgrade.Upgrade2;
			woodSellPrice = (int)WoodSell.Price1;
			stoneSellPrice = (int)StoneSell.Price1;
			
			break;
		case Upgrade.two:
			woodCostForNextLevel = (int)WoodCostPerUpgrade.Upgrade3;
			stoneCostForNextLevel = (int)StoneCostPerUpgrade.Upgrade3;
			woodSellPrice = (int)WoodSell.Price2;
			stoneSellPrice = (int)StoneSell.Price2;
			
			break;
		case Upgrade.three:
			woodCostForNextLevel = (int)WoodCostPerUpgrade.Upgrade4;
			stoneCostForNextLevel = (int)StoneCostPerUpgrade.Upgrade4;
			woodSellPrice = (int)WoodSell.Price3;
			stoneSellPrice = (int)StoneSell.Price3;
			
			break;
		case Upgrade.four:
			woodCostForNextLevel = (int)WoodCostPerUpgrade.Upgrade5;
			stoneCostForNextLevel = (int)StoneCostPerUpgrade.Upgrade5;
			woodSellPrice = (int)WoodSell.Price4;
			stoneSellPrice = (int)StoneSell.Price4;
			
			break;
		case Upgrade.five:
			woodCostForNextLevel = int.MaxValue;
			stoneCostForNextLevel = int.MaxValue;
			woodSellPrice = (int)WoodSell.Price5;
			stoneSellPrice = (int)StoneSell.Price5;
			
			break;
		}
		
		UpdateArt();
	}
}
