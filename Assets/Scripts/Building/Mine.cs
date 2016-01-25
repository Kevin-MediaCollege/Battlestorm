using UnityEngine;
using System.Collections;

public class Mine:Building {

    private ResourceText stoneResourcePopup;

    new void Start () {
		base.Start();

		UpdateArt();
		StartCoroutine("Tick");

	}

    void Awake () {
        //Create the resource Object
        GameObject stoneObject = Instantiate(Resources.Load("Prefabs/Text/StoneResourceText"), transform.position, Quaternion.identity) as GameObject;

        stoneResourcePopup = stoneObject.GetComponent<ResourceText>();
        stoneResourcePopup.transform.position = transform.position + new Vector3(0, 0.5f, 0);

        stoneResourcePopup.transform.parent = transform;

    }

    protected override IEnumerator Tick () {
        //Gives the player Resources and Instantiates a Resource Popup.

        while (true) {

            yield return new WaitForSeconds(tickDelay);

            PlayerData.Instance.woodAmount += stats.resourcesPerTick[currentLevel - 1];

            stoneResourcePopup.TweenResourceText(stats.resourcesPerTick[currentLevel - 1]);

        }

    }

}
