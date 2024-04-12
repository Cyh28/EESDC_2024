using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
[Serializable]
public struct EnemyInfo
{
    public Vector2 pos;
    public Vector2 vel;
    public int hp;
    public EnemyType type;
}

[Serializable]
public struct Batch
{
    public int triangle_num;
    public int square_num;
    public int circle_num;
    public int dot_num;
    public int swim_pentagon_num;
    public int rotate_pentagon_num;
    public int hexagon_num;
    public int rhombus_num;
    public int star_num;
    public bool is_continuous;
    public float exist_time;
    public int Sum()
    { return triangle_num+square_num+circle_num+dot_num+swim_pentagon_num+rotate_pentagon_num+hexagon_num+rhombus_num+star_num; }
    public int RandomChoose()
    {
        int random = UnityEngine.Random.Range(0, 9);
        switch(random)
        {
            case 0:
                if (triangle_num >= 1)
                {
                    triangle_num--;
                    return 0;
                }
                return -1;
            case 1:
                if(square_num>=1)
                {
                    square_num--;
                    return 2;
                }
                return -1;
            case 2:
                if (circle_num >= 1)
                {
                    circle_num--;
                    return 1;
                }
                return -1;
            case 3:
                if (dot_num >= 1)
                {
                    dot_num--;
                    return 8;
                }
                return -1;
            case 4:
                if (swim_pentagon_num >= 1)
                {
                    swim_pentagon_num--;
                    return 5;
                }
                return -1;
            case 5:
                if (rotate_pentagon_num >= 1)
                {
                    rotate_pentagon_num--;
                    return 6;
                }
                return -1;
            case 6:
                if (hexagon_num >= 1)
                {
                    hexagon_num--;
                    return 3;
                }
                return -1;
            case 7:
                if (rhombus_num >= 1)
                {
                    rhombus_num--;
                    return 4;
                }
                return -1;
            case 8:
                if (star_num >= 1)
                {
                    star_num--;
                    return 7;
                }
                return -1;
            default:
                return -1;
        }
    }
    public int RandomSample()
    {
        int index;
        while(true)
        {
            index = RandomChoose();
            if (index != -1)
                break;
        }
        return index;
    }
};

public interface IEnemy
{//敌人基类继承自此接口
    public void TakeDamage(int damage);//用于对敌人造成伤害
    public void Attack();
}
public interface IEnemyManager
{//敌人管理器继承自此接口
    public List<EnemyInfo> GetEnemyList();//获取所有已生成的敌人信息
}