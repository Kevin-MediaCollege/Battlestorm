using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour {

    public WaveManager waveManager;
    public ColorCorrectionTransition colorEffect;

    public Text goldCounterText;
    public Text woodCounterText;
    public Text stoneCounterText;
    public Text waveCounterText;

    public Toggle speedUpButton;
    public Button menuButton;
    public Button toMenuButton;
    public Button closeConfirmPopupButton;
    public Button nextWaveButton;

    public CanvasGroup confirmPopup;
    public CanvasGroup fullOverlay;

	void Start () {

        waveManager.OnWaveStarted += WaveManager_OnWaveStarted;
        waveManager.OnEnemiesSpawned += WaveManager_OnEnemiesSpawned;
        menuButton.onClick.AddListener(() => OnMenuPopupButtonClicked());
        nextWaveButton.onClick.AddListener(() => OnNextWaveButtonClicked());
        closeConfirmPopupButton.onClick.AddListener(() => OnMenuPopupClosed());
        toMenuButton.onClick.AddListener(() => OnMenuButtonClicked());
        speedUpButton.onValueChanged.AddListener(delegate { OnSpeedChanged(); });

        confirmPopup.alpha = 0;
        confirmPopup.interactable = false;
        confirmPopup.blocksRaycasts = false;
        fullOverlay.alpha = 0;

    }

    private void OnSpeedChanged () {
        Debug.LogError("Not finished");
    }

    private void WaveManager_OnEnemiesSpawned () {

        MoveNextWaveButton(355);

    }

    private void OnMenuButtonClicked () {

        fullOverlay.DOFade(1, 2).OnComplete(GoToMenu);

    }

    private void GoToMenu () {

        SceneManager.LoadScene("GameMenu");

    }

    private void OnNextWaveButtonClicked () {

        MoveNextWaveButton(430);
        waveManager.StartNextWaveButton();
        colorEffect.TweenToValue(0.5f, 0.5f);
    }

    private void OnMenuPopupButtonClicked () {

        confirmPopup.interactable = true;
        confirmPopup.blocksRaycasts = true;
        confirmPopup.DOFade(1, 0.5f);

    }

    private void OnMenuPopupClosed () {

        confirmPopup.interactable = false;
        confirmPopup.blocksRaycasts = false;
        confirmPopup.DOFade(0, 0.3f);

    }

    private void WaveManager_OnWaveStarted () {

        speedUpButton.isOn = false;
        waveCounterText.text = "" + WaveData.Instance.currentWave;
        MoveNextWaveButton(430);

        if (colorEffect.GetValue() != 1) {
            colorEffect.TweenToValue(1, 0.5f);
        }
    }

    private void MoveNextWaveButton (int _YPosition) {
        nextWaveButton.GetComponent<RectTransform>().DOLocalMoveY(_YPosition, 1);
    }

    // Update is called once per frame
    void FixedUpdate () {

        goldCounterText.text = PlayerData.Instance.goldAmount.ToString();
        stoneCounterText.text = PlayerData.Instance.stoneAmount.ToString();
        woodCounterText.text = PlayerData.Instance.woodAmount.ToString();

    }

}
