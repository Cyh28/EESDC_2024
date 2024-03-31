using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonationControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayExplode());
    }
    IEnumerator DelayExplode()
    {
        yield return new WaitForSeconds(1);
        Explode();
    }
    void Explode()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
