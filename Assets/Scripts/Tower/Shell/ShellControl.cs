using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellControl : MonoBehaviour
{
    public ShellData shellData;
    Animator animator;
    public int level;
    public float lifeTimeMax, destroyTimeMax;
    private float lifeTime = 0;
    public GameObject shell, shockWave;
    public Rigidbody2D rbShell;
    // Start is called before the first frame update
    void Start()
    {
        shell = transform.Find("Shell").gameObject;
        shockWave = transform.Find("ShockWave").gameObject;
        rbShell = GetComponent<Rigidbody2D>();
        animator = shell.GetComponent<Animator>();
    }
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > lifeTimeMax)
        {
            if (shockWave && !shockWave.activeInHierarchy)
            {
                Explode();
                StartCoroutine(DelayDestroy());
            }
        }
        if (lifeTime > destroyTimeMax)
            Destroy(gameObject);
    }
    void Explode()
    {
        rbShell.velocity = Vector3.zero;
        animator.SetTrigger("Disappear");
        shockWave.SetActive(true);
        shockWave.GetComponent<ShockWaveControl>().Explode();
    }
    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("damage?");
        if (collision.CompareTag("Enemy"))
        {
            // Debug.Log("damage!");
            collision.GetComponent<Enemy>().TakeDamage(shellData.damage[level]);
        }
    }
}
