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
public interface IEnemy
{//敌人基类继承自此接口
    public void TakeDamage(int damage);//用于对敌人造成伤害
    public void Attack();
}
public interface IEnemyManager
{//敌人管理器继承自此接口
    public List<EnemyInfo> GetEnemyList();//获取所有已生成的敌人信息
}