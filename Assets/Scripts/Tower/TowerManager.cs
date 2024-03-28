using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TowerType
{
    None,
    Defender,
    Beacon,
    Projector,
    Parclose,
}
public class TowerManager : SingletonMono<TowerManager>
{
    public GameObject towerPa, defender, beacon, projector, parclose;
    GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        towerPa = GameObject.Find("tower");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateTower(TowerType towerType, Vector2 pos, Quaternion baseQuaternion, Quaternion? batQuaternion = null)
    {
        switch (towerType)
        {
            case TowerType.Defender:
                Instantiate(defender, pos, baseQuaternion, towerPa.transform);
                break;
            case TowerType.Projector:
                Instantiate(projector, pos, baseQuaternion, towerPa.transform);
                break;
            case TowerType.Parclose:
                Instantiate(parclose, pos, baseQuaternion, towerPa.transform);
                break;
            case TowerType.Beacon:
                obj = Instantiate(beacon, pos, baseQuaternion, towerPa.transform);
                obj.transform.Find("Battery").rotation = batQuaternion.Value;
                break;
        }
    }
}
