using UnityEngine;
using System.Collections;

public class Base:MonoBehaviour {
	public float tickDelay;

	public int woodPerTick;
	public int stonePerTick;

	void Start() {
		StartCoroutine("Tick");
	}

	IEnumerator Tick() {
		while(true) {
			yield return new WaitForSeconds(tickDelay - 0.6f);
			
			Vector3 position = transform.position;
			position.y += 12;

			TextMesh woodPopup = (Instantiate(Resources.Load("Prefabs/Text/WoodResourceText"), position, Quaternion.identity) as GameObject).GetComponent<TextMesh>();

			PlayerData.Instance.woodAmount += woodPerTick;
			PlayerData.Instance.stoneAmount += stonePerTick;

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
