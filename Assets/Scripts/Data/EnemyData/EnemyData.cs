using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "GameData/EnemyData", order = 0)]

public class EnemyData : ScriptableObject
{
    public float speed_decay;
    public float rhombus_speed_range;
    public float rhombus_speed_mul;
    public float pentagon_fire_gap_time;
    public float pentagon_rotate_speed;
    public float pentagon_call_time;
    public float pentagon_swim_time;
    public float max_speed;
}

