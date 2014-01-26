using UnityEngine;
using System.Collections;

public class Mine:Building {

	new void Start () {
		base.Start();

		UpdateArt();
		StartCoroutine("Tick");

	}
	
	protected override IEnumerator Tick() {
		//Gives the player Resources and Instantiates a Resource Popup.

		while(true) {

			yield return new WaitForSeconds(tickDelay);

			PlayerData.Instance.stoneAmount += stats.resourcesPerTick[currentLevel - 1];

			Vector3 position = transform.position;
			position.y += 5;

			GameObject popupText = Instantiate(Resources.Load("Prefabs/Text/StoneResourceText"), position, Quaternion.identity) as GameObject;

			TextMesh textPopup = popupText.GetComponent<TextMesh>();
			textPopup.text = stats.resourcesPerTick[currentLevel - 1].ToString();
			textPopup.color = Color.gray;
			textPopup.transform.parent = this.transform;

		}

	}

}
