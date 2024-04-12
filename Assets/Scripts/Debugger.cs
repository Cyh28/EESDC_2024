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
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameControl.GetInstance().LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameControl.GetInstance().ShowFailPanel();
        }
    }
}
