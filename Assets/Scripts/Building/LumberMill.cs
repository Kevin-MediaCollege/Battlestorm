using UnityEngine;
using System.Collections;

public class LumberMill:Building {
	new void Start() {
		base.Start();

		UpdateArt();
		StartCoroutine("Tick");
	}
	
	protected override IEnumerator Tick() {
		while(true) {
			yield return new WaitForSeconds(tickDelay);
			
			Vector3 position = transform.position;
			position.y += 5;
			
			GameObject popupText = Instantiate(Resources.Load("Prefabs/Text/WoodResourceText"), position, Quaternion.identity) as GameObject;
			TextMesh textPopup = popupText.GetComponent<TextMesh>();

			PlayerData.Instance.woodAmount += stats.resourcesPerTick[currentLevel - 1];

			textPopup.text = stats.resourcesPerTick[currentLevel].ToString();
			textPopup.color = new Color(0.6f, 0.2f, 0);
			textPopup.transform.parent = this.transform;
		}
	}
}
