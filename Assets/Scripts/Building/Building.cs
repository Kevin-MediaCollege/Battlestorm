using UnityEngine;
using System.Collections;

public class Building:MonoBehaviour {
	public string prefabPath = "Prefabs/Buildings/";

	[HideInInspector]
	public BuildingStats stats;

	public bool interactable;

	public int tickDelay;
	public int currentLevel;

	protected GameObject art;

	protected void Start() {
		stats = GetComponent<BuildingStats>();
	}
	
	public virtual void SwitchLevel(int newLevel) {
		if(newLevel > stats.levels)
			return;
		
		currentLevel = newLevel;
		
		UpdateArt();
	}

	protected virtual IEnumerator Tick() { return null; }

	protected void UpdateArt() {
		if(art != null)
			Destroy(art);

		art = Instantiate(Resources.Load(prefabPath + " " + (int)currentLevel), transform.position, transform.rotation) as GameObject;

		art.transform.parent = this.transform;
		art.transform.name = "Art";
	}
}
