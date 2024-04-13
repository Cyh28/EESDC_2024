using UnityEngine;
[CreateAssetMenu(fileName = "NormalEnemyData", menuName = "GameData/NormalEnemyData", order = 0)]

public class NormalEnemy : ScriptableObject
{
    public int hp;
    public float speed_rate;
    public int damage;
    public int reward_score;
    public int reward_energy;
    public float max_speed;
}