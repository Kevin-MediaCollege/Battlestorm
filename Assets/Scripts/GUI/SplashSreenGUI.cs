using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
        #endif

        mainGroup.DOFade(1, 5);
        StartCoroutine(WaitToFadeOut());

    }

    #endregion

    #region Functions

    IEnumerator WaitToFadeOut () {

        yield return new WaitForSeconds(6);

        mainGroup.DOFade(0, 2);

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("GameMenu");

    }

    #endregion

}