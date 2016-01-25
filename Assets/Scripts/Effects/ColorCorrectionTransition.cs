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

        TweenToValue(1);

        #if UNITY_WEBGL
        colorCorrection.saturation = 1;
        #endif

    }

    /// <summary>
    /// Tweens the saturation to given value.
    /// </summary>
    /// <param name="_value">Saturation value: 1 is default.</param>
    public void TweenToValue (float _value) {

        HOTween.To(colorCorrection, 6, "saturation", _value);

    }

}
