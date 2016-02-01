using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

namespace BattlestormUI {

    public class UpgradeUI : MonoBehaviour {

        public bool isOpen;

        [SerializeField]
        private TransactionTexts sellTexts, buyTexts;

        [SerializeField]
        private BuildingDetailTexts detailTexts;

        private CanvasGroup canvasGroup;
        private Building selectedBuilding;

        void Awake () {

            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;

        }


        public void Open (Building _building, EBuildingType _type) {

            selectedBuilding = _building;

            isOpen = true;
            canvasGroup.DOFade(1, 0.35f);

            ChangeLayout(_type);
            SetDetails();
            SetSellDetails();
        }

        public void Close () {

            isOpen = false;
            canvasGroup.DOFade(0, 0.35f);
           
        }

        private void SetSellDetails () {

            SetText(sellTexts.woodText, "+ " + (Mathf.FloorToInt(selectedBuilding.stats.woodCostPerLevel[selectedBuilding.currentLevel - 1] * selectedBuilding.stats.sellRate)),false);
            SetText(sellTexts.stoneText, "+ " + (Mathf.FloorToInt(selectedBuilding.stats.stoneCostPerLevel[selectedBuilding.currentLevel - 1] * selectedBuilding.stats.sellRate)),false);
            SetText(sellTexts.goldText, "+ " + (Mathf.FloorToInt(selectedBuilding.stats.goldCostPerLevel[selectedBuilding.currentLevel - 1] * selectedBuilding.stats.sellRate)),false);

        }

        private void SetDetails () {

            Tower tower = selectedBuilding.GetComponent<Tower>();

            if (tower != null) {

                SetText(detailTexts.rangeText, "Range: " + tower.stats.rangePerLevel[tower.currentLevel]);
                SetText(detailTexts.damageText, "Damage: " + tower.stats.damagePerLevel[tower.currentLevel]);
                SetText(detailTexts.speedText, "Speed: " + tower.stats.speedPerLevel[tower.currentLevel]);
               
            } else {

                SetText(detailTexts.resourceText,"Resources per tick: " + selectedBuilding.stats.resourcesPerTick[selectedBuilding.currentLevel]);

            }

            SetText(detailTexts.levelText, "Level: " + selectedBuilding.currentLevel);
            detailTexts.buildingText.text = selectedBuilding.name;

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

        private void SetText(Text _textField, string _newText) {

            _textField.color = Color.white;
            _textField.text = _newText;

        }

        private void SetText(Text _textField, string _newText, bool _isRed) {

            if (_isRed) {
                _textField.color = Color.red;
            } else {
                _textField.color = Color.green;
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