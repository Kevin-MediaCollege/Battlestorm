using UnityEngine;
using System.Collections;

public class BaseHealthBar:MonoBehaviour {
	public Texture healthBar;
	public Texture bar;

	private Vector3 position;

	void FixedUpdate() {
		position = Camera.main.WorldToScreenPoint(transform.position);
	}
	
	void OnGUI() {
		if(transform.renderer.isVisible) {
			GUI.DrawTexture(new Rect(position.x, Screen.height + -position.y, (healthBar.width / 3), healthBar.height / 3), bar);
			GUI.DrawTexture(new Rect(position.x, Screen.height + -position.y, healthBar.width / 3, healthBar.height / 3), healthBar);
		}
	}
}
