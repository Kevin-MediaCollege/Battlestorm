using UnityEngine;
using System.Collections;

public class BuildingManager:MonoBehaviour {
	//Also called Plot or Platform.

	public GameObject platform; // The Art and collider of the BuildingManager.

	public bool isUnlocked; // Check whether its been unlocked;

	public ParticleSystem[] particle; // The visual effect of the Plot.

	private GameObject building; // The building the BuildingManager created.

	private GameObject position; // Spawn position of the Building.

	public GameObject instantiateparticle; // Visual Effect created upon making a Building.

	public BuildManagerGUI BMGUI; //Reference to the BuildManagerGUI.

	public EBuildingType currentType; // Which type the current Building is.

	void Start() {
		// Getting Reference of BuildManagerGUI.
		BMGUI = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BuildManagerGUI>();

		// Deactivates particle if it isnt unlocked.
		if(isUnlocked){
			SetParticle(true);
		}else{
			SetParticle(false);
		}

		//Get reference to the SpawnPosition of the Building.
		position = transform.FindChild("BuildingPosition").gameObject;

	}

	public void CreateBuilding(EBuildingType type) {
		//Creates a building on the Platform.

		for(int i = 0; i < particle.Length; i++){
			SetParticle(false);
		}

		Instantiate(instantiateparticle, transform.position, instantiateparticle.transform.rotation);
		StartCoroutine(WaitForParticle(type));

	}

	IEnumerator WaitForParticle(EBuildingType type){
		//Waiting time for particle Animation.

		platform.collider.enabled = false;
		currentType = type;
		yield return new WaitForSeconds(2.5f);

		//Creates The Building

		Instantiate(Resources.Load("Prefabs/SoundPrefabs/BuildingCreateSound"), position.transform.position, position.transform.rotation);
		platform.renderer.enabled = false;
		platform.GetComponent<BoxCollider>().enabled = false;
		
		building = Instantiate(Resources.Load("Prefabs/Buildings/" + type), position.transform.position, position.transform.rotation) as GameObject;
		building.transform.parent = this.transform;
		
		if(type == EBuildingType.TowerNormal || type == EBuildingType.TowerIce || type == EBuildingType.TowerFire){
			building.transform.name = "Tower";
		}

	}
	
	public void SetParticle(bool state){
		//Sets state of the building Particles.

		for(int i = 0; i < particle.Length; i++){
			particle[i].renderer.enabled = state;
		}

	}

	public void CreatePoofParticle(){
		Instantiate(Resources.Load("Particles/PlotPoof"), transform.position,Quaternion.identity);
	}

	public void DestroyBuilding(Building building) {
		//Removes the Building.

		switch(currentType){
			case EBuildingType.TowerNormal:
				BMGUI.removeCount(1);
			break;
			case EBuildingType.LumberMill:
				BMGUI.removeCount(2);
			break;
			case EBuildingType.Mine:
				BMGUI.removeCount(3);
			break;
			case EBuildingType.TowerFire:
				BMGUI.removeCount(4);
			break;
			case EBuildingType.TowerIce:
				BMGUI.removeCount(5);
			break;
		}

		SetParticle(true);
		platform.renderer.enabled = true;
		platform.GetComponent<BoxCollider>().enabled = true;
		CreatePoofParticle();
		Destroy(building.gameObject);

	}

}
