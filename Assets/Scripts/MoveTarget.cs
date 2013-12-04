using UnityEngine;
using System.Collections;

public class MoveTarget:MonoBehaviour {
	void Update () {
		Vector3 pos = transform.position;

		if(Input.GetKeyDown(KeyCode.S)) {
			pos.z -= 4;
		} else if(Input.GetKeyDown(KeyCode.W)) {
			pos.z += 4;
		}

		if(Input.GetKeyDown(KeyCode.A)) {
			pos.x -= 4;
		} else if(Input.GetKeyDown(KeyCode.D)) {
			pos.x += 4;
		}



		if(pos != transform.position) {
			transform.position = pos;

			GameObject.Find("Worker").GetComponent<PathFollower>().SearchPath();
		}
	}
}
