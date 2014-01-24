using UnityEngine;
using System.Collections;

public class InputHandler:MonoBehaviour {
	/** Left key */
	public static KeyCode left = KeyCode.A;

	/** Right key */
	public static KeyCode right = KeyCode.D;

	/** Forward key */
	public static KeyCode forward = KeyCode.W;

	/** Backward key */
	public static KeyCode back = KeyCode.S;

	/** Up key */
	public static KeyCode up = KeyCode.E;

	/** Down key */
	public static KeyCode down = KeyCode.Q;

	/** Minimap key */
	public static KeyCode minimap = KeyCode.Tab;

	/** Building Manager key */
	public static KeyCode buildingManager = KeyCode.Space;

	/** Player set FOV */
	public static float FOV = 70;
}
