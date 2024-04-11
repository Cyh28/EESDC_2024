using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Circle : Enemy
{
    private EnemyManager manager;
    new void Start()
    {
        base.Start();
        info.type = EnemyType.Circle;
        info.hp = Constant.HpDic[info.type];
        speed_rate = Constant.SpeedDic[info.type];
        damage = Constant.DamageDic[info.type];
        score = Constant.ScoreDic[info.type];
        energy = Constant.EnergyDic[info.type];
        manager = EnemyManager.GetInstance();
    }
    new void Update()
    {
        base.Update();
    }
    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
    public void Dead()
    {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
    }
    public void ReSetVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    public void CallDot()
    {
        manager.Hatch(rb.position, EnemyType.Dot);
    }
}
