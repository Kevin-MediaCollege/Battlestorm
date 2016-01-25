using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The Credits State of the menu.
/// </summary>
public class CreditsGUI : BaseMenuState {

    #region Variables

    public Button backButton;

    #endregion


    #region Unity Functions

    void Awake () {

        backButton.onClick.AddListener(() => OnBackClicked());

    }

    #endregion

    #region Events

    private void OnBackClicked () {

        Click();
        SwitchState("Main");

    }

    #endregion

}
