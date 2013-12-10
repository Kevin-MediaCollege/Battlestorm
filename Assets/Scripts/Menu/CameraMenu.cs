using UnityEngine;
using System.Collections;

public class CameraMenu : MonoBehaviour {
	public GameObject Point1;
	public GameObject Point2;

	public Vector3 startPoint;
	public Vector3 endPoint;

	public float duration = 5.0f;
	private float startTime;
	// Use this for initialization
	void Start () {
		startPoint = Point1.transform.position;
		endPoint = Point2.transform.position;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime) / duration);
	}
}
