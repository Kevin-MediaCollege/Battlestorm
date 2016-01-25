using UnityEngine;
using System.Collections;

public class BaseMenuState : MonoBehaviour {

    #region Variables

    public delegate void StateSwitch (string _state);
    public event StateSwitch OnStateSwitch;

    public delegate void ClickEvent ();
    public event ClickEvent onCLick;

    public Camera stateCamera;

    #endregion


    #region Unity Functions

    void Awake () {

        this.gameObject.SetActive(false);

    }

    #endregion

    #region Functions

    public virtual void Exit () {

        this.gameObject.SetActive(false);

    }

    public virtual void Enter () {

        this.gameObject.SetActive(true);

    }

    public virtual void SetCamera (bool _state) {

        stateCamera.enabled = _state;

    }

    public void SwitchState (string _state) {

        OnStateSwitch(_state);

    }

    public void Click () {

        onCLick();

    }

    #endregion

}
