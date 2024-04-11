using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelData", menuName = "GameData/LevelData", order = 0)]

public class LevelData : ScriptableObject
{
    public float wait_time;
    public float generate_gap_time;
    public Batch[] batches;
}