using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using Holoville.HOTween;

public class ColorCorrectionTransition : MonoBehaviour {

    ColorCorrectionCurves colorCorrection;

	void Awake () {
        colorCorrection = GetComponent<ColorCorrectionCurves>();
        HOTween.To(colorCorrection, 6, "saturation", 1);
	}

}
