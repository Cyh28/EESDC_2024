﻿using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Circle : Enemy
{
    new void Start()
    {
        base.Start();
        info.type = EnemyType.Circle;
        info.hp = Constant.HpDic[info.type];
        speed = Constant.SpeedDic[info.type];
        damage = Constant.DamageDic[info.type];
        score = Constant.ScoreDic[info.type];
        energy = Constant.EnergyDic[info.type];
    }
    private void Update()
    {
        Step2Place();
    }
}
