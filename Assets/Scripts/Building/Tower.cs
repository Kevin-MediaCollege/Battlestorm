using UnityEngine;
using System.Collections;

public class Tower:Building {
	public enum TowerType {
		Normal = 1,
		Ice = 2,
		Fire = 3
	};

	public TowerType towerType;
	
	private GameObject target;
	private Transform top;

	public AudioClip shotSound;
	private Vector3 arrowPosition;

	new void Start() {
		base.Start();

		UpdateArt();

		if(towerType == TowerType.Normal) {
			top = transform.FindChild("Art").transform.FindChild("Pivot");
			arrowPosition = transform.FindChild("Art").transform.FindChild("Pivot").transform.FindChild("ArrowPosition").transform.position;
		}

		StartCoroutine("Tick");
	}

	void Update() {
		if(target != null)
			Debug.DrawLine(transform.position, target.transform.position);
	}

	void FixedUpdate(){
		if(target == null || target.GetComponent<Enemy>().isDead)
			SearchForNewTarget();

		if(target != null && top != null){
			Vector3 lookPos = target.transform.position - top.transform.position;
		Quaternion rotation = Quaternion.LookRotation(lookPos);
			rotation.x = 0;
			rotation.z = 0;
		top.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 50);
		}
			//top.transform.LookAt(target.transform.position);
	}

	protected override IEnumerator Tick() {
		while(true) {
			yield return new WaitForSeconds(stats.speedPerLevel[currentLevel - 1]);

			if(target != null) {
				if(Vector3.Distance(transform.position, target.transform.position) > stats.rangePerLevel[currentLevel - 1])
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
		if(target != null) {
			if(!target.GetComponent<Enemy>().isDead) {
				if(towerType == TowerType.Normal) {
					Projectile projectile = (Instantiate(Resources.Load("Prefabs/Projectile/Arrow"), arrowPosition, Quaternion.identity) as GameObject).GetComponent<Projectile>();

					projectile.target = target.transform;
					projectile.damage = stats.damagePerLevel[currentLevel - 1];
					projectile.targetScript = target.gameObject.GetComponent<Enemy>();

					audio.PlayOneShot(shotSound);
				} else if(towerType == TowerType.Ice) {
					Projectile projectile = (Instantiate(Resources.Load("Prefabs/Projectile/Ice"), arrowPosition, Quaternion.identity) as GameObject).GetComponent<Projectile>();
					
					projectile.target = target.transform;
					projectile.damage = stats.damagePerLevel[currentLevel - 1];
					projectile.targetScript = target.gameObject.GetComponent<Enemy>();
					
					audio.PlayOneShot(shotSound);

					target.GetComponent<Enemy>().Slowdown();
				} else if(towerType == TowerType.Fire) {
					Projectile projectile = (Instantiate(Resources.Load("Prefabs/Projectile/Fire"), arrowPosition, Quaternion.identity) as GameObject).GetComponent<Projectile>();
					
					projectile.target = target.transform;
					projectile.damage = stats.damagePerLevel[currentLevel - 1];
					projectile.targetScript = target.gameObject.GetComponent<Enemy>();
					
					audio.PlayOneShot(shotSound);

					target.GetComponent<Enemy>().Burn();
				}
			}
		}
	}

	IEnumerator getPivot(){
		yield return new WaitForSeconds(0.05f);
		top = transform.FindChild("Art").transform.FindChild("Pivot");
		arrowPosition = transform.FindChild("Art").transform.FindChild("Pivot").transform.FindChild("ArrowPosition").transform.position;
	}
}
