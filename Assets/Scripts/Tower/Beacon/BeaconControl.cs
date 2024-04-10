using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconControl : TowerBase
{
    public int chargingState;
    public GameObject lightSource;
    public bool lightEnabled = true;
    public void SwitchChargingState(int num)
    {
        chargingState += num;
    }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        lightSource = transform.Find("Battery").GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (chargingState == 0)
        {
            lightSource.SetActive(false);
        }
        else
        {
            lightSource.SetActive(true);
        }
    }
}
