using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Exti : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }
    public void Exit()
    {
        SceneManager.LoadScene("MainUI");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
