using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonationControl : MonoBehaviour
{
    GameObject detonationExplode, entity;
    // Start is called before the first frame update
    void Start()
    {
        detonationExplode = transform.Find("ShockWave").gameObject;
        entity = transform.Find("Entity").gameObject;
        StartCoroutine(DelayTrigger());
    }
    IEnumerator DelayTrigger()
    {
        yield return new WaitForSeconds(ParaDefine.GetInstance().detonationData.explodeTime);
        entity.SetActive(false);
        TowerManager.GetInstance().RemoveTower(transform.position);
        Trigger();
    }
    void Trigger()
    {
        detonationExplode.SetActive(true);
        detonationExplode.GetComponent<DetonationExplodeControl>().Trigger();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
