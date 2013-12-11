using UnityEngine;
using System.Collections;

public class Tower:Building {	
	public enum StoneCost {
		Level1 = 0,
		Level2 = 250,
		Level3 = 600,
		Level4 = 1200,
		Level5 = 3000
	};
	
	public enum StoneSell {
		Level1 = 0,
		Level2 = 10,
		Level3 = 10,
		Level4 = 210,
		Level5 = 500
	};
	
	private enum WoodCost {
		Level1 = 0,
		Level2 = 150,
		Level3 = 300,
		Level4 = 500,
		Level5 = 3000
	};
	
	public enum WoodSell {
		Level1 = 0,
		Level2 = 10,
		Level3 = 10,
		Level4 = 210,
		Level5 = 500
	};

	public float damage;
	public float maxRange;

	private GameObject target;
	
	void Start() {
		currentLevel = Upgrade.Level1;

		stoneCost = (int)StoneCost.Level2;
		stoneSell = (int)StoneSell.Level1;
		
		woodCost = (int)WoodCost.Level2;
		woodSell = (int)WoodSell.Level1;

		UpdateArt();
		StartCoroutine("Tick");
	}

	protected override IEnumerator Tick() {
		while(true) {
			yield return new WaitForSeconds(tickDelay);
			
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
	
	public override void SwitchLevel(Upgrade newLevel) {
		if(newLevel > maxLevel)
			return;
		
		currentLevel = newLevel;
		
		stoneCost++;
		stoneSell++;
		
		woodCost++;
		woodSell++;
		
		UpdateArt();
	}
}
