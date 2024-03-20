﻿using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Hexagon : Enemy
{
    public bool given_birth;
    Hexagon()
    {
        info.type=EnemyType.Hexagon;
        info.hp = Constant.HpDic[info.type];
        speed_rate = Constant.SpeedDic[info.type];
        given_birth = false;
    }
    private void Update()
    {
        Step2Place();
        if(!given_birth&info.hp< 0.5*Constant.HpDic[info.type])
            EnemyManager.GetInstance().Hatch()
        
    }
}
