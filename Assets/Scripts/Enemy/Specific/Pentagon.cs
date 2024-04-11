using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Pentagon : Enemy
{
    private EnemyManager manager;
    private float pentagon_call_time;
    new void Start()
    {
        base.Start();
        info.type = EnemyType.Pentagon;
        info.hp = Constant.HpDic[info.type];
        speed_rate = Constant.SpeedDic[info.type];
        damage = Constant.DamageDic[info.type];
        score = Constant.ScoreDic[info.type];
        energy = Constant.EnergyDic[info.type];
        pentagon_call_time = Constant.pentagon_call_time;
        manager = EnemyManager.GetInstance();
        StartCoroutine(StartHatch());
    }
    new void Update()
    {
        base.Update();
    }
    IEnumerator StartHatch()
    {
        while (true)
        {
            yield return new WaitForSeconds(pentagon_call_time);
            manager.Hatch(rb.position, EnemyType.Dot);
        }
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
