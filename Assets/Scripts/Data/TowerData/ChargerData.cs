using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargerData", menuName = "GameData/ChargerData", order = 0)]
public class ChargerData : ScriptableObject
{
    public int cost;
    public float lightRadius;
    public float explodeDelay;
    public float explodeTime;
    public float lightLastTime;
    public AnimationCurve animationCurve;
}
