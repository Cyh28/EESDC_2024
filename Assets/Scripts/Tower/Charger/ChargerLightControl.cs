using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChargerLightControl : MonoBehaviour
{
    public Light2D light2D;
    public CircleCollider2D circleCollider2D;
    public float currentRadius;
    public void Trigger()
    {

        light2D = GetComponent<Light2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        StartCoroutine(Explode());
    }
    IEnumerator Explode()
    {
        float timer = 0;
        while (timer <= ParaDefine.GetInstance().chargerData.explodeTime)
        {
            currentRadius = ParaDefine.GetInstance().chargerData.lightRadius *
                ParaDefine.GetInstance().chargerData.animationCurve.Evaluate(timer / ParaDefine.GetInstance().chargerData.explodeTime);
            light2D.pointLightOuterRadius = circleCollider2D.radius = currentRadius;
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(ParaDefine.GetInstance().chargerData.lightLastTime);
        // Destroy(gameObject);
        Destroy(transform.parent.gameObject);
    }
}
