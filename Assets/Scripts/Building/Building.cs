using UnityEngine;
using System.Collections;

public class Building:MonoBehaviour {

	public string prefabPath = "Prefabs/Buildings/"; // Default path to search the Art in.

	[HideInInspector]
	public BuildingStats stats; // Stats of the building

	public bool interactable; // Check whether its interactable.

	public int tickDelay; // amount of time inbetween Ticks.

	public int currentLevel; // Current level of the building.

	protected GameObject art; // The Gameobject that has the Art of the Building.

	protected void Start() {
		//Get reference to the BuildingStats.

		stats = GetComponent<BuildingStats>();

	}
	
	public virtual void SwitchLevel(int newLevel) {
		//Replaces the art of the Building.

		if(newLevel > stats.levels){
			return;
		}
		
		currentLevel = newLevel;
		
		UpdateArt();

	}

	protected virtual IEnumerator Tick() { return null; }

	protected void UpdateArt() {
		//Removes previous Art GameObject. Replaces with new Art.

		if(art != null){
			Destroy(art);
		}

		art = Instantiate(Resources.Load(prefabPath + " " + (int)currentLevel), transform.position, transform.rotation) as GameObject;
		art.transform.parent = this.transform;
		art.transform.name = "Art";

	}

}
