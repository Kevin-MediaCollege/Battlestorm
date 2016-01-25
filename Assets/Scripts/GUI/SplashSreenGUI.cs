using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using UnityEngine.SceneManagement;

/// <summary>
/// The SplashScreen that is shown when starting the application.
/// </summary>
public class SplashSreenGUI : MonoBehaviour {

    #region Variables

    public CanvasGroup mainGroup;
    public CanvasGroup webGLGroup;
    private bool webGLFadeToggle;

    #endregion


    #region Unity Functions

    // Use this for initialization
    void Start () {

        webGLGroup.alpha = 0;
        mainGroup.alpha = 0;

        #if UNITY_WEBGL
        webGLGroup.alpha = 1;
        webGLFadeToggle = true;
        #else
        HOTween.To(mainGroup, 3, "alpha", 1);
        #endif

        StartCoroutine(WaitToFadeOut());

    }

    void Update () {

        #if UNITY_WEBGL

        if (webGLFadeToggle) {

            if(mainGroup.alpha < 1) {

                mainGroup.alpha += 0.015f;

            }

        } else {

            if (mainGroup.alpha > 0) {

                mainGroup.alpha -= 0.015f;

            }

        }

        #endif

    }

    #endregion

    #region Functions

    IEnumerator WaitToFadeOut () {

        yield return new WaitForSeconds(6);

        #if UNITY_WEBGL
        webGLFadeToggle = false;
        #else
        HOTween.To(mainGroup, 2, "alpha", 0);
        #endif

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("GameMenu");

    }

    #endregion

}