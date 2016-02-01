using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

namespace BattlestormUI {

    public class BuildingUI : MonoBehaviour {

        public Canvas canvas;

        //--------UI Elements-------------------->

        public Button towerButton;
        public Button mineButton;
        public Button lumbermillButton;
        public Button firetowerButton;
        public Button icetowerButton;
        public Button bridgeButton;

        public RectTransform tooltipPanel;
        public RectTransform spellCircle;
        public RectTransform buildingUIAnchor;

        public CanvasGroup buildingUICanvasGroup;
        public CanvasGroup bridgeUICanvasGroup;

        public UpgradeUI upgradeUI;
        //-------------------------------------->


        private Transform target;
        private bool isSelected;
        private IslandData islandData;
        private BuildingManager buildingManager;
        private Bridge bridgeManager;
        private Building building;
        private Tooltip tooltip;

        void Awake () {

            towerButton.onClick.AddListener(() => Build(EBuildingType.TowerNormal));
            firetowerButton.onClick.AddListener(() => Build(EBuildingType.TowerFire));
            icetowerButton.onClick.AddListener(() => Build(EBuildingType.TowerIce));
            lumbermillButton.onClick.AddListener(() => Build(EBuildingType.LumberMill));
            mineButton.onClick.AddListener(() => Build(EBuildingType.Mine));
            bridgeButton.onClick.AddListener(() => Build(EBuildingType.Bridge));
            tooltip = tooltipPanel.GetComponent<Tooltip>();

            Deselect();

        }



        // Update is called once per frame
        void Update () {

            if (target != null) {

                SetUIWorldPosition();

            }

            if (Input.GetMouseButtonDown(1)) {

                Deselect();

            }

            if (Input.GetMouseButtonDown(0)) {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                islandData = null;

                if (Physics.Raycast(ray, out hit, 100) && !isSelected) {

                    if (!hit.transform.gameObject.CompareTag("Enemy") && !hit.transform.gameObject.CompareTag("Untagged")) {
                       
                        IslandReference islandReference = hit.transform.GetComponent<IslandReference>();
                        if (islandReference != null) {

                            islandData = hit.transform.GetComponent<IslandReference>().iData;

                        }

                        target = hit.transform;
                        buildingManager = hit.transform.gameObject.GetComponent<BuildingManager>();
                        BuildingType buildingType = hit.transform.GetComponent<BuildingType>();
                        building = hit.transform.gameObject.GetComponent<Building>();

                        StopCoroutine("SelectBridge");
                        StopCoroutine("Select");

                        bool isUpgrade = true;

                        switch (buildingType.type) {

                            case EBuildingType.Empty:

                            StartCoroutine("Select");

                            CheckAvailableBuilding(EBuildingType.TowerNormal);
                            CheckAvailableBuilding(EBuildingType.Mine);
                            CheckAvailableBuilding(EBuildingType.LumberMill);
                            CheckAvailableBuilding(EBuildingType.TowerFire);
                            CheckAvailableBuilding(EBuildingType.TowerIce);

                            isUpgrade = false;

                            break;

                            case EBuildingType.Bridge:

                            bridgeManager = hit.transform.parent.gameObject.GetComponent<Bridge>();
                            StartCoroutine("SelectBridge");

                            isUpgrade = false;

                            break;

                        }

                        if (isUpgrade) {
                            
                            upgradeUI.Open(building, buildingType.type);

                        }

                    }

                }

            }

        }

        IEnumerator SelectBridge () {

            buildingUICanvasGroup.interactable = false;
            buildingUICanvasGroup.blocksRaycasts = false;
            bridgeUICanvasGroup.interactable = true;
            bridgeUICanvasGroup.blocksRaycasts = true;

            bridgeUICanvasGroup.alpha = 1;

            SetUIPosition(tooltipPanel, new Vector2(0, -100), 1);
            SetUIPosition(bridgeButton.GetComponent<RectTransform>(), new Vector2(0, 200), 1);
            SetUIPosition(bridgeUICanvasGroup.transform.Find("Circle").GetComponent<RectTransform>(), Vector2.zero, 0.4f);

            yield return new WaitForSeconds(1);

        }

