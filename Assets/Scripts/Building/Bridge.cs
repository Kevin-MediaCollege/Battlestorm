using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour {
	public int bridgeParts;
	public GameObject spawnposition;

	public bool beenMade;
	// Use this for initialization
	void Start () {
		BuildBridge();
	}
	
	public void BuildBridge(){
		for(int i = 0; i < bridgeParts; i++){
			Vector3 spawnpos = spawnposition.transform.position + (Vector3.forward * 0.5f);
			GameObject part = Instantiate(Resources.Load("Prefabs/Buildings/BridgePart"),spawnpos,transform.rotation) as GameObject;
			part.transform.parent = spawnposition.transform;
			part.name = "BridgePart";
			part.transform.position += Vector3.forward * i;
			beenMade = true;
		}
	}
}
