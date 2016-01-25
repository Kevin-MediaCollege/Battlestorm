using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

    #region Variables

    public MainMenuGUI MainMenu;
    public OptionsGUI OptionsMenu;
    public CreditsGUI CreditsMenu;
    public CameraMenu flybyCamera;

    private BaseMenuState currentMenuState;
    public FadeScript fadeEffect;
    private string nextStateName;
    public AudioSource clicksource;

    #endregion


    #region Unity Functions

    void Start () {

        MainMenu.OnStateSwitch += OnStateSwitch;
        OptionsMenu.OnStateSwitch += OnStateSwitch;
        CreditsMenu.OnStateSwitch += OnStateSwitch;
        MainMenu.onCLick += OnClick;
        OptionsMenu.onCLick += OnClick;
        CreditsMenu.onCLick += OnClick;
        MainMenu.Enter();
        currentMenuState = MainMenu;

    }

    #endregion

    #region Events

    private void OnStateSwitch (string _state) {
        nextStateName = _state;

        fadeEffect.StartFade();
        fadeEffect.OnFadeComplete += OnFadeComplete;
    }

    private void OnClick () {

        clicksource.Play();

    }

    private void OnFadeComplete () {

        currentMenuState.Exit();
        currentMenuState.SetCamera(false);
        fadeEffect.OnFadeComplete -= OnFadeComplete;

        switch (nextStateName) {

            case "Main":
            currentMenuState = MainMenu;
            flybyCamera.stopMoving = false;
            break;

            case "Options":
            currentMenuState = OptionsMenu;
            flybyCamera.stopMoving = true;
            break;

            case "Credits":
            currentMenuState = CreditsMenu;
            flybyCamera.stopMoving = true;
            break;

        }

        currentMenuState.SetCamera(true);
        currentMenuState.Enter();

    }

    #endregion

}
