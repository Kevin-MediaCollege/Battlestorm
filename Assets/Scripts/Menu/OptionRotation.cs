using UnityEngine;
using System.Collections;

public class OptionRotation : MonoBehaviour {
	public float duration = 100.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.down, 360.0f * Mathf.Deg2Rad / duration * Time.deltaTime);
	}
}
