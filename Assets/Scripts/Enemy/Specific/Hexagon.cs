using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Hexagon : Enemy
{
    private EnemyManager manager;
    new void Start()
    {
        base.Start();
        manager=EnemyManager.GetInstance();
        info.type = EnemyType.Hexagon;
        info.hp = Constant.HpDic[info.type];
        speed_rate = Constant.SpeedDic[info.type];
        damage = Constant.DamageDic[info.type];
        score = Constant.ScoreDic[info.type];
        energy = Constant.EnergyDic[info.type];
        //避免和塔碰撞
        StartCoroutine(Fixed());
    }
    IEnumerator Fixed()
    {
        yield return new WaitForSeconds(1f);
        rb.velocity *= 0;
    }
    new void Update()
    {
        
    }
    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
    public void ReSetVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    public void Dead()
    {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
    }
}
