using UnityEngine;
using System.Collections;

public class ResourceText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.localEulerAngles = new Vector3(0, 0, 0);
		Destroy(gameObject,1.5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.LookAt(Camera.main.transform);
		transform.Translate(new Vector3(0,0.02f,0));
	}
}
