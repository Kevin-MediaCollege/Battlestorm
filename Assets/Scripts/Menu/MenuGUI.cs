using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

    public MainMenuGUI MainMenu;
    public OptionsGUI OptionsMenu;
    public CreditsGUI CreditsMenu;
    public CameraMenu flybyCamera;

    private BaseMenuState currentMenuState;
    public FadeScript fadeEffect;
    private string nextStateName;
    public AudioSource clicksource;

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

    private void OnStateSwitch (string _state) {
        nextStateName = _state;

        fadeEffect.StartFade();
        fadeEffect.OnFadeComplete += FadeEffect_OnFadeComplete;
    }

    private void OnClick () {
        clicksource.Play();
    }

    private void FadeEffect_OnFadeComplete () {
        currentMenuState.Exit();
        currentMenuState.SetCamera(false);
        fadeEffect.OnFadeComplete -= FadeEffect_OnFadeComplete;
    
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

}


public class BaseMenuState : MonoBehaviour {

    public delegate void StateSwitch (string _state);
    public event StateSwitch OnStateSwitch;

    public delegate void ClickEvent ();
    public event ClickEvent onCLick;

    public Camera stateCamera;

    void Awake () {
        
        this.gameObject.SetActive(false);
    }

    public virtual void Exit () {
        this.gameObject.SetActive(false);
    }

    public virtual void Enter () {
        this.gameObject.SetActive(true);
    }

    public virtual void SetCamera(bool _state) {
        stateCamera.enabled = _state;
    }

    public void SwitchState (string _state) {
        OnStateSwitch(_state);
    }

    public void Click () {
        onCLick();
    }
    
}
