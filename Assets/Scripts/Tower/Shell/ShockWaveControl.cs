using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveControl : MonoBehaviour
{
    public int level;
    public ShellData shellData;
    public void Explode()
    {
        StartCoroutine(ShockWaveStay());
    }
    IEnumerator ShockWaveStay()
    {
        yield return new WaitForSeconds(0.1f);
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
