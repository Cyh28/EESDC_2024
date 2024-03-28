using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamingUIControl : SingletonMono<GamingUIControl>
{
    public Slider healthBar;
    public TextMeshProUGUI healthText, energyText, scoreText;
    public AnimationCurve animationCurve;
    public float animationTime;
    public GameObject energyIcon;
    bool energyIconPlaying;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.Find("BaseHealth").GetComponent<Slider>();
        healthText = transform.Find("BaseHealth").Find("BaseHealthText").GetComponent<TextMeshProUGUI>();
        energyIcon = transform.Find("Energy").Find("EnergyIcon").gameObject;
        energyText = transform.Find("Energy").Find("EnergyText").GetComponent<TextMeshProUGUI>();
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateHealth()
    {
        healthBar.value = 1.0f * BaseControl.GetInstance().GetHealth() / BaseControl.GetInstance().maxHealth;
        healthText.text = BaseControl.GetInstance().GetHealth().ToString();
    }
    public void UpdateEnergy()
    {
        if (!energyIconPlaying)
            StartCoroutine(EnergyAnim(energyIcon, animationCurve));
        energyText.text = BaseControl.GetInstance().GetEnergy().ToString();
    }
    IEnumerator EnergyAnim(GameObject animGameObject, AnimationCurve animationCurve)
    {
        energyIconPlaying = true;
        float timer = 0;
        Vector3 BasicScale = animGameObject.transform.localScale;
        while (timer <= animationTime)
        {
            animGameObject.transform.localScale = animationCurve.Evaluate(timer / animationTime) * BasicScale;
            // Debug.Log("time:" + timer + "  value:" + animationCurve.Evaluate(timer / animationTime) * BasicScale);
            timer += Time.deltaTime;
            yield return 0;
        }
        energyIconPlaying = false;
    }
    public void UpdateScore()
    {
        scoreText.text = "Score: " + BaseControl.GetInstance().GetScore();
    }
    public void DefenderButtonDown()
    {
        if (BaseControl.GetInstance().GetEnergy() > ParaDefine.GetInstance().defenderData.cost)
            PlayerControl.GetInstance().holdingDefender = true;
    }
    public void BeaconButtonDown()
    {
        if (BaseControl.GetInstance().GetEnergy() > ParaDefine.GetInstance().beaconData.cost)
            PlayerControl.GetInstance().holdingBeacon = true;
    }
    public void ProjectorButtonDown()
    {
        if (BaseControl.GetInstance().GetEnergy() > ParaDefine.GetInstance().projectorData.cost)
            PlayerControl.GetInstance().holdingProjector = true;
    }
    public void ParcloseButtonDown()
    {
        if (BaseControl.GetInstance().GetEnergy() > ParaDefine.GetInstance().parcloseData.cost)
            PlayerControl.GetInstance().holdingParclose = true;
    }
}
