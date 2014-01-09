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
		if(target == null || target.GetComponent<Enemy>().isdead)
			SearchForNewTarget();

		if(target != null && top != null)
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
			if(!target.GetComponent<Enemy>().isdead){
				GameObject projectile = Instantiate(Resources.Load("Prefabs/Projectile/Projectile"), arrowPosition, Quaternion.identity) as GameObject;
				Projectile proj = projectile.GetComponent<Projectile>();
				proj.target = target.transform;
				proj.damage = damage;
				proj.targetScript = target.gameObject.GetComponent<Enemy>();
				audio.PlayOneShot(shotSound);
			}
		}
	}

	public override void SwitchLevel(Upgrade newLevel) {
		if(newLevel > maxLevel)
			return;
		switch(newLevel){
		case Upgrade.Level1:
			stoneCost = (int)StoneCost.Level2;
			stoneSell = (int)StoneSell.Level1;
			
			woodCost = (int)WoodCost.Level2;
			woodSell = (int)WoodSell.Level1;

			break;
		case Upgrade.Level2:
			stoneCost = (int)StoneCost.Level3;
			stoneSell = (int)StoneSell.Level2;
			
			woodCost = (int)WoodCost.Level3;
			woodSell = (int)WoodSell.Level2;

			break;
		case Upgrade.Level3:
			stoneCost = (int)StoneCost.Level4;
			stoneSell = (int)StoneSell.Level3;
			
			woodCost = (int)WoodCost.Level4;
			woodSell = (int)WoodSell.Level3;

			break;
		case Upgrade.Level4:
			stoneCost = (int)StoneCost.Level4;
			stoneSell = (int)StoneSell.Level5;
			
			woodCost = (int)WoodCost.Level5;
			woodSell = (int)WoodSell.Level4;

			break;
		case Upgrade.Level5:
			stoneCost = (int)StoneCost.Level5;
			stoneSell = (int)StoneSell.Level5;
			
			woodCost = (int)WoodCost.Level5;
			woodSell = (int)WoodSell.Level5;

			break;
		}
		currentLevel = newLevel;
		
		UpdateArt();
		StartCoroutine("getPivot");
	}
	IEnumerator getPivot(){
		yield return new WaitForSeconds(0.05f);
		top = transform.FindChild("Art").transform.FindChild("Pivot");
		arrowPosition = transform.FindChild("Art").transform.FindChild("Pivot").transform.FindChild("ArrowPosition").transform.position;
	}
}
