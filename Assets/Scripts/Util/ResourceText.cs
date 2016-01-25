using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class ResourceText : MonoBehaviour {

	void Awake() {

		transform.localEulerAngles = new Vector3(0, 0, 0);

	}
    
    public void TweenResourceText () {

        HOTween.To(transform, 4, "localposition", new Vector3(0, 5, 0));

    }
}
