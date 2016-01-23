using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditsGUI : BaseMenuState {

    public Button backButton;

    void Awake () {

        backButton.onClick.AddListener(() => OnBackClicked());

    }

    private void OnBackClicked () {
        Click();
        SwitchState("Main");
    }

}
