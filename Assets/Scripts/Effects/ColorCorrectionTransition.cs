using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using DG.Tweening;

/// <summary>
/// The transition from gray to color when entering the game map.
/// </summary>
public class ColorCorrectionTransition : MonoBehaviour {

    private ColorCorrectionCurves colorCorrection;

	void Awake () {

        colorCorrection = GetComponent<ColorCorrectionCurves>();

        TweenToValue(1,5);

    }

    /// <summary>
    /// Tweens the saturation to given value.
    /// </summary>
    /// <param name="_value">Saturation value: 1 is default.</param>
    public void TweenToValue (float _value,float _duration) {

        DOTween.To(ChangeValue, 0, _value, _duration);

    }

    private void ChangeValue (float _value) {
        
        colorCorrection.saturation = _value;

    }

    public float GetValue () {
        return colorCorrection.saturation;
    }

}
