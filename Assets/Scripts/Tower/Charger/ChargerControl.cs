using System.Collections;
using UnityEngine;

public class ChargerControl : MonoBehaviour
{
    GameObject chargerLight, entity;
    // Start is called before the first frame update
    void Start()
    {
        chargerLight = transform.Find("ChargeLight").gameObject;
        entity = transform.Find("Entity").gameObject;
        StartCoroutine(DelayTrigger());
    }
    IEnumerator DelayTrigger()
    {
        yield return new WaitForSeconds(ParaDefine.GetInstance().chargerData.explodeDelay);
        entity.SetActive(false);
        Trigger();
    }
    void Trigger()
    {
        chargerLight.SetActive(true);
        chargerLight.GetComponent<ChargerLightControl>().Trigger();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
