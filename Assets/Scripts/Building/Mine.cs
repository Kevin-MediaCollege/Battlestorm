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

			GameObject popupText = Instantiate(Resources.Load("Prefabs/Text/StoneResourceText"), transform.position, Quaternion.identity) as GameObject;
			TextMesh textPopup = popupText.GetComponent<TextMesh>();

			PlayerData.Instance.stoneAmount += stats.resourcesPerTick[currentLevel - 1];

			textPopup.text = "" + stats.resourcesPerTick[currentLevel];
			textPopup.color = Color.gray;
			textPopup.transform.parent = this.transform;
		}
	}
}
