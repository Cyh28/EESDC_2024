using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public Rigidbody2D rb;
    public Animator ani;
    [SerializeField]
    public EnemyInfo info;

    public Vector2 target;
    public float speed_rate;
    public float max_speed;

    private BaseControl baseC;
    private ShieldControl shieldC;
    private TowerBase towerC;
    public int damage;
    public int score;
    public int energy;

    public bool givenBirth = false;
    public bool attack_mode = false;
    public bool attack_base = false;
    public bool attack_sheild = false;
    public bool attack_tower = false;
    public float attackCDTimer;
    public float attackCD;
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = 128;
        baseC = BaseControl.GetInstance();
        max_speed = Constant.max_speed;
        ani = GetComponent<Animator>();
        attackCD = 1f;
    }
    protected void Update()
    {
        attack_mode = attack_base || attack_sheild || attack_tower;
        Step2Place();
        Attack();
        attack_sheild = attack_base = attack_tower=false;
    }
    public void Step2Place()
    {
        if (!attack_mode)
        {
            Vector2 r = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
            float norm = r.magnitude;
            if (rb.position.magnitude > 5f | norm < 0.5f)
            {
                SetTarget(new Vector2(0, 0));
            }
            if (rb.velocity.magnitude < max_speed)
                rb.AddForce(r / norm * speed_rate);
        }
    }
    public void SetTarget(Vector2 place)
    {
        target = place;
    }
    public void UpdateInfo()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        info.vel = rb.velocity;
        info.pos = rb.position;
    }
    public void TakeDamage(int damage)
    {
        ani.SetTrigger("Injured");
        info.hp -= damage;
    }
    public void Pushed(Vector2 direction, float val)
    {
        rb.AddForce(direction * val);
        rb.velocity += direction * val;
    }
    public void Attack()
    {
        if (attackCDTimer > 0)
        {
            attackCDTimer -= Time.deltaTime;
            if (attackCDTimer < 0)
                attackCDTimer = 0;
        }
        if (attackCDTimer == 0)
        {
            if (attack_base)
            {
                baseC.DamageBase(damage);
                attackCDTimer = attackCD;
            }
            else if (attack_sheild && shieldC)
            {
                shieldC.ShieldTakeDamage(damage);
                attackCDTimer = attackCD;
            }
            else if (attack_tower && towerC)
            {
                towerC.DamageTower(damage);
                attackCDTimer = attackCD;
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Tower"))
        {
            Debug.Log("tower get");
            towerC = other.collider.GetComponent<TowerBase>();
            attack_tower = true;
        }
        else if (other.collider.CompareTag("Base"))
        {
            attack_base = true;
        }
        else if (other.collider.CompareTag("Shield"))
        {
            shieldC = other.collider.GetComponent<ShieldControl>();
            Debug.Log("shield get");
            attack_sheild = true;
        }
    }
}
