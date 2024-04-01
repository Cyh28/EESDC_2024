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
            this.color = _color;
            this.idensity = _idensity;
        }
        public Color color;
        public float idensity;
    }
    public LitColorSetting signDisableColor, signEnableColor;
    public TowerData defenderData, beaconData, projectorData, parcloseData;
    public DetonationData detonationData;
}
