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

	private int pathNum = 0;
	private FadeScript fade;

	private bool findnewPath;
	private bool pathFound;

	private float startTime;
	private Quaternion rotation;

	public bool stopMoving = false;

	void Start() {
		stopMoving = false;
		fade = GetComponent<FadeScript>();
		GetData();
	}

	void GetData() {
		pathNum++;

		if(pathNum == 4)
			pathNum = 0;

		currentPath = paths[pathNum];
		PathData path = currentPath.GetComponent<PathData>();
		transform.rotation = path.pointA.transform.rotation;

		duration = path.duration;
		startTime = Time.time;

		startPoint = path.pointA.transform.position;
		endPoint = path.pointB.transform.position;
		startRotation = path.pointA.transform.rotation;
		endRotation = path.pointB.transform.rotation;
		transform.rotation = path.pointA.transform.rotation;
		transform.position = path.pointA.transform.position;

		pathFound = true;
		fade.fadingOut = false;
		startPoint = transform.position;

		StopCoroutine("Delay");
	}

	IEnumerator Delay() {
		pathFound = false;

		yield return new WaitForSeconds(0.4f);

		GetData();
		findnewPath = false;
	}

	void Update() {
		if(!stopMoving) {
			if(findnewPath && fade.alphaFadeValue >= 0.95f) {
				fade.alphaFadeValue = 1;
				StartCoroutine("Delay");
			}

			if((Time.time - startTime) >= (duration - 1.0f)) {
				fade.fadingOut = true;
				findnewPath = true;
			}
			
			if(pathFound) {
				transform.position = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime) / duration);
				transform.rotation = Quaternion.Lerp(startRotation,endRotation, (Time.time - startTime) / duration);
			}
		}
	}
}
