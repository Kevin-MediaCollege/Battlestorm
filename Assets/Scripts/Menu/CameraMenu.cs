using UnityEngine;
using System.Collections;

public class CameraMenu:MonoBehaviour {
	public Vector3 startPoint;
	public Vector3 endPoint;
	
	public Quaternion startRotation;
	public Quaternion endRotation;

	public float duration = 5.0f;

	public GameObject currentPath;
	public GameObject[] paths;

	private int pathnum = 0;
	private FadeScript fade;

	private bool findnewPath;
	private bool pathfound;

	private float startTime;
	private Quaternion rotation;

	void Start() {
		fade = GetComponent<FadeScript>();
		GetData();
	}

	void GetData() {
		pathnum++;

		if(pathnum == 4)
			pathnum = 0;

		startPoint = transform.position;
		endPoint = transform.position;
		currentPath = paths[pathnum];
		PathData path = paths[pathnum].GetComponent<PathData>();
		duration = path.duration;
		startTime = Time.time;
		startPoint = path.pointA.transform.position;
		endPoint = path.pointB.transform.position;
		startRotation = path.pointA.transform.rotation;
		endRotation = path.pointB.transform.rotation;
		pathfound = true;
		fade.fadingOut = false;

		StopCoroutine("Delay");
	}

	IEnumerator Delay() {
		pathfound = false;
		yield return new WaitForSeconds(0.4f);
		GetData();
		findnewPath = false;
	}

	void Update() {
		if(findnewPath && fade.alphaFadeValue >= 0.95f) {
			fade.alphaFadeValue = 1;
			StartCoroutine("Delay");
		}

		if((Time.time - startTime) >= (duration - 1.0f)) {
			fade.fadingOut = true;
			findnewPath = true;
		}

		if(pathfound) {
			transform.position = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime) / duration);
			transform.rotation = Quaternion.Lerp(startRotation,endRotation, Time.time * 0.04f);
		}
	}
}
