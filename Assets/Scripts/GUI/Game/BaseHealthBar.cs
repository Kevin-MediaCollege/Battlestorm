using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BaseHealthBar:MonoBehaviour {

    public Image fillBar;

	public PlayerData playerHealth;

	void FixedUpdate() {
        fillBar.fillAmount = playerHealth.health / 10.0f;
	
	}
	
	
}
