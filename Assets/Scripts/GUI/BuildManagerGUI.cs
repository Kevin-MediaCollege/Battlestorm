using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildManagerGUI : MonoBehaviour {
	public int managerposition;

	public Texture tower;
	public Texture lumbermill;
	public Texture mine;
	public Texture fireTower;
	public Texture iceTower;

	public int towerCount;
	public int lumbermillCount;
	public int mineCount;
	public int fireTowerCount;
	public int iceTowerCount;

	public GUIStyle numberstyle;

	public bool openpanel;
	// Use this for initialization
	void Start () {
		managerposition = 160;
	}
	public void addCount(int type){
		switch(type){
			case 1:
			towerCount++;
			break;
			case 2:
			lumbermillCount++;
			break;
			case 3:
			mineCount++;
			break;
			case 4:
			fireTowerCount++;
			break;
			case 5:
			iceTowerCount++;
			break;
		}
	}
	public void removeCount(int type){
		switch(type){
		case 1:
			towerCount--;
			break;
		case 2:
			lumbermillCount--;
			break;
		case 3:
			mineCount--;
			break;
		case 4:
			fireTowerCount--;
			break;
		case 5:
			iceTowerCount--;
			break;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKey(InputHandler.buildingManager)){
			openpanel = true;
		}else{
			openpanel = false;
		}

		if(openpanel){
			if(managerposition != 0 || managerposition > 0){
				managerposition -= 4;
			}
		}else{
			if(managerposition != 160){
				managerposition += 4;
			}
		}
	}
	void OnGUI(){
		float rx = Screen.width / GameManager.nativeWidth;
		float ry = Screen.height / GameManager.nativeHeight;
		GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 
		GUI.BeginGroup(new Rect(1135 + managerposition,100,150,900),"");
			GUI.BeginGroup(new Rect(0,0,150,75));
				GUI.DrawTexture(new Rect(0,0,tower.width,tower.height),tower);
				GUI.Label(new Rect(15,17,50,50),"" + towerCount,numberstyle);
	        GUI.EndGroup();
			GUI.BeginGroup(new Rect(0,100,150,75));
				GUI.DrawTexture(new Rect(0,0,mine.width,mine.height),mine);
				GUI.Label(new Rect(15,17,50,50),"" + mineCount,numberstyle);
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,200,150,75));
				GUI.DrawTexture(new Rect(0,0,lumbermill.width,lumbermill.height),lumbermill);
				GUI.Label(new Rect(15,17,50,50),"" + lumbermillCount,numberstyle);
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,300,150,75));
				GUI.DrawTexture(new Rect(0,0,fireTower.width,fireTower.height),fireTower);
				GUI.Label(new Rect(15,17,50,50),"" + fireTowerCount,numberstyle);
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(0,400,150,75));
				GUI.DrawTexture(new Rect(0,0,iceTower.width,iceTower.height),iceTower);
				GUI.Label(new Rect(15,17,50,50),"" + iceTowerCount,numberstyle);
			GUI.EndGroup();
		GUI.EndGroup();
	}
}
