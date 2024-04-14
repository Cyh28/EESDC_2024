using System.Buffers.Text;
using System.Collections;
using UnityEngine;

public class Hexagon : Enemy
{
    private EnemyManager manager;
    private float hexagon_call_time;
    new void Start()
    {
        base.Start();
        info.type = EnemyType.Hexagon;
        manager = EnemyManager.GetInstance();
        info.hp = Constant.HpDic[info.type];
        speed_rate = Constant.SpeedRateDic[info.type];
        max_speed = Constant.MaxSpeedDic[info.type];
        damage = Constant.DamageDic[info.type];
        score = Constant.ScoreDic[info.type];
        energy = Constant.EnergyDic[info.type];
        hexagon_call_time = Constant.hexagon_call_time;
        StartCoroutine(StartHatch());
    }
    IEnumerator StartHatch()
    {
        while (true)
        {
            yield return new WaitForSeconds(hexagon_call_time);
            manager.Hatch(rb.position, EnemyType.Dot);
        }
    }
    new void Update()
    {
        base.Update();
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
