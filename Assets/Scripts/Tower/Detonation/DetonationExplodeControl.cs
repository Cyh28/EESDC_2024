using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetonationExplodeControl : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Trigger()
    {
        StartCoroutine(Explode());
    }
    IEnumerator Explode()
    {
        float timer = 0;
        while (timer <= ParaDefine.GetInstance().detonationData.explodeTime)
        {
            transform.localScale = ParaDefine.GetInstance().detonationData.explodeRadius * Vector3.one *
            ParaDefine.GetInstance().detonationData.animationCurve.Evaluate(timer / ParaDefine.GetInstance().detonationData.explodeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
        Destroy(transform.parent.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Enemy"))
        {
            collider2D.GetComponent<Enemy>().TakeDamage(ParaDefine.GetInstance().detonationData.damage);
        }
    }
}
