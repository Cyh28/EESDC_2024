using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
[CreateAssetMenu(fileName = "TowerData", menuName = "GameData/TowerData", order = 0)]

public class TowerData : ScriptableObject
{
    public int cost;
    public float rotarySpeed;
    public float firingRate;
    public int health;
    public float explodeRadius;
    public float explodeDelay;
    public float explodeTime;
    public float effectLastTime;
    public int damage;
    public AnimationCurve animationCurve;
}
