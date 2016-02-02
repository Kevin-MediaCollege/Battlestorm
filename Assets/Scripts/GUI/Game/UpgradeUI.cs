using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

namespace BattlestormUI {

    public class UpgradeUI : MonoBehaviour {

        public bool isOpen;

        public delegate void UpgradeAction ();
        public event UpgradeAction OnUpgradeBought;
        public event UpgradeAction OnUpgradeSold;

        [SerializeField] private TransactionTexts sellTexts, buyTexts;
        [SerializeField] private BuildingDetailTexts detailTexts;
        [SerializeField] private Button buyButton, sellButton;

        private int calculatedGoldBuyPrice, calculatedWoodBuyPrice, calculatedStoneBuyPrice;
        private int calculatedGoldSellPrice, calculatedWoodSellPrice, calculatedStoneSellPrice;

        private CanvasGroup canvasGroup;
        private Building selectedBuilding;

        void Awake () {

            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;

            buyButton.onClick.AddListener(()  => TryToBuy ());
            sellButton.onClick.AddListener(() => SellBuilding());
        }

        void FixedUpdate () {
            if (isOpen)
                UpdateBuyText();
        }

        private void TryToBuy () {
            if (PlayerData.Instance.goldAmount  >= calculatedGoldBuyPrice && 
                PlayerData.Instance.woodAmount  >= calculatedWoodBuyPrice && 
                PlayerData.Instance.stoneAmount >= calculatedStoneBuyPrice) {

                Instantiate(Resources.Load("Prefabs/SoundPrefabs/UpgradeSound"), selectedBuilding.transform.position, Quaternion.identity);
                PlayerData.Instance.goldAmount -= calculatedGoldBuyPrice;
                PlayerData.Instance.stoneAmount -= calculatedWoodBuyPrice;
                PlayerData.Instance.woodAmount -= calculatedStoneBuyPrice;
                selectedBuilding.SwitchLevel(selectedBuilding.currentLevel + 1);

                OnUpgradeBought();
            }
        }

        private void SellBuilding () {

            BuildingManager buildingManager = selectedBuilding.transform.parent.GetComponent<BuildingManager>();

            PlayerData.Instance.goldAmount += calculatedGoldSellPrice;
            PlayerData.Instance.stoneAmount += calculatedStoneSellPrice;
            PlayerData.Instance.woodAmount += calculatedWoodSellPrice;

            buildingManager.DestroyBuilding(selectedBuilding);

            OnUpgradeSold();

        }

        public void Open (Building _building, EBuildingType _type) {

            selectedBuilding = _building;

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            isOpen = true;
            canvasGroup.DOFade(1, 0.35f);

            ChangeLayout(_type);
            ChangeBuyLayout();
            CalculatePrices();
            SetDetails();
           

        }

        public void Close () {

            isOpen = false;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOFade(0, 0.35f);
           
        }

        private void CalculatePrices () {

            calculatedGoldBuyPrice  = selectedBuilding.stats.goldCostPerLevel [selectedBuilding.currentLevel];
            calculatedStoneBuyPrice = selectedBuilding.stats.stoneCostPerLevel[selectedBuilding.currentLevel];
            calculatedWoodBuyPrice  = selectedBuilding.stats.woodCostPerLevel [selectedBuilding.currentLevel];

            calculatedGoldSellPrice = (Mathf.FloorToInt(selectedBuilding.stats.goldCostPerLevel[selectedBuilding.currentLevel - 1] * selectedBuilding.stats.sellRate));
            calculatedStoneSellPrice = (Mathf.FloorToInt(selectedBuilding.stats.stoneCostPerLevel[selectedBuilding.currentLevel - 1] * selectedBuilding.stats.sellRate));
            calculatedWoodSellPrice = (Mathf.FloorToInt(selectedBuilding.stats.woodCostPerLevel[selectedBuilding.currentLevel - 1] * selectedBuilding.stats.sellRate));

        }

        private void UpdateBuyText () {

            SetText(buyTexts.goldText, " " + calculatedGoldBuyPrice, !(PlayerData.Instance.goldAmount >= calculatedGoldBuyPrice));
            SetText(buyTexts.stoneText, " " + calculatedStoneBuyPrice, !(PlayerData.Instance.goldAmount >= calculatedStoneBuyPrice));
            SetText(buyTexts.woodText, " " + calculatedWoodBuyPrice, !(PlayerData.Instance.goldAmount >= calculatedWoodBuyPrice));

        }

        private void SetDetails () {

            Tower tower = selectedBuilding.GetComponent<Tower>();

            if (tower != null) {

                SetText(detailTexts.rangeText,  "Range: "  + tower.stats.rangePerLevel[tower.currentLevel],  Color.white);
                SetText(detailTexts.damageText, "Damage: " + tower.stats.damagePerLevel[tower.currentLevel], Color.white);
                SetText(detailTexts.speedText,  "Speed: "  + tower.stats.speedPerLevel[tower.currentLevel],  Color.white);
               
            } else {

                SetText(detailTexts.resourceText,"Resources per tick: " + selectedBuilding.stats.resourcesPerTick[selectedBuilding.currentLevel],Color.white);

            }

            SetText(detailTexts.levelText, "Level: " + selectedBuilding.currentLevel, Color.white);
            detailTexts.buildingText.text = selectedBuilding.name;

            SetText(sellTexts.woodText, "+ " + calculatedWoodSellPrice, Color.green);
            SetText(sellTexts.stoneText, "+ " + calculatedStoneSellPrice, Color.green);
            SetText(sellTexts.goldText, "+ " + calculatedGoldSellPrice, Color.green);

        }

        private void ChangeLayout (EBuildingType _type) {

            if (_type == EBuildingType.LumberMill || _type == EBuildingType.Mine) {

                detailTexts.damageText.gameObject.SetActive(false);
                detailTexts.rangeText.gameObject.SetActive(false);
                detailTexts.speedText.gameObject.SetActive(false);
                detailTexts.rangeText.gameObject.SetActive(false);

                detailTexts.resourceText.gameObject.SetActive(true);

            } else {

                detailTexts.damageText.gameObject.SetActive(true);
                detailTexts.rangeText.gameObject.SetActive(true);
                detailTexts.speedText.gameObject.SetActive(true);
                detailTexts.rangeText.gameObject.SetActive(true);

                detailTexts.resourceText.gameObject.SetActive(false);

            }

        }

        private void ChangeBuyLayout () {

            if (selectedBuilding.currentLevel == selectedBuilding.stats.levels) {
                //Deactivate Buy Panel.

                buyButton.interactable = false;
                buyButton.transform.parent.gameObject.SetActive(false);

            } else {
                //Activate Buy Panel

                buyButton.transform.parent.gameObject.SetActive(true);
                buyButton.interactable = true;

            }

        }

        private void SetText(Text _textField, string _newText,Color _color) {

            _textField.color = _color;
            _textField.text = _newText;

        }

        private void SetText(Text _textField, string _newText, bool _isRed) {

            if (_isRed) {

                _textField.color = Color.red;

            } else {

                _textField.color = Color.white;

            }

            _textField.text = _newText;

        }

    }

    [System.Serializable]
    public class TransactionTexts {

        public Text woodText;
        public Text stoneText;
        public Text goldText;

    }

    [System.Serializable]
    public class BuildingDetailTexts {

        public Text levelText;
        public Text buildingText;
        public Text damageText;
        public Text rangeText;
        public Text speedText;
        public Text resourceText;

    }

}