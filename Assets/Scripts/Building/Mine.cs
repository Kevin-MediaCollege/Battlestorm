using UnityEngine;
using System.Collections;

public class Mine:Building {
	new void Start () {
		base.Start();

		UpdateArt();
		StartCoroutine("Tick");
	}
	
	protected override IEnumerator Tick() {
		while(true) {
			yield return new WaitForSeconds(tickDelay);
			
			Vector3 position = transform.position;
			position.y += 5;

			GameObject popupText = Instantiate(Resources.Load("Prefabs/Text/StoneResourceText"), position, Quaternion.identity) as GameObject;
			TextMesh textPopup = popupText.GetComponent<TextMesh>();

			PlayerData.Instance.stoneAmount += stats.resourcesPerTick[currentLevel - 1];

			textPopup.text = stats.resourcesPerTick[currentLevel - 1].ToString();
			textPopup.color = Color.gray;
			textPopup.transform.parent = this.transform;
		}
	}
}
