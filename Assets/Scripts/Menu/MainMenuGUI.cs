using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuGUI : BaseMenuState {

    public Button playButton;
    public Button optionsButton;
    public Button creditsButton;
    public Button quitButton;

    void Awake () {

        playButton.onClick.AddListener(() => OnPlayClicked());
        quitButton.onClick.AddListener(() => OnQuitClicked());
        optionsButton.onClick.AddListener(() => OnOptionsClicked());
        creditsButton.onClick.AddListener(() => OnCreditsClicked());
        

    }

    public override void Enter () {
        base.Enter();
    }

    public override void Exit () {
        base.Exit();
    }

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

}
