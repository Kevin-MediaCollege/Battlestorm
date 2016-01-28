using UnityEngine;
using System.Collections;

public class BuildingDataManager : MonoBehaviour  {

    public BuildingStats[] stats;
    public static BuildingDataManager Instance { get; private set; }

    void Awake () {
        Instance = this;
    }
    public BuildingStats GetDataByType(EBuildingType _type) {

        switch (_type) {

            case EBuildingType.TowerNormal:
            return stats[0];
            case EBuildingType.LumberMill:
            return stats[1];
            case EBuildingType.Mine:
            return stats[2];
            case EBuildingType.TowerIce:
            return stats[3];
            case EBuildingType.TowerFire:
            return stats[4];

        }
        return null;
    }

}
