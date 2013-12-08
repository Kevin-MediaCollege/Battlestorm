using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
///Reminder on What to do:
// Use a raycast to check if i clicked a building
// check which type it is
// render gui around mouse
	private PlayerData pData;

	private bool chooseemptyplot;
	private bool choosetower;
	private bool choosemine;
	private bool chooselumbermill;

	private bool doneaction;

	private SpawnBuilding plotspawn;
	private LumberMill lumbermill;
	private Mine mine;
	private Vector3 guiposition;
	private Transform target;

	public Texture stone;
	public Texture wood;

	//Variable's for building panel
	private string buildingname;
	private string resourcename;

	public GUIStyle buystyle;
	public GUIStyle sellstyle;
	void Start(){
		pData = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerData>();
	}
	void FixedUpdate(){
		Debug.Log(chooseemptyplot);
		if(target != null){
			guiposition = Camera.main.WorldToScreenPoint(target.position);
		}
		if(Input.GetMouseButtonDown(1)){
		}
		if(Input.GetMouseButton(0)){
			doneaction = true;
		}
		else{
			doneaction = false;
		}
		if(Input.GetMouseButton(1)){
			chooseemptyplot = false;
			choosetower = false;
			chooselumbermill = false;
			choosemine = false;
		}
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,100))
			{
				if(!chooseemptyplot && !chooselumbermill && !choosetower && !choosemine){
					if(hit.transform.gameObject.tag == "EmptyPlot"){
						doneaction = true;
						StartCoroutine("SlightDelay");
						target = hit.transform;
						guiposition = Camera.main.WorldToScreenPoint(target.position);
						plotspawn = hit.transform.gameObject.GetComponent<SpawnBuilding>();
						return;
					}
				
					if(hit.transform.gameObject.tag == "LumberMill"){
						doneaction = true;
						StartCoroutine("SlightDelayL");
						target = hit.transform;
						guiposition = Camera.main.WorldToScreenPoint(target.position);
						lumbermill = hit.transform.gameObject.GetComponent<LumberMill>();
						resourcename = "Wood";
						buildingname = "Lumber Mill";
						return;
					}

					if(hit.transform.gameObject.tag == "Mine"){
						doneaction = true;
						StartCoroutine("SlightDelayM");
						target = hit.transform;
						guiposition = Camera.main.WorldToScreenPoint(target.position);
						mine = hit.transform.gameObject.GetComponent<Mine>();
						resourcename = "Stone";
						buildingname = "Mine";
						return;
					}
				}
			}
		}
		}
	IEnumerator SlightDelay(){
		yield return new WaitForSeconds(0.02f);
		chooseemptyplot = true;
	}
	IEnumerator SlightDelayL(){
		yield return new WaitForSeconds(0.02f);
		chooselumbermill = true;
	}
	IEnumerator SlightDelayM(){
		yield return new WaitForSeconds(0.02f);
		choosemine = true;
	}
	void OnGUI(){
	if(chooseemptyplot && !chooselumbermill && !choosemine && !choosetower){
			if (GUI.Button(new Rect(guiposition.x - 25,Screen.height + -guiposition.y - 100,50,50),"Tower")){
				if(!doneaction){
				createBuilding(1);
				}
			}
			if (GUI.Button(new Rect(guiposition.x - 100,Screen.height + -guiposition.y + 25,50,50),"Lumber")){
				if(!doneaction){
				createBuilding(2);
				}
			}
			if (GUI.Button(new Rect(guiposition.x + 50,Screen.height + -guiposition.y + 25,50,50),"Mine")){
				if(!doneaction){
				createBuilding(3);
				}
			}
		}

		//Box for the building stats/ upgrade type
		if(choosemine || choosetower || chooselumbermill){
			GUI.BeginGroup(new Rect(guiposition.x - 100,Screen.height + -guiposition.y - 150, 200, 150));
			GUI.Box(new Rect(0, 0, 200, 150),buildingname);
			GUI.Label(new Rect(5,0,200,20),"" + resourcename+ ": ");
			GUI.Box(new Rect(140, 30, 60, 120),"");
			if(chooselumbermill){
				LumberMillPanel();
			}
			if(choosemine){
				MinePanel();
			}
		GUI.EndGroup();
		}
	}
	void MinePanel(){
		GUI.Label(new Rect(85,25,200,20),"LVL: " + mine.currentlevel);
		//Upgrade Details
		if(mine.woodcostfornextlevel >= 999999){} else{
			GUI.Box(new Rect(0, 30, 60, 120),"");
			GUI.DrawTexture(new Rect(0,50,20,20),wood);
			GUI.DrawTexture(new Rect(0,70,20,20),stone);
			GUI.Label(new Rect(25,50,30,20),"-" + mine.woodcostfornextlevel,buystyle);
			GUI.Label(new Rect(25,72,30,20),"-" + mine.stonecostfornextlevel,buystyle);
			GUI.Label(new Rect(18,27,30,20),"Cost");
			if (GUI.Button(new Rect(5,95,50,50),"Upgrade")){
				if(pData.woodAmount >= mine.woodcostfornextlevel){
					if(pData.stoneAmount >= mine.stonecostfornextlevel){
						mine.SwitchLevel(mine.currentlevel);
					}
				}
			}
		}
		//
		//Upgrade Details
		GUI.DrawTexture(new Rect(140,50,20,20),wood);
		GUI.DrawTexture(new Rect(140,70,20,20),stone);
		GUI.Label(new Rect(165,50,30,20),"+" + mine.woodsellprice,sellstyle);
		GUI.Label(new Rect(165,72,30,20),"+" + mine.stonesellprice,sellstyle);
		//
		
		if (GUI.Button(new Rect(145,95,50,50),"Sell")){
			//Selling Logic
		}
	}
	void LumberMillPanel(){
		GUI.Label(new Rect(85,25,200,20),"LVL: " + lumbermill.currentlevel);
		//GUI.Label(new Rect(130,40,200,20),"" + (int)lumbermill.resourcesPerTick);
		//Upgrade Details
		if(lumbermill.woodcostfornextlevel >= 999999){} else{
			GUI.Box(new Rect(0, 30, 60, 120),"");
			GUI.DrawTexture(new Rect(0,50,20,20),wood);
			GUI.DrawTexture(new Rect(0,70,20,20),stone);
			GUI.Label(new Rect(25,50,30,20),"-" + lumbermill.woodcostfornextlevel,buystyle);
			GUI.Label(new Rect(25,72,30,20),"-" + lumbermill.stonecostfornextlevel,buystyle);
			GUI.Label(new Rect(18,27,30,20),"Cost");
			if (GUI.Button(new Rect(5,95,50,50),"Upgrade")){
				if(pData.woodAmount >= lumbermill.woodcostfornextlevel){
					if(pData.stoneAmount >= lumbermill.stonecostfornextlevel){
						lumbermill.SwitchLevel(lumbermill.currentlevel);
					}
				}
			}
		}
		//
		//Upgrade Details
		GUI.DrawTexture(new Rect(140,50,20,20),wood);
		GUI.DrawTexture(new Rect(140,70,20,20),stone);
		GUI.Label(new Rect(165,50,30,20),"+" + lumbermill.woodsellprice,sellstyle);
		GUI.Label(new Rect(165,72,30,20),"+" + lumbermill.stonesellprice,sellstyle);
		//
		
		if (GUI.Button(new Rect(145,95,50,50),"Sell")){
			//Selling Logic
		}
	}
	void createBuilding(int type){
		plotspawn.createBuilding(type);
		plotspawn.tag = "Untagged";
		chooseemptyplot = false;
	}
}
