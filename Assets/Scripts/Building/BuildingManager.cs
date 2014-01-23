using UnityEngine;
using System.Collections;

public class BuildingManager:MonoBehaviour {
	public GameObject platform;

	public bool isUnlocked;

	public ParticleSystem[] particle;
	private GameObject building;
	private GameObject position;
	public GameObject instantiateparticle;
	void Start() {
		if(isUnlocked){
			SetParticle(true);
		}else{
			SetParticle(false);
		}
		position = transform.FindChild("BuildingPosition").gameObject;
	}

	public void CreateBuilding(EBuildingType type) {
		for(int i = 0; i < particle.Length; i++){
			SetParticle(false);
		}

		Instantiate(instantiateparticle, transform.position, instantiateparticle.transform.rotation);
		StartCoroutine(waitforParticle(type));
	}

	IEnumerator waitforParticle(EBuildingType type){
		platform.collider.enabled = false;

		yield return new WaitForSeconds(3f);

		Instantiate(Resources.Load("Prefabs/SoundPrefabs/BuildingCreateSound"), position.transform.position, position.transform.rotation);
		platform.renderer.enabled = false;
		platform.GetComponent<BoxCollider>().enabled = false;
		
		building = Instantiate(Resources.Load("Prefabs/Buildings/" + type), position.transform.position, position.transform.rotation) as GameObject;
		building.transform.parent = this.transform;
		
		if(type == EBuildingType.TowerNormal || type == EBuildingType.TowerIce || type == EBuildingType.TowerFire)
			building.transform.name = "Tower";
	}

	public void SetParticle(bool state){
		for(int i = 0; i < particle.Length; i++){
			particle[i].renderer.enabled = state;
		}
	}

	public void DestroyBuilding(Building building) {
		SetParticle(true);
		platform.renderer.enabled = true;
		platform.GetComponent<BoxCollider>().enabled = true;

		Destroy(building.gameObject);
	}
}
