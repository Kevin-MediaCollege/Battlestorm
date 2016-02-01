using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BattlestormUI {

    public class Tooltip : MonoBehaviour {

        public Text woodUI;
        public Text stoneUI;
        public Text goldUI;
        public Text textfield;

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

            switch (parsed_enum) {

                case EBuildingType.None:
                text = TooltipTexts.Instance.build_not_available;
                break;

                case EBuildingType.TowerNormal:
                text = TooltipTexts.Instance.build_tower_normal;
                break;

                case EBuildingType.LumberMill:
                text = TooltipTexts.Instance.build_lumbermill;
                break;

                case EBuildingType.Mine:
                text = TooltipTexts.Instance.build_mine;
                break;

                case EBuildingType.Bridge:
                text = TooltipTexts.Instance.build_bridge;
                break;

                case EBuildingType.TowerIce:
                text = TooltipTexts.Instance.build_tower_ice;
                break;

                case EBuildingType.TowerFire:
                text = TooltipTexts.Instance.build_tower_fire;
                break;

            }

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

            if (data.goldAmount >= stats.goldCostPerLevel[0]) {
                goldUI.color = Color.green;
            } else {
                goldUI.color = Color.red;
            }

            if (data.stoneAmount >= stats.stoneCostPerLevel[0]) {
                stoneUI.color = Color.green;
            } else {
                stoneUI.color = Color.red;
            }

            if (data.woodAmount >= stats.woodCostPerLevel[0]) {
                woodUI.color = Color.green;
            } else {
                woodUI.color = Color.red;
            }

        }

    }

}
