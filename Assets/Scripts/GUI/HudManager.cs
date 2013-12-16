using UnityEngine;
using System.Collections;

public class HudManager:MonoBehaviour {
	public Texture texture;
	public Vector2 position;

	private int textureX;

	void OnGUI() {
		textureX = texture.width / 2;

		GUI.DrawTexture(new Rect(position.x * textureX, position.y * Screen.height, texture.width, texture.height), texture);

		Debug.Log(position.x * textureX);
	}
}
