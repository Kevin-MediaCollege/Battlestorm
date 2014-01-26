using UnityEngine;
using System.Collections;

public class Base:MonoBehaviour {
	/// Your Main Capitol. The building you're protecting from enemies.
	/// Give's a fixed amount of resources per tick.

	public float tickDelay; //Time between Tick

	public int woodPerTick; // Amount of wood per Tick.

	public int stonePerTick; // Amount of stone per Tick.

	void Start() {
		StartCoroutine("Tick");
	}
	IEnumerator Tick() {
		//Adds Resource Points and Creates the Resource popup Text.

		while(true) {

			yield return new WaitForSeconds(tickDelay - 0.6f);

			PlayerData.Instance.woodAmount += woodPerTick;
			PlayerData.Instance.stoneAmount += stonePerTick;

			Vector3 position = transform.position;
			position.y += 12;

			TextMesh woodPopup = (Instantiate(Resources.Load("Prefabs/Text/WoodResourceText"), position, Quaternion.identity) as GameObject).GetComponent<TextMesh>();

			woodPopup.text = woodPerTick.ToString();
			woodPopup.color = new Color(0.6f, 0.2f, 0);
			woodPopup.transform.parent = this.transform;

			yield return new WaitForSeconds(0.6f);

			TextMesh stonePopup = (Instantiate(Resources.Load("Prefabs/Text/StoneResourceText"), position, Quaternion.identity) as GameObject).GetComponent<TextMesh>();

			stonePopup.text = stonePerTick.ToString();
			stonePopup.color = Color.gray;
			stonePopup.transform.parent = this.transform;

		}

	}

}