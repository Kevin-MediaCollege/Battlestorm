using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using Holoville.HOTween;

/// <summary>
/// The transition from gray to color when entering the game map.
/// </summary>
public class ColorCorrectionTransition : MonoBehaviour {

    ColorCorrectionCurves colorCorrection;

	void Awake () {

        colorCorrection = GetComponent<ColorCorrectionCurves>();

        HOTween.To(colorCorrection, 6, "saturation", 1);

        #if UNITY_WEBGL
        colorCorrection.saturation = 1;
        #endif

    }

}
