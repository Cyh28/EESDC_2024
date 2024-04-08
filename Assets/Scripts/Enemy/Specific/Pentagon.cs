using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Pentagon : Enemy
{
    private EnemyManager manager;
    private int pentagon_call_cnt;
    new void Start()
    {
        base.Start();
        info.type = EnemyType.Pentagon;
        info.hp = Constant.HpDic[info.type];
        speed_rate = Constant.SpeedDic[info.type];
        damage = Constant.DamageDic[info.type];
        score = Constant.ScoreDic[info.type];
        energy = Constant.EnergyDic[info.type];
        pentagon_call_cnt = Constant.pentagon_call_cnt;
        manager = EnemyManager.GetInstance();
    }
    new void Update()
    {
        base.Update();
        pentagon_call_cnt--;
        if (pentagon_call_cnt == 0)
        {
            manager.Hatch(rb.position, EnemyType.Dot);
            pentagon_call_cnt = Constant.pentagon_call_cnt;
        }
    }
    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
