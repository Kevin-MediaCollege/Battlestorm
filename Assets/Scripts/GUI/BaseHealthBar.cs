using UnityEngine;
using System.Collections;

public class BaseHealthBar:MonoBehaviour {
	public Texture healthBar;
	public Texture bar;
	public bool visiblecheck;
	private Vector3 position;
	public PlayerData playerHealth;

	void FixedUpdate() {
		position = Camera.main.WorldToScreenPoint(transform.position);
	}
	
	void OnGUI() {
		if(visiblecheck) {
			GUI.DrawTexture(new Rect(position.x - 75, Screen.height + -position.y, (18 * playerHealth.health), healthBar.height / 3), bar);
			GUI.DrawTexture(new Rect(position.x - 75, Screen.height + -position.y, healthBar.width / 3, healthBar.height / 3), healthBar);
		}
	}
	void OnBecameVisible() {
		visiblecheck = true;
	}
	void OnBecameInvisible(){
		visiblecheck = false;
	}
}
