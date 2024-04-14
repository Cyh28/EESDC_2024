using System.Collections;
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

    public BaseControl baseC;
    private ShieldControl shieldC;
    private TowerBase towerC;
    public int damage;
    public int score;
    public int energy;
    public bool isDead = false;
    private bool niubi = true;
    private bool attack_mode = false;
    private bool attack_base = false;
    private bool attack_sheild = false;
    private bool attack_tower = false;
    private float attackCDTimer;
    private float attackCD;

    public bool givenBirth = false;
    public int index;
    public bool is_hatched = false;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = 128;
        baseC = BaseControl.GetInstance();
        ani = GetComponent<Animator>();
        attackCD = 1f;
        if (index == 5)
            ani.SetTrigger("Birth");
        StartCoroutine(DisableCollidefor(0.1f));
        StartCoroutine(Protectfor(0.2f));
    }
    protected void Update()
    {
        if (!isDead)
        {
            Step2Place();
            Attack();
        }
    }
    public IEnumerator DisableCollidefor(float time)
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(time);
        GetComponent<Collider2D>().enabled = true;
    }
    public IEnumerator Protectfor(float time)
    {
        yield return new WaitForSeconds(time);
        niubi = false;
    }
    public void Step2Place()
    {
        if (!attack_mode)
        {
            Vector2 r = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
            if ((rb.velocity.normalized - r.normalized).magnitude > 0.1f || rb.velocity.magnitude < max_speed)
                rb.AddForce(r.normalized * speed_rate);
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
        if (!niubi)
            info.hp -= damage;
        if (info.hp <= 0)
        {
            if (info.type == EnemyType.Rhombus)
                transform.Find("Base").GetComponent<Animator>().SetTrigger("Die");
            else
                ani.SetTrigger("Die");
            return;
        }
        if (info.type == EnemyType.Rhombus)
            transform.Find("Base").GetComponent<Animator>().SetTrigger("Injured");
        else
            ani.SetTrigger("Injured");
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
                Debug.Log("attack tower");
                towerC.DamageTower(damage);
                attackCDTimer = attackCD;
            }
        }
        attack_mode = attack_sheild = attack_base = attack_tower = false;
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Tower"))
        {
            DisableCollidefor(0.4f);
            target *= 0;
            towerC = other.transform.parent.GetComponent<TowerBase>();
            attack_tower = true;
        }
        else if (other.collider.CompareTag("Base"))
        {
            DisableCollidefor(1f);
            attack_base = true;
        }
        else if (other.collider.CompareTag("Shield"))
        {
            shieldC = other.collider.GetComponent<ShieldControl>();
            attack_sheild = true;
        }
        attack_mode = attack_base || attack_sheild || attack_tower;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpeedField"))
        {
            rb.velocity *= Constant.speed_mul;
            max_speed *= 2;
        }
    }
}
