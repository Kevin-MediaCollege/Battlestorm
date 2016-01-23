using UnityEngine;
using System.Collections;

public class CameraMenu:MonoBehaviour {

	public Vector3 startPoint; // Starting Point of Camera.

	public Vector3 endPoint; // End Point of Camera.
	
	public Quaternion startRotation; // Start Rotation of Camera.

	public Quaternion endRotation; // End Rotation of Camera.

	public float duration = 5.0f; // Duration of CameraMovement.

	public GameObject currentPath; // Current Path being done.

	public GameObject[] paths; // List of Paths.

	private int pathnum = 0; // Number of current Path.

	private FadeScript fade; // Fade animation.

	private bool findnewPath; // Check if a new path needs to be found.

	private bool pathfound; // Check if a path has been found.

	private float startTime; // Starting time.

	private Quaternion rotation; // Rotation.

	public bool stopMoving = false; // Check to stop moving the Camera.

	void Start() {
		stopMoving = false;
		fade = GetComponent<FadeScript>();
		GetData();
	}

	void GetData() {
		//Get Data from a Path.

		pathnum++;

		if(pathnum == 4)
			pathnum = 0;

		currentPath = paths[pathnum];
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
		pathfound = true;
		fade.fading = false;
		startPoint = transform.position;
		StopCoroutine("Delay");
	}

	IEnumerator Delay() {
		pathfound = false;
		yield return new WaitForSeconds(0.4f);
		GetData();
		findnewPath = false;
	}

	void Update() {
		if(!stopMoving){
			if(findnewPath && fade.alphaFadeValue >= 0.95f) {
				fade.alphaFadeValue = 1;
				StartCoroutine("Delay");
			}

			if((Time.time - startTime) >= (duration - 1.0f)) {
				fade.fading = true;
				findnewPath = true;
			}

			if(pathfound) {
				transform.position = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime) / duration);
				transform.rotation = Quaternion.Lerp(startRotation,endRotation, (Time.time - startTime) / duration);
			}

		}

	}

}
