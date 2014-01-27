using UnityEngine;
using System.Collections;

public class LumberMill:Building {

	new void Start() {
		base.Start();

		UpdateArt();
		StartCoroutine("Tick");

	}
	
	protected override IEnumerator Tick() {
		//Gives the player Resources and Instantiates a Resource Popup.

		while(true) {

			yield return new WaitForSeconds(tickDelay);

			PlayerData.Instance.woodAmount += stats.resourcesPerTick[currentLevel - 1];

			Vector3 position = transform.position;
			position.y += 5;
			
			GameObject popupText = Instantiate(Resources.Load("Prefabs/Text/WoodResourceText"), position, Quaternion.identity) as GameObject;

			TextMesh textPopup = popupText.GetComponent<TextMesh>();
			textPopup.text = stats.resourcesPerTick[currentLevel - 1].ToString();
			textPopup.color = new Color(0.6f, 0.2f, 0);
			textPopup.transform.parent = this.transform;

		}

	}

}