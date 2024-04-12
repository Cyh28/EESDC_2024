using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Rendering;
using UnityEngine;

public class Pentagon : Enemy
{
    private EnemyManager manager;

    private bool rotate_mode = false;
    public float pentagon_call_time;
    private float rotate_direction = 0f;
    private float rotate_speed = 0f;

    private Vector2 attack_target;
    private float pentagon_fire_gap_time;
    private float pentagon_swim_time;

    private float left;
    private float right;
    private float up;
    private float down;
    System.Random random;

    new void Start()
    {
        base.Start();
        random = new System.Random(this.gameObject.GetInstanceID());
        info.type = EnemyType.Pentagon;
        info.hp = Constant.HpDic[info.type];
        speed_rate = Constant.SpeedRateDic[info.type];
        max_speed = Constant.MaxSpeedDic[info.type];
        damage = Constant.DamageDic[info.type];
        score = Constant.ScoreDic[info.type];
        energy = Constant.EnergyDic[info.type];
        manager = EnemyManager.GetInstance();
        Vector3 rightUp = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 leftDown = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        right = rightUp.x;
        left = leftDown.x;
        up = rightUp.y;
        down = leftDown.y;
        if (index == 6)
        {
            rotate_mode = true;
            rotate_speed = Constant.pentagon_rotate_speed;
            Debug.Log("penta:" + pentagon_call_time);
            pentagon_call_time = Constant.pentagon_call_time;
            if (UnityEngine.Random.value < 0.5f)
                rotate_direction = 1f;
            else
                rotate_direction = -1f;
            StartCoroutine(StartHatch());
        }
        else
        {
            pentagon_fire_gap_time = Constant.pentagon_fire_gap_time;
            pentagon_swim_time = Constant.pentagon_swim_time;
            rotate_mode = false;
            attack_target = Vector2.zero;
            StartCoroutine(FireEnemy());
            StartCoroutine(StartSwim());
        }
    }
    IEnumerator FireEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(pentagon_fire_gap_time);
            StartCoroutine(DisableCollidefor(0.8f));
            manager.HatchwithTarget(rb.position, EnemyType.Dot, attack_target);
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
                if ((rb.position - target).magnitude > 6f)
                    break;
            }
            SetTarget(target);
            yield return new WaitForSeconds(pentagon_swim_time);
        }
    }
    private void SearchforTower()
    {
        List<Vector2> towerList = TowerManager.GetInstance().towerPos;
        if (towerList.Count != 0)
        {
            towerList.Sort((x, y) =>
            {
                if ((x - rb.position).magnitude < (y - rb.position).magnitude)
                    return -1;
                else
                    return 1;
            });
            attack_target = towerList[0];
        }
    }
    IEnumerator StartHatch()
    {
        while (true)
        {
            yield return new WaitForSeconds(pentagon_call_time);
            StartCoroutine(DisableCollidefor(0.5f));
            manager.Hatch(rb.position, EnemyType.Dot);
        }
    }

    new void Update()
    {
        if (rotate_mode)
            transform.RotateAround(new Vector3(0f, 0f, 0f), Vector3.forward, rotate_speed * rotate_direction * Time.deltaTime);
        else
        {
            base.Update();
            SearchforTower();
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
    private float Range(float x, float y)
    {
        return (float)random.NextDouble() * (y - x) + x;
    }
}
