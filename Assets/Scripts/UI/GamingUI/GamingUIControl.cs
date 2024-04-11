using System;
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
    Dictionary<TowerType, Tuple<TextMeshProUGUI, TextMeshProUGUI>> towerText = new Dictionary<TowerType, Tuple<TextMeshProUGUI, TextMeshProUGUI>>();
    bool energyIconPlaying;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.Find("BaseHealth").GetComponent<Slider>();
        healthText = transform.Find("BaseHealth").Find("BaseHealthText").GetComponent<TextMeshProUGUI>();
        energyIcon = transform.Find("Energy").Find("EnergyIcon").gameObject;
        energyText = transform.Find("Energy").Find("EnergyText").GetComponent<TextMeshProUGUI>();
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        foreach (TowerType towerType in Enum.GetValues(typeof(TowerType)))
        {
            Debug.Log(towerType.ToString() + "Button");
            if (towerType == TowerType.None)
                continue;
            towerText.Add(towerType, new Tuple<TextMeshProUGUI, TextMeshProUGUI>(
                transform.Find("SideBar").Find(towerType.ToString() + "Button").Find("TowerName").GetComponent<TextMeshProUGUI>(),
                transform.Find("SideBar").Find(towerType.ToString() + "Button").Find("TowerCost").GetComponent<TextMeshProUGUI>()));
        }
        if (ParaDefine.GetInstance().towerData.Count == 0)
        {
            ParaDefine.GetInstance().InitializeTowerData();
        }
        foreach (TowerType towerType in Enum.GetValues(typeof(TowerType)))
        {
            if (towerType == TowerType.None)
                continue;
            Debug.Log(ParaDefine.GetInstance().towerData.Count);
        }
        foreach (TowerType towerType in Enum.GetValues(typeof(TowerType)))
        {
            if (towerType == TowerType.None)
                continue;
            towerText[towerType].Item1.text = towerType.ToString();
            towerText[towerType].Item2.text = ParaDefine.GetInstance().towerData[towerType].cost.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateHealth()
    {
        if (!healthBar)
            healthBar = transform.Find("BaseHealth").GetComponent<Slider>();
        if (!healthText)
            healthText = transform.Find("BaseHealth").Find("BaseHealthText").GetComponent<TextMeshProUGUI>();
        healthBar.value = 1.0f * BaseControl.GetInstance().GetHealth() / BaseControl.GetInstance().maxHealth;
        healthText.text = BaseControl.GetInstance().GetHealth().ToString();
    }
    public void UpdateEnergy()
    {
        if (!energyText)
            energyText = transform.Find("Energy").Find("EnergyText").GetComponent<TextMeshProUGUI>();
        if (!energyIcon)
            energyIcon = transform.Find("Energy").Find("EnergyIcon").gameObject;
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
        if (!scoreText)
            scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
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
    public void DetonationButtonDown()
    {
        if (BaseControl.GetInstance().GetEnergy() > ParaDefine.GetInstance().detonationData.cost)
            PlayerControl.GetInstance().holdingDetonation = true;
    }
    public void ChargerButtonDown()
    {
        if (BaseControl.GetInstance().GetEnergy() > ParaDefine.GetInstance().chargerData.cost)
            PlayerControl.GetInstance().holdingCharger = true;
    }
}
