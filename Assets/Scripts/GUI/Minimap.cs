using UnityEngine;
using System.Collections;

public class Minimap:MonoBehaviour {
	public bool openMap = false;
	public Vector2 mapSize;

	void Start() {
		mapSize.x = 8;
		mapSize.y = 7;
	}

	void FixedUpdate() {
		gameObject.camera.pixelRect = new Rect (0, 0, Screen.width / mapSize.x, Screen.height / mapSize.y);

		if(Input.GetAxisRaw("Minimap") != 0) {
			openMap = true;
		} else {
			openMap = false;
		}

		if(openMap) {
			if(mapSize.x > 1.5f)
				mapSize.x -= 0.1f;

			if(mapSize.y > 1.5f)
				mapSize.y -= 0.1f;
		} else {
			if(mapSize.x < 8)
				mapSize.x += 0.1f;

			if(mapSize.y < 7)
				mapSize.y += 0.1f;
		}
	}
}
