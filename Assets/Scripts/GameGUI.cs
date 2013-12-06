using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
///Reminder on What to do:
// Use a raycast to check if i clicked a building
// check which type it is
// render gui around mouse

	public bool chooseemptyplot;
	public float clickedmousex;
	public float clickedmousey;

	private SpawnBuilding plotspawn;

	void FixedUpdate(){
		if(Input.GetMouseButtonDown(1)){
			chooseemptyplot = false;
		}
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,100))
			{
				if(!chooseemptyplot){
					if(hit.transform.gameObject.tag == "EmptyPlot"){
						chooseemptyplot = true;
						clickedmousex = Input.mousePosition.x;
						clickedmousey = Input.mousePosition.y;
						plotspawn = hit.transform.gameObject.GetComponent<SpawnBuilding>();
						return;
					}
				}
			}
		}
	}

	void OnGUI(){
	if(chooseemptyplot){
			if (GUI.Button(new Rect(clickedmousex - 25,Screen.height + -clickedmousey - 100,50,50),"Tower")){
				createBuilding(3);
			}
			if (GUI.Button(new Rect(clickedmousex - 100,Screen.height + -clickedmousey + 25,50,50),"Lumber")){
				createBuilding(1);
			}
			if (GUI.Button(new Rect(clickedmousex + 50,Screen.height + -clickedmousey + 25,50,50),"Mine")){
				createBuilding(2);
			}
		}
	}
	void createBuilding(int type){
		plotspawn.createBuilding(type);
		plotspawn.tag = "Untagged";
		chooseemptyplot = false;
	}
}
