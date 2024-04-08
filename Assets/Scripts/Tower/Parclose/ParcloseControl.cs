using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcloseControl : TowerBase
{
    public int chargingState;
    public GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        shield = transform.Find("Shield").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SwitchChargingState(int num)
    {
        chargingState += num;
    }
}
