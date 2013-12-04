using UnityEngine;
using System.Collections;

public class MouseHandler : MonoBehaviour {
	private SpawnBuilding plot;
	private LumberMill mill;
	private Mine mine;
	private PlayerData pData;

	public int selection;
	// Use this for initialization
	void Start () {
		selection = 1;

		pData = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerData>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Q)){
			selection = 1;
		}
		if (Input.GetKey(KeyCode.W)){
			selection = 2;
		}
		if( Input.GetMouseButtonDown(0) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			
			if( Physics.Raycast( ray, out hit, 100 ) )
			{
				if(hit.transform.gameObject.tag == "EmptyPlot"){
					Debug.Log("ASD");
					plot = hit.transform.gameObject.GetComponent<SpawnBuilding>();
					plot.createBuilding(selection);
					plot.tag = "Untagged";
					return;
				}
				plot = null;
				if(hit.transform.gameObject.tag == "LumberMill"){
					mill = hit.transform.GetComponent<LumberMill>();
					if(pData.woodAmount >= mill.woodcostfornextlevel){
						if(pData.stoneAmount >= mill.stonecostfornextlevel){
							pData.stoneAmount -= mill.stonecostfornextlevel;
							pData.woodAmount -= mill.woodcostfornextlevel;
							mill.SwitchLevel(mill.currentlevel);
							return;
						}
					}
				}
				mill = null;
				if(hit.transform.gameObject.tag == "Mine"){
					mine = hit.transform.GetComponent<Mine>();
					if(pData.woodAmount >= mine.woodcostfornextlevel){
						if(pData.stoneAmount >= mine.stonecostfornextlevel){
							pData.stoneAmount -= mine.stonecostfornextlevel;
							pData.woodAmount -= mine.woodcostfornextlevel;
							mine.SwitchLevel(mine.currentlevel);
							return;
						}
					}
				}
				mine = null;
			}
		}
	}
}
