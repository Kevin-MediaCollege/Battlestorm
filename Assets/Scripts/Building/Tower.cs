using UnityEngine;
using System.Collections;

public class Tower:Building {
	public enum TowerType {
		Normal = 1,
		Ice = 2
	};

	public TowerType towerType;

	public float damage;
	public float maxRange;
	
	private GameObject target;
	private Transform top;

	public AudioClip shotSound;
	private Vector3 arrowPosition;

	new void Start() {
		base.Start();

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
		if(target == null || target.GetComponent<Enemy>().isDead)
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

	public override void SwitchLevel(int newLevel) {
		base.SwitchLevel(newLevel);

		damage *= 2;
		tickDelay /= 2;
	}

	void Fire() {
		if(target != null) {
			if(!target.GetComponent<Enemy>().isDead) {
				GameObject projectile = Instantiate(Resources.Load("Prefabs/Projectile/Projectile"), arrowPosition, Quaternion.identity) as GameObject;
				Projectile proj = projectile.GetComponent<Projectile>();

				proj.target = target.transform;
				proj.damage = damage;
				proj.targetScript = target.gameObject.GetComponent<Enemy>();

				audio.PlayOneShot (shotSound);

				if(towerType == TowerType.Ice)
					target.GetComponent<Enemy>().Slowdown();
			}
		}
	}

	IEnumerator getPivot(){
		yield return new WaitForSeconds(0.05f);
		top = transform.FindChild("Art").transform.FindChild("Pivot");
		arrowPosition = transform.FindChild("Art").transform.FindChild("Pivot").transform.FindChild("ArrowPosition").transform.position;
	}
}
