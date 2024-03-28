using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    public LaserData laserData;
    public int level;
    private float lifeTime = 0;
    Animator animator;
    Collider2D collideBox;
    Transform trail;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collideBox = GetComponent<Collider2D>();
        trail = transform.Find("Trail");
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > laserData.lifeTime)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(laserData.damage[level]);
            collideBox.enabled = false;
            animator.SetTrigger("Disappear");
            trail.gameObject.SetActive(false);
        }
    }
    public void Disappear()
    {
        Destroy(gameObject);
    }
}
