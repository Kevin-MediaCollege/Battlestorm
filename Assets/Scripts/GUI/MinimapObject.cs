using UnityEngine;
using System.Collections;

public class MinimapObject : MonoBehaviour {
	public GameObject pointtarget;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = new Vector3(pointtarget.transform.position.x,50,pointtarget.transform.position.z);
		this.transform.position = position;

	}
}
