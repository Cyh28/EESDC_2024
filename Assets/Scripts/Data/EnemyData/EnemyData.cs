using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "GameData/EnemyData", order = 0)]

public class EnemyData : ScriptableObject
{
    public int decay_cnt;
    public float speed_decay;
    public int damage_cnt;
    public float rhombus_speed_range;
    public float rhombus_speed_mul;
    public int pentagon_call_cnt;
    public float max_speed;
}

