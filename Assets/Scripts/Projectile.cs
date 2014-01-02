using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public Transform target;
	private Vector3 speed = new Vector3(3,0,0);
	public AudioClip hitSound;

	public float damage;
	public Enemy targetScript;
	// Use this for initialization
	void Start () {
	//	this.transform.parent = GameObject.FindGameObjectWithTag("ProjectileObject").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null){
			Debug.Log(target.position);
		//Quaternion.LookRotation(target.position);
		transform.LookAt(target.transform.position);
			transform.Translate(Vector3.forward);
			renderer.enabled = true;
		}
		else{
			renderer.enabled = false;
			Destroy(gameObject);
		}
	}
	void OnTriggerEnter(Collider coll){
		Instantiate(Resources.Load("Particles/Arrowhit"),transform.position,transform.rotation);
	}
	void OnTriggerStay(Collider coll){
		targetScript.Damage(damage);
		Destroy(gameObject);
	}
	void OnDestroy(){
		audio.PlayOneShot(hitSound);
	}
}
