using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The Main Menu State of the menu.
/// </summary>
public class MainMenuGUI : BaseMenuState {

    #region Variables

    public Button playButton;
    public Button optionsButton;
    public Button creditsButton;
    public Button quitButton;

    #endregion


    #region Unity Functions

    void Awake () {

        playButton.onClick.AddListener(() => OnPlayClicked());
        quitButton.onClick.AddListener(() => OnQuitClicked());
        optionsButton.onClick.AddListener(() => OnOptionsClicked());
        creditsButton.onClick.AddListener(() => OnCreditsClicked());

        #if UNITY_WEBGL
        quitButton.gameObject.SetActive(false);
        transform.Find("Anchor").GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-100);
        #endif

    }

    #endregion

    #region BaseMenuState-Options

    public override void Enter () {
        base.Enter();
    }

    public override void Exit () {
        base.Exit();
    }

    #endregion

    #region Events

    private void OnPlayClicked () {

        Click();
        LoadingScreen.Instance.loadLoadingScreen("GameMap");

    }

    private void OnOptionsClicked () {

        Click();
        SwitchState("Options");

    }

    private void OnCreditsClicked () {

        Click();
        SwitchState("Credits");

    }

    private void OnQuitClicked () {

        Click();
        Application.Quit();

    }

    #endregion

}