        IEnumerator Select () {

            buildingUICanvasGroup.alpha = 1;
            tooltipPanel.GetComponent<CanvasGroup>().alpha = 0;

            buildingUICanvasGroup.interactable = true;
            buildingUICanvasGroup.blocksRaycasts = true;
            bridgeUICanvasGroup.interactable = false;
            bridgeUICanvasGroup.blocksRaycasts = false;

            //Animate main buttons
            SetUIPosition(towerButton.GetComponent<RectTransform>(), new Vector2(0, 200), 1);
            SetUIPosition(lumbermillButton.GetComponent<RectTransform>(), new Vector2(-100, 75), 1);
            SetUIPosition(mineButton.GetComponent<RectTransform>(), new Vector2(100, 75), 1);
            SetUIPosition(firetowerButton.GetComponent<RectTransform>(), new Vector2(-150, 250), 1);
            SetUIPosition(icetowerButton.GetComponent<RectTransform>(), new Vector2(150, 250), 1);
            SetUIPosition(spellCircle, Vector2.zero, 0.4f);

            yield return new WaitForSeconds(0.4f);
            SetUIPosition(tooltipPanel, new Vector2(0, -100), 1);

            isSelected = true;

        }

        void Deselect () {

            target = null;
            isSelected = false;

            mineButton.interactable = false;
            towerButton.interactable = false;
            icetowerButton.interactable = false;
            firetowerButton.interactable = false;
            lumbermillButton.interactable = false;

            if (buildingUICanvasGroup.alpha != 0) {

                buildingUICanvasGroup.DOFade(0, 0.35f);

            }

            if (bridgeUICanvasGroup.alpha != 0) {

                bridgeUICanvasGroup.DOFade(0, 0.35f);

            }

            if (upgradeUI.isOpen) {

                upgradeUI.Close();

            }

            tooltipPanel.GetComponent<CanvasGroup>().DOFade(0, 0.35f);

        }

        void Build (EBuildingType _type) {

            if (_type == EBuildingType.Bridge) {

                bridgeManager.Build();

            } else {

                buildingManager.CreateBuilding(_type);

                BuildingStats stats = BuildingDataManager.Instance.GetDataByType(_type);
                
                PlayerData.Instance.goldAmount -= stats.goldCostPerLevel[0];
                PlayerData.Instance.stoneAmount -= stats.stoneCostPerLevel[0];
                PlayerData.Instance.woodAmount -= stats.woodCostPerLevel[0];

            }

            Deselect();

        }

        private void SetUIPosition (RectTransform _uiObject, Vector3 _endpos, float _alpha) {

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

                if (islandData.allowedBuildings[i] == _type) {

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
                SetButton(mineButton, canHaveBuilding);
                break;
                case EBuildingType.TowerFire:
                SetButton(firetowerButton, canHaveBuilding);
                break;
                case EBuildingType.TowerIce:
                SetButton(icetowerButton, canHaveBuilding);
                break;

            }

        }

        /// <summary>
        /// Changes to button to the Available / Unavailable / NotEnoughCurrency icon.
        /// </summary>
        /// <param name="_button">The button we apply the change to.</param>
        /// <param name="_canhave">if this button is available or not.</param>
        private void SetButton (Button _button, bool _canhave) {

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

        /// <summary>
        /// Sets the UI anchor to the world position of the targeted object.
        /// </summary>
        private void SetUIWorldPosition () {

            RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(target.position);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            buildingUIAnchor.anchoredPosition = WorldObject_ScreenPosition;

        }

        /// <summary>
        /// Shows tooltip when hovered over the building buttons.
        /// </summary>
        public void OnButtonHover (string _name) {

            tooltip.FillContentByType(_name);

        }

        
    }

}
