using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldControl : MonoBehaviour
{
    public ShieldData shieldData;
    ParcloseControl parclose;
    [SerializeField]
    int currentDurability;
    [SerializeField]
    float chargingEnergy;
    [SerializeField]
    AnimationCurve formShield, destroyShield;
    // int current
    void Start()
    {
        currentDurability = 0;
        chargingEnergy = 100;
        transform.localScale = Vector3.zero;
        parclose = transform.parent.GetComponent<ParcloseControl>();
    }
    void Update()
    {
        // Debug.Log(currentDurability);
        if (chargingEnergy == 100)
        {
            if (currentDurability == 0)
            {
                FormShield();
            }
        }
        else
        {
            chargingEnergy += shieldData.chargeSpeed[parclose.chargingState] * Time.deltaTime;
            if (chargingEnergy > 100)
                chargingEnergy = 100;
        }
    }
    void FormShield()
    {
        StartCoroutine(IFormShield());
    }
    IEnumerator IFormShield()
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / shieldData.formTime;
            transform.localScale = Vector3.one * formShield.Evaluate(time) * shieldData.radius;
            yield return null;
        }
        currentDurability = shieldData.durability;
    }
    void DestroyShield()
    {
        chargingEnergy = 0;
        StartCoroutine(IDestroyShield());
    }
    IEnumerator IDestroyShield()
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / shieldData.formTime;
            transform.localScale = Vector3.one * destroyShield.Evaluate(time) * shieldData.radius;
            yield return null;
        }
    }
    public void ShieldTakeDamage(int damage)
    {
        Debug.Log("shield damaged");
        if (currentDurability > 0)
        {
            currentDurability -= damage;
            if (currentDurability <= 0)
            {
                currentDurability = 0;
                DestroyShield();
            }
        }
    }
}
