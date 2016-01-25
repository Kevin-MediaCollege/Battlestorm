using UnityEngine;
using System.Collections;

/// <summary>
/// Your Main Capitol. The building you're protecting from enemies.
///  Gives a fixed amount of resources per tick.
/// </summary>
public class Base:MonoBehaviour {

	public float tickDelay; //Time between Tick

	public int woodPerTick; // Amount of wood per Tick.

	public int stonePerTick; // Amount of stone per Tick.

    private ResourceText woodResourcePopup;
    private ResourceText stoneResourcePopup;

    public Transform resourceSpawnPosition;

    void Awake () {
        //Create the resource Objects
        GameObject woodObject = Instantiate(Resources.Load("Prefabs/Text/WoodResourceText"), transform.position, Quaternion.identity) as GameObject;
        GameObject stoneObject = Instantiate(Resources.Load("Prefabs/Text/StoneResourceText"), transform.position, Quaternion.identity) as GameObject;

        woodResourcePopup = woodObject.GetComponent<ResourceText>();
        woodResourcePopup.transform.position = resourceSpawnPosition.position;
        woodResourcePopup.transform.parent = resourceSpawnPosition;

        stoneResourcePopup = stoneObject.GetComponent<ResourceText>();
        stoneResourcePopup.transform.position = resourceSpawnPosition.position;
        stoneResourcePopup.transform.parent = resourceSpawnPosition;

    }

	void Start() {

		StartCoroutine("Tick");

	}

	IEnumerator Tick() {
		//Adds Resource Points and Creates the Resource popup Text.

		while(true) {

			yield return new WaitForSeconds(tickDelay - 0.6f);

			PlayerData.Instance.woodAmount += woodPerTick;
			PlayerData.Instance.stoneAmount += stonePerTick;

            woodResourcePopup.TweenResourceText(woodPerTick);

			yield return new WaitForSeconds(0.6f);

            stoneResourcePopup.TweenResourceText(stonePerTick);

		}

	}

}