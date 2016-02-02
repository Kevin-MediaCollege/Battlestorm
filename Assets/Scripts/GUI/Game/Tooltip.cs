using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BattlestormUI {

    public class Tooltip : MonoBehaviour {

        public Text woodUI, stoneUI, goldUI, textfield;

        private string text;
        private BuildingStats stats;


        void Awake () {

            ClearTexts();

        }

        public void FillContentByType (string _type) {

            EBuildingType parsed_enum = (EBuildingType)System.Enum.Parse(typeof(EBuildingType), _type);
            ClearTexts();
            stats = null;

            stats = BuildingDataManager.Instance.GetDataByType(parsed_enum);
            text = TooltipTexts.Instance.GetTextByBuildingType(parsed_enum);

            if (stats != null) {

                UpdateTooltip();

            }

        }

        private void ClearTexts () {

            woodUI.text = "";
            stoneUI.text = "";
            goldUI.text = "";
            textfield.text = "";

        }

        private void UpdateTooltip () {

            textfield.text = text;

            PlayerData data = PlayerData.Instance;

            goldUI.text = stats.goldCostPerLevel[0].ToString();
            stoneUI.text = stats.stoneCostPerLevel[0].ToString();
            woodUI.text = stats.woodCostPerLevel[0].ToString();

            SetTooltipTextColor(goldUI,  data.goldAmount,  stats.goldCostPerLevel[0] );
            SetTooltipTextColor(stoneUI, data.stoneAmount, stats.stoneCostPerLevel[0]);
            SetTooltipTextColor(woodUI,  data.woodAmount,  stats.woodCostPerLevel[0] );

        }

        private void SetTooltipTextColor(Text _textField, int _resourceAmount, int _resourceCost) {

            if (_resourceAmount >= _resourceCost) {

                _textField.color = Color.green;

            } else {

                _textField.color = Color.red;

            }

        }

    }

}
