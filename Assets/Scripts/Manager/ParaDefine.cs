using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ParaDefine : SingletonMono<ParaDefine>
{
    [Serializable]
    public class LitColorSetting
    {
        public LitColorSetting(Color _color, float _idensity)
        {
            color = _color;
            idensity = _idensity;
        }
        public Color color;
        public float idensity;
    }
    public LitColorSetting signDisableColor, signEnableColor;
    public TowerData defenderData, beaconData, projectorData, parcloseData, detonationData, chargerData;
    public Dictionary<TowerType, TowerData> towerData = new Dictionary<TowerType, TowerData>();
    void Start()
    {
        // InitializeTowerData();
    }
    public void InitializeTowerData()
    {

        towerData.Add(TowerType.Defender, defenderData);
        towerData.Add(TowerType.Beacon, beaconData);
        towerData.Add(TowerType.Projector, projectorData);
        towerData.Add(TowerType.Parclose, parcloseData);
        towerData.Add(TowerType.Detonation, detonationData);
        towerData.Add(TowerType.Charger, chargerData);
        // Debug.Log("after add" + towerData.Count);
    }
}
