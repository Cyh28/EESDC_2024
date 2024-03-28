using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellTrailControl : MonoBehaviour
{
    TrailRenderer trailRenderer;
    public float height;
    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.widthCurve.AddKey(0,
                trailRenderer.widthCurve.keys[0].value * transform.localScale.x
        );
        height = trailRenderer.widthCurve.keys[0].value * transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
