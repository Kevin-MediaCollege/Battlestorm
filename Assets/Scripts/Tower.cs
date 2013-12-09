using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
	//
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("MainCamera");
	}
	// Update is called once per frame
	void Update () {
		transform.LookAt(player.transform.position);
		StartCoroutine("ShootObject");
	}
	IEnumerator ShootObject(){
		yield return new WaitForSeconds(4);

		StartCoroutine("ShootObject");
	}
}
