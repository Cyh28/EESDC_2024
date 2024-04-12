using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UIElements;
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
    public LevelData levelData_0;
    public LevelData levelData_1;
    public LevelData levelData_2;
    public LevelData levelData_3;

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
    public static Dictionary<int, Batch[]> LevelDic;
    public static Dictionary<int, float> waitDic;
    public static Dictionary<int, float> generateGapDic;

    public static float pentagon_call_time;
    public static float pentagon_swim_time;
    public static float pentagon_swim_stay_time;
    public static float pentagon_swim_speed;
    public static float speed_decay;
    public static float speed_range;
    public static float speed_mul;
    public static float max_speed;

    new private void Awake()
    {
        waitDic = new Dictionary<int, float>
        {
            {0,levelData_0.wait_time},
            {1,levelData_1.wait_time},
            {2,levelData_2.wait_time},
            {3,levelData_3.wait_time},
        };
        generateGapDic = new Dictionary<int, float>
        {
            {0,levelData_0.generate_gap_time},
            {1,levelData_1.generate_gap_time},
            {2,levelData_2.generate_gap_time},
            {3,levelData_3.generate_gap_time},
        };
        LevelDic = new Dictionary<int, Batch[]>
        {
            {0,levelData_0.batches },
            {1,levelData_1.batches},
            {2,levelData_2.batches},
            {3,levelData_3.batches},
        };
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
            pentagon_call_time = enemyData.pentagon_call_time;
            pentagon_swim_time = enemyData.pentagon_swim_time;
            pentagon_swim_stay_time = enemyData.pentagon_swim_stay_time;
            pentagon_swim_speed = enemyData.pentagon_swim_speed;
            speed_range = enemyData.rhombus_speed_range;
            speed_mul = enemyData.rhombus_speed_mul;
            max_speed = enemyData.max_speed;
        }
}