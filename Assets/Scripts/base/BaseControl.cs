using System.Collections;
using UnityEngine;

public class BaseControl : SingletonMono<BaseControl>, IBase
{
    public int maxHealth = 100;
    int health;
    int energy;
    int score;
    public int originEnergy;
    // Start is called before the first frame update
    // IEnumerator UpdatePerSecond()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     while (true)
    //     {
    //         AddEnergy(1);
    //         yield return new WaitForSeconds(1);
    //     }
    // }
    void Start()
    {
        health = maxHealth;
        energy = originEnergy;
        GamingUIControl.GetInstance().UpdateScore();
        GamingUIControl.GetInstance().UpdateEnergy();
        GamingUIControl.GetInstance().UpdateHealth();
        // StartCoroutine(UpdatePerSecond());
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Heath" + health);
        // Debug.Log("Energy" + energy);
        // Debug.Log("Score" + score);

    }

    public int GetHealth()
    {
        return health;
    }
    public int GetEnergy()
    {
        return energy;
    }
    public int GetScore()
    {
        return score;
    }
    public void AddScore(int _score)
    {
        score += _score;
        if (_score > 0)
            GamingUIControl.GetInstance().UpdateScore();
    }
    public void AddEnergy(int _energy)
    {
        energy += _energy;
        if (_energy > 0)
            GamingUIControl.GetInstance().UpdateEnergy();
    }
    public void DamageBase(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (damage > 0)
            GamingUIControl.GetInstance().UpdateHealth();
    }
}
