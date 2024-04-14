using UnityEngine;
[CreateAssetMenu(fileName = "LevelData", menuName = "GameData/LevelData", order = 0)]

public class LevelData : ScriptableObject
{
    public Batch[] batches;
}