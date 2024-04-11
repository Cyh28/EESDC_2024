using System;
using System.Collections;
using System.Collections.Generic;
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
    public int pentagon_num;
    public int hexagon_num;
    public int rhombus_num;
    public int star_num;
    public float gap_time;
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