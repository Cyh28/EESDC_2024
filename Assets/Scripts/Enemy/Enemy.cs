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
    public int decay_cnt;

    private BaseControl baseC;
    private ShieldControl shieldC;
    public int damage;
    public int damage_cnt;
    public int score;
    public int energy;

    public bool givenBirth = false;
    public bool attack_mode = false;
    public bool attack_base = false;
    public bool attack_sheild = false;
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = 128;
        baseC = BaseControl.GetInstance();
        decay_cnt = Constant.decay_cnt;
        damage_cnt = Constant.damage_cnt;
        max_speed = Constant.max_speed;
        ani = GetComponent<Animator>();
    }
    protected void Update()
    {
       attack_mode = attack_base || attack_sheild;
       Step2Place();
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
            if(rb.velocity.magnitude<max_speed)
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
        if (attack_base)
            baseC.DamageBase(damage);
        else if (attack_sheild)
            shieldC.ShieldTakeDamage(damage);
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Base"))
        {
            attack_base = true;
            rb.velocity *= 0f;
        }
        else if(other.CompareTag("Shield"))
        {
            shieldC = other.GetComponent<ShieldControl>();
            attack_sheild = true;
            rb.velocity *= 0f;
        }
    }
}
