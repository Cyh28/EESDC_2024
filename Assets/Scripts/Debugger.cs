using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : SingletonMono<Debugger>
{
    public GameObject shield;
    int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (cnt-- <= 0)
        // {
        //     cnt = 30;
        //     shield.GetComponent<ShieldControl>().ShieldTakeDamage(1);
        // }
    }
}
