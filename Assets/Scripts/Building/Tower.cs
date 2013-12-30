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
	public Transform top;

	public AudioClip shotSound;
	private Vector3 arrowPosition;
	void Start() {
		currentLevel = Upgrade.Level1;

		stoneCost = (int)StoneCost.Level2;
		stoneSell = (int)StoneSell.Level1;
		
		woodCost = (int)WoodCost.Level2;
		woodSell = (int)WoodSell.Level1;

		UpdateArt();
		top = transform.FindChild("Art").transform.FindChild("Pivot");
		arrowPosition = transform.FindChild("Art").transform.FindChild("Pivot").transform.FindChild("ArrowPosition").transform.position;
		StartCoroutine("Tick");
	}

	void Update() {
		if(target != null)
			Debug.DrawLine(transform.position, target.transform.position);
	}

	void FixedUpdate(){
		if(target == null)
			SearchForNewTarget();

		if(target != null)
			top.transform.LookAt(target.transform.position);
	}

	protected override IEnumerator Tick() {
		while(true) {
			yield return new WaitForSeconds(tickDelay);
			

			
			if(target != null) {
				if(Vector3.Distance(transform.position, target.transform.position) > maxRange)
					SearchForNewTarget();
				
				Fire();
			}
		}
	}

	void SearchForNewTarget() {
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = 0;
		
		foreach(GameObject go in targets) {
			if(Vector3.Distance(go.transform.position, transform.position) <= maxRange) {
				if(closest == null) {
					closest = go;
					distance = Vector3.Distance(go.transform.position, transform.position);
				}
				
				if(Vector3.Distance(go.transform.position, transform.position) < distance) {
					closest = go;
					distance = Vector3.Distance(go.transform.position, transform.position);
				}
			}
		}
		
		target = closest;
	}

	void Fire() {
		if(target != null){
		GameObject projectile = Instantiate(Resources.Load("Prefabs/Projectile/Projectile"), arrowPosition, Quaternion.identity) as GameObject;
		projectile.GetComponent<Projectile>().target = target.transform;
			audio.PlayOneShot(shotSound);
		}
		if(target != null)
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
