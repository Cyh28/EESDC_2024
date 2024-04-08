using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Star : Enemy
{
    new void Start()
    {
        base.Start();
        info.type = EnemyType.Star;
        info.hp = Constant.HpDic[info.type];
        speed_rate = Constant.SpeedDic[info.type];
        damage = Constant.DamageDic[info.type];
        score = Constant.ScoreDic[info.type];
        energy = Constant.EnergyDic[info.type];
    }
    new void Update()
    {
        base.Update();
    }
    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
