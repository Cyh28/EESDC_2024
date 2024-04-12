using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public int health;
    public TowerData towerData;
    public void Start()
    {
        health = towerData.health;
    }
    public void DamageTower(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            TowerManager.GetInstance().RemoveTower(transform.position);
            Destroy(gameObject);
        }
    }
}
