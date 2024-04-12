using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelShaderControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LevelStart()
    {
        GameControl.GetInstance().levelStart = true;
        Destroy(gameObject);
    }
}
