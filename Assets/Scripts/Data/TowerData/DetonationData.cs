using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DetonationData", menuName = "GameData/DetonationData", order = 0)]
public class DetonationData : ScriptableObject
{
    public int cost;
    public float explodeRadius;
    public int damage;
    public float explodeTime;
    public float explodeLastTime;
    public AnimationCurve animationCurve;
}
