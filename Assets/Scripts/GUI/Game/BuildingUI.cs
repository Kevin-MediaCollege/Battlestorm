using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour {

    private EBuildingType selectedBuilding;
    public Canvas canvas;

    public Button towerButton;
    public Button mineButton;
    public Button lumbermillButton;
    public Button firetowerButton;
    public Button icetowerButton;
    public RectTransform tooltipPanel;

    public RectTransform spellCircle;

    public RectTransform buildingUIAnchor;

    private CanvasGroup buildingUICanvasGroup;
    private Transform target;
    private bool isSelected;
    private IslandData islandData;
    private BuildingManager buildingManager;

    void Awake () {
        towerButton.onClick.AddListener(() => Build(EBuildingType.TowerNormal));
        firetowerButton.onClick.AddListener(() => Build(EBuildingType.TowerFire));
        icetowerButton.onClick.AddListener(() => Build(EBuildingType.TowerIce));
        lumbermillButton.onClick.AddListener(() => Build(EBuildingType.LumberMill));
        mineButton.onClick.AddListener(() => Build(EBuildingType.Mine));
    }

    void Start () {

        buildingUICanvasGroup = buildingUIAnchor.GetComponent<CanvasGroup>();
        buildingUICanvasGroup.alpha = 0;
        Deselect();

    }

    // Update is called once per frame
    void Update () {

        if (target != null) {

            SetUIPosition();

        }

        if (Input.GetMouseButtonDown(1)) {

            Deselect();

        }

        if (Input.GetMouseButtonDown(0)) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            islandData = null;

            if (Physics.Raycast(ray, out hit, 100) && selectedBuilding == EBuildingType.None) {

                if (!hit.transform.gameObject.CompareTag("Enemy") && !hit.transform.gameObject.CompareTag("Untagged")) {

                    islandData = hit.transform.parent.GetComponent<IslandReference>().iData;
                    target = hit.transform;
                    buildingManager = hit.transform.parent.gameObject.GetComponent<BuildingManager>();

                    StopCoroutine("Select");
                    StartCoroutine("Select");

                    CheckAvailableBuilding(EBuildingType.TowerNormal);
                    CheckAvailableBuilding(EBuildingType.Mine);
                    CheckAvailableBuilding(EBuildingType.LumberMill);
                    CheckAvailableBuilding(EBuildingType.TowerFire);
                    CheckAvailableBuilding(EBuildingType.TowerIce);

                }

            }

        }

    }

    IEnumerator Select () {

        buildingUICanvasGroup.alpha = 1;
        tooltipPanel.GetComponent<CanvasGroup>().alpha = 0;

        //Animate main buttons
        SetUIButtonPosition(towerButton.GetComponent<RectTransform>(), new Vector2(0, 200), 1);
        SetUIButtonPosition(lumbermillButton.GetComponent<RectTransform>(), new Vector2(-100, 75), 1);
        SetUIButtonPosition(mineButton.GetComponent<RectTransform>(), new Vector2(100, 75), 1);
        SetUIButtonPosition(firetowerButton.GetComponent<RectTransform>(), new Vector2(-150, 250), 1);
        SetUIButtonPosition(icetowerButton.GetComponent<RectTransform>(), new Vector2(150, 250), 1);
        SetUIButtonPosition(spellCircle, Vector2.zero, 0.4f);

        yield return new WaitForSeconds(0.4f);
        SetUIButtonPosition(tooltipPanel, new Vector2(0, -100), 1);

        isSelected = true;

    }

    void Deselect () {

        selectedBuilding = EBuildingType.None;
        target = null;
        isSelected = false;

        if (buildingUICanvasGroup.alpha != 0) {

            buildingUICanvasGroup.DOFade(0, 0.35f);

        }

    }

    void Build (EBuildingType _type) {
        buildingManager.CreateBuilding(_type);

        BuildingStats stats = BuildingDataManager.Instance.GetDataByType(_type);

        PlayerData.Instance.goldAmount -= stats.goldCostPerLevel[0];
        PlayerData.Instance.stoneAmount -= stats.stoneCostPerLevel[0];
        PlayerData.Instance.woodAmount -= stats.woodCostPerLevel[0];

        Deselect();
    }
    private void SetUIButtonPosition (RectTransform _uiObject, Vector3 _endpos,float _alpha) {

        _uiObject.localPosition = this.transform.localPosition;
        _uiObject.DOLocalMove(this.transform.localPosition + _endpos, 0.5f);
        _uiObject.localScale = Vector3.zero;
        _uiObject.DOScale(new Vector3(1, 1, 1), 0.5f);
        _uiObject.GetComponent<CanvasGroup>().alpha = 0;
        _uiObject.GetComponent<CanvasGroup>().DOFade(_alpha, 0.35f);

    }

    private void CheckAvailableBuilding (EBuildingType _type) {

        bool canHaveBuilding = false;

        for (int i = 0; i < islandData.allowedBuildings.Length; i++) {
            if(islandData.allowedBuildings[i] == _type) {
                canHaveBuilding = true;
            }
        }



        switch (_type) {

            case EBuildingType.TowerNormal:
            SetButton(towerButton, canHaveBuilding);
            break;
            case EBuildingType.LumberMill:
            SetButton(lumbermillButton, canHaveBuilding);
            break;
            case EBuildingType.Mine:
            SetButton(lumbermillButton, canHaveBuilding);
            break;
            case EBuildingType.TowerFire:
            SetButton(firetowerButton, canHaveBuilding);
            break;
            case EBuildingType.TowerIce:
            SetButton(icetowerButton, canHaveBuilding);
            break;

        }

    }
 
    private void SetButton (Button _button,bool _canhave) {

        BuildingButtonData buttonData = _button.GetComponent<BuildingButtonData>();
        BuildingStats stats = buttonData.stats;
        _button.interactable = false;

        if (_canhave) {

            if (PlayerData.Instance.goldAmount >= stats.goldCostPerLevel[0] && PlayerData.Instance.stoneAmount >= stats.stoneCostPerLevel[0] && PlayerData.Instance.woodAmount >= stats.woodCostPerLevel[0]) {
                //We can purchase this building
                _button.interactable = true;
                _button.image.sprite = buttonData.normal;

            } else {
                //Not enough gold
                _button.image.sprite = buttonData.noGold;
            }
        } else {
            //unavailable
            _button.image.sprite = buttonData.unavailable;
        }
       
    }

    private void SetUIPosition () {

        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(target.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        buildingUIAnchor.anchoredPosition = WorldObject_ScreenPosition;

    }

}
