using UnityEngine;
using System.Collections;

public class Tower:Building {

	public enum TowerType {
		Normal = 1,
		Ice = 2,
		Fire = 3
	};

	public TowerType towerType; // The type of the Tower.
	
	public GameObject target; // The Enemy Target of the Tower.

	public Transform top; // Reference to the rotating top of a Arrow Tower.

	public AudioClip shotSound; // Sound of tower Shooting (not of the projectile).

	public GameObject arrowPosition; // The position the arrow is being spawned at.

	new void Start() {
		base.Start();

		UpdateArt();

		if(towerType == TowerType.Normal) {
			top = transform.FindChild("Art").transform.FindChild("Pivot");
			arrowPosition = transform.FindChild("Art").transform.FindChild("Pivot").transform.FindChild("ArrowPosition").gameObject;
		}

		if(towerType == TowerType.Fire){
			arrowPosition = transform.FindChild("Art").transform.FindChild("ArrowPosition").gameObject;
		}

		StartCoroutine("Tick");

	}

	void FixedUpdate(){
		//If he doesnt have a target find a new one.

		if(target == null || target.GetComponent<Enemy>().isDead){
			SearchForNewTarget();
		}

		//Normal/Arrow Type Tower behavior.
		if(towerType == TowerType.Normal) {

			if(top == null || arrowPosition == null){
				//If it cant find a top or a ArrowPosition.
				StartCoroutine("getPivot");
			}

			if(target != null && top != null) {
				Vector3 lookPos = target.transform.position - top.transform.position;

				Quaternion rotation = Quaternion.LookRotation(lookPos);
				rotation.x = 0;
				rotation.z = 0;

				top.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 50);
			}

		}

	}

	protected override IEnumerator Tick() {

		while(true) {

			yield return new WaitForSeconds(stats.speedPerLevel[currentLevel - 1]);

			if(target != null) {

				if(Vector3.Distance(transform.position, target.transform.position) > stats.rangePerLevel[currentLevel - 1]){
					SearchForNewTarget();
				}
				
				Fire();
			}

		}

	}

	void SearchForNewTarget() {
		//Search for a new Enemy Target.

		GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = 0;
		
		foreach(GameObject go in targets) {

			if(Vector3.Distance(go.transform.position, transform.position) <= stats.rangePerLevel[currentLevel - 1]) {

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
		//Fire a Projectile Towards the Enemy Target.

		if(target != null) {

			if(!target.GetComponent<Enemy>().isDead) {

				if(towerType == TowerType.Normal) {

					if(arrowPosition != null){
						Projectile projectile = (Instantiate(Resources.Load("Prefabs/Projectile/Arrow"), arrowPosition.transform.position, top.transform.rotation) as GameObject).GetComponent<Projectile>();
						projectile.target = target.transform;
						projectile.damage = stats.damagePerLevel[currentLevel - 1];
						projectile.targetScript = target.gameObject.GetComponent<Enemy>();
						
						GetComponent<AudioSource>().PlayOneShot(shotSound);
					}

				} else if(towerType == TowerType.Ice) {

					Projectile projectile = (Instantiate(Resources.Load("Prefabs/Projectile/Ice"), transform.position + new Vector3(0,6,0), Quaternion.identity) as GameObject).GetComponent<Projectile>();
					
					projectile.target = target.transform;
					projectile.damage = stats.damagePerLevel[currentLevel - 1];
					projectile.targetScript = target.gameObject.GetComponent<Enemy>();

					target.GetComponent<Enemy>().Slowdown();

				} else if(towerType == TowerType.Fire) {

					Projectile projectile = (Instantiate(Resources.Load("Prefabs/Projectile/Fire"), transform.position + new Vector3(0,6,0), Quaternion.identity) as GameObject).GetComponent<Projectile>();
					
					projectile.target = target.transform;
					projectile.damage = stats.damagePerLevel[currentLevel - 1];
					projectile.targetScript = target.gameObject.GetComponent<Enemy>();

					target.GetComponent<Enemy>().Burn();

				}

			}

		}

	}

	IEnumerator getPivot(){
		//Get Reference to the Towers pivot point or Arrow Position.

		yield return new WaitForSeconds(0.05f);

		if(top == null){
		top = transform.FindChild("Art").transform.FindChild("Pivot");
		}

		if(arrowPosition == null){
		arrowPosition = transform.FindChild("Art").transform.FindChild("Pivot").transform.FindChild("ArrowPosition").gameObject;
		}

	}

}
