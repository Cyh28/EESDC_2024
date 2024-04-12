using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Rendering;
using UnityEngine;

public class Pentagon : Enemy
{
    private EnemyManager manager;
    private float pentagon_call_time;
    private float pentagon_swim_time;
    private float pentagon_swim_stay_time;
    private float pentagon_swim_speed;
    private bool rotate_mode=false;
    private float rotate_direction = 0f;
    private float left;
    private float right;
    private float up;
    private float down;
    System.Random random;

    new void Start()
    {
        base.Start();
        random=new System.Random(this.gameObject.GetInstanceID());
        info.type = EnemyType.Pentagon;
        info.hp = Constant.HpDic[info.type];
        speed_rate = Constant.SpeedDic[info.type];
        damage = Constant.DamageDic[info.type];
        score = Constant.ScoreDic[info.type];
        energy = Constant.EnergyDic[info.type];
        manager = EnemyManager.GetInstance();
        pentagon_call_time = Constant.pentagon_call_time;
        Vector3 rightUp = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 leftDown = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        right = rightUp.x;
        left = leftDown.x;
        up = rightUp.y;
        down = leftDown.y;
        if (index == 6)
        {
            rotate_mode = true;
            if (UnityEngine.Random.value<0.5f)
                rotate_direction = 1f;
            else
                rotate_direction = -1f;
        }
        else
        {
            pentagon_swim_speed = Constant.pentagon_swim_speed;
            pentagon_swim_time = Constant.pentagon_swim_time;
            pentagon_swim_stay_time = Constant.pentagon_swim_stay_time;
            rotate_mode = false;
            StartCoroutine(StartSwim());
        }
        StartCoroutine(StartHatch());
    }
    new void Update()
    {
        if(rotate_mode)
            transform.RotateAround(new Vector3(0f, 0f, 0f), Vector3.forward, speed_rate *rotate_direction* Time.deltaTime);
    }
    IEnumerator StartHatch()
    {
        while (true)
        {
            yield return new WaitForSeconds(pentagon_call_time);
            manager.Hatch(rb.position, EnemyType.Dot);
        }
    }
    IEnumerator StartSwim()
    {
        float ratio = 0.8f;
        Vector2 target;
        while (true)
        {
            while (true)
            {
                target = new Vector2(Range(left * ratio, right * ratio), Range(down * ratio, up * ratio));
                if ((rb.position - target).magnitude > 5f)
                    break;
            }
            rb.velocity = (target - rb.position).magnitude / pentagon_swim_time * (target - rb.position).normalized;
            yield return new WaitForSeconds(pentagon_swim_time);
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
    private float Range(float x,float y)
    {
        return (float)random.NextDouble() * (y - x) + x;
    }
}
