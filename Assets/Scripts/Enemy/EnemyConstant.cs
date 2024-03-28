using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
public enum EnemyType
{
    None,
    Triangle,   //enemyData.low hp enemyData.medium speed
    Dot,        //enemyData.low hp enemyData.high speed
    Square,     //enemyData.medium hp enemyData.medium speed
    Circle,     //enemyData.medium hp enemyData.medium speed     split a dot
    Pentagon,   // create a dot
    Rhombus,    // speed up fel enemyData.lows after death
    Star,       // create cloud forward  
    Hexagon,    //  enemyData.high hp    create two Rhombus when half hp
}
public class Constant : SingletonMono<Constant>
{
    public NormalEnemy dotData;
    public NormalEnemy circleData;
    public NormalEnemy hexagonData;
    public NormalEnemy rhombusData;
    public NormalEnemy squareData;
    public NormalEnemy triangleData;
    public NormalEnemy pentagonData;
    public NormalEnemy starData;
    public EnemyData enemyData;
    public static Dictionary<EnemyType, int> HpDic;
    public static Dictionary<EnemyType, float> SpeedDic;
    public static Dictionary<EnemyType, int> DamageDic;
    public static Dictionary<EnemyType, int> ScoreDic;
    public static Dictionary<EnemyType, int> EnergyDic;
    public static int decay_cnt;
    public static int damage_cnt;
    public static int pentagon_call_cnt;
    public static float speed_decay;
    public static float speed_range;
    public static float speed_mul;
    public static float max_speed;

    private void Start()
    {
        HpDic = new Dictionary<EnemyType, int>
        {
            {EnemyType.Triangle, triangleData.hp },
            {EnemyType.Dot, dotData.hp},
            {EnemyType.Square,squareData.hp},
            {EnemyType.Circle,circleData.hp},
            {EnemyType.Pentagon,pentagonData.hp},
            {EnemyType.Rhombus,rhombusData.hp},
            {EnemyType.Star,starData.hp},
            {EnemyType.Hexagon,hexagonData.hp},
        };
        SpeedDic = new Dictionary<EnemyType, float>
        {
            {EnemyType.Triangle, triangleData.speed},
            {EnemyType.Dot, dotData.speed},
            {EnemyType.Square, squareData.speed },
            {EnemyType.Circle,circleData.speed},
            {EnemyType.Pentagon ,pentagonData.speed},
            {EnemyType.Rhombus ,rhombusData.speed},
            {EnemyType.Star,starData.speed},
            {EnemyType.Hexagon,hexagonData.speed},
        };
        DamageDic = new Dictionary<EnemyType, int>
        {
            {EnemyType.Triangle, triangleData.damage},
            {EnemyType.Dot, dotData.damage},
            {EnemyType.Square, squareData.damage },
            {EnemyType.Circle,circleData.damage},
            {EnemyType.Pentagon ,pentagonData.damage},
            {EnemyType.Rhombus ,rhombusData.damage},
            {EnemyType.Star,starData.damage},
            {EnemyType.Hexagon,hexagonData.damage},
        };
        ScoreDic = new Dictionary<EnemyType, int>
        {
            {EnemyType.Triangle, triangleData.reward_score},
            {EnemyType.Dot, dotData.reward_score},
            {EnemyType.Square, squareData.reward_score },
            {EnemyType.Circle,circleData.reward_score},
            {EnemyType.Pentagon ,pentagonData.reward_score},
            {EnemyType.Rhombus ,rhombusData.reward_score},
            {EnemyType.Star,starData.reward_score},
            {EnemyType.Hexagon,hexagonData.reward_score},
        };
        EnergyDic = new Dictionary<EnemyType, int>
        {
            {EnemyType.Triangle, triangleData.reward_energy},
            {EnemyType.Dot, dotData.reward_energy},
            {EnemyType.Square, squareData.reward_energy },
            {EnemyType.Circle,circleData.reward_energy},
            {EnemyType.Pentagon ,pentagonData.reward_energy},
            {EnemyType.Rhombus ,rhombusData.reward_energy},
            {EnemyType.Star,starData.reward_energy},
            {EnemyType.Hexagon,hexagonData.reward_energy},
        };

        speed_decay = enemyData.speed_decay;
        decay_cnt = enemyData.decay_cnt;
        damage_cnt = enemyData.damage_cnt;
        pentagon_call_cnt = enemyData.pentagon_call_cnt;
        speed_range = enemyData.rhombus_speed_range;
        speed_mul = enemyData.rhombus_speed_mul;
        max_speed = enemyData.max_speed;
    }
}