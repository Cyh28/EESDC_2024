using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerLightControl : MonoBehaviour
{

    public void Trigger()
    {
        StartCoroutine(Explode());
    }
    IEnumerator Explode()
    {
        float timer = 0;
        while (timer <= ParaDefine.GetInstance().chargerData.lightLastTime)
        {
            transform.localScale = ParaDefine.GetInstance().chargerData.lightRadius * Vector3.one *
            ParaDefine.GetInstance().chargerData.animationCurve.Evaluate(timer / ParaDefine.GetInstance().chargerData.lightLastTime);
            timer += Time.deltaTime;
            yield return null;
        }
        // Destroy(gameObject);
        // Destroy(transform.parent.gameObject);
    }
}
