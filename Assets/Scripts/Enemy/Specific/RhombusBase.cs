using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhombusBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SelfDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
    public void ReSetVelocity()
    {
        transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    public void Dead()
    {
        transform.parent.GetComponent<Rhombus>().isDead = true;
        GetComponent<Collider2D>().enabled = false;
    }
}
