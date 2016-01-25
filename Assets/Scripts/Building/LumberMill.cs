using UnityEngine;
using System.Collections;

public class LumberMill:Building {

    private ResourceText woodResourcePopup;

    new void Start() {
		base.Start();

		UpdateArt();
		StartCoroutine("Tick");

	}
	
    void Awake () {
        //Create the resource Object
        GameObject woodObject = Instantiate(Resources.Load("Prefabs/Text/WoodResourceText"), transform.position, Quaternion.identity) as GameObject;
      
        woodResourcePopup = woodObject.GetComponent<ResourceText>();
        woodResourcePopup.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        
        woodResourcePopup.transform.parent = transform;

    }

    protected override IEnumerator Tick() {
		//Gives the player Resources and Instantiates a Resource Popup.

		while(true) {

			yield return new WaitForSeconds(tickDelay);

			PlayerData.Instance.woodAmount += stats.resourcesPerTick[currentLevel - 1];

            woodResourcePopup.TweenResourceText(stats.resourcesPerTick[currentLevel - 1]);

		}

	}

}