using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {
	public bool openmap = false;
	public Vector2 mapsize;
	// Use this for initialization
	void Start () {
		mapsize.x = 8;
		mapsize.y = 7;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.camera.pixelRect =new Rect(0,0,Screen.width / mapsize.x,Screen.height / mapsize.y);
	if(Input.GetKey(KeyCode.Tab)){
			openmap = true;
		}
		else{
			openmap = false;
		}
		if(openmap){
			if(mapsize.x > 1.5f){
				mapsize.x -= 0.1f;
			}
			if(mapsize.y > 1.5f){
				mapsize.y -= 0.1f;
			}
		}
		else{
			if(mapsize.x < 8){
				mapsize.x += 0.1f;
			}
			if(mapsize.y < 7){
				mapsize.y += 0.1f;
			}
		}
	}

}
