using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsGUI : BaseMenuState {

    public Button backButton;
    public Slider fieldofView;
    public Slider musicSlider;
    public Slider soundSlider;
    public Text fieldofViewText;

    void Awake () {

        backButton.onClick.AddListener(() => OnBackClicked());
        fieldofView.onValueChanged.AddListener(delegate { OnFieldOfViewChanged(); });
        musicSlider.onValueChanged.AddListener(delegate { OnMusicChanged(); });
        soundSlider.onValueChanged.AddListener(delegate { OnSoundChanged(); });
        musicSlider.value = VolumeManager.MusicVolume;
        soundSlider.value = VolumeManager.SoundVolume;
        fieldofViewText.text = "90";
        fieldofView.value = 0.50f;
    }

    private void OnFieldOfViewChanged () {

        int val = 70 + (int)(40 * fieldofView.value);
        fieldofViewText.text = "" + val;
        InputHandler.FOV = val;

    }

    private void OnMusicChanged () {

        VolumeManager.MusicVolume = musicSlider.value;
        
    }

    private void OnSoundChanged () {

        VolumeManager.SoundVolume = soundSlider.value;

    }

    private void OnBackClicked () {
        Click();
        SwitchState("Main");
    }

}
