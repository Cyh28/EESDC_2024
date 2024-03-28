using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : SingletonMono<PlayerControl>
{
    public bool holdingDefender, holdingBeacon, holdingProjector, holdingParclose;
    public bool setDirection1, setDirection2;
    public GameObject defender, beacon, projector, parclose;
    public GameObject holdingObject;
    public Quaternion quaternion1, quaternion2;
    public Vector2 dropPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

    }
    void GetInput()
    {
        if (holdingDefender)
        {
            if (Input.GetMouseButtonDown(0))
            {
                holdingDefender = false;
                Destroy(holdingObject);
                TowerManager.GetInstance().CreateTower(
                    TowerType.Defender,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    Quaternion.identity);
            }
            else
            {
                if (!holdingObject)
                {
                    holdingObject = Instantiate(defender);
                }
                holdingObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Input.GetMouseButtonDown(1))
                {
                    holdingDefender = false;
                    Destroy(holdingObject);
                }
            }
        }
        if (holdingProjector)
        {
            if (Input.GetMouseButtonDown(0))
            {
                holdingProjector = false;
                Destroy(holdingObject);
                TowerManager.GetInstance().CreateTower(
                    TowerType.Projector,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    Quaternion.identity);
            }
            else
            {
                if (!holdingObject)
                {
                    holdingObject = Instantiate(projector);
                }
                holdingObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Input.GetMouseButtonDown(1))
                {
                    holdingProjector = false;
                    Destroy(holdingObject);
                }
            }
        }
        if (holdingParclose)
        {
            if (Input.GetMouseButtonDown(0))
            {
                holdingParclose = false;
                Destroy(holdingObject);
                TowerManager.GetInstance().CreateTower(
                    TowerType.Parclose,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    Quaternion.identity);
            }
            else
            {
                if (!holdingObject)
                {
                    holdingObject = Instantiate(parclose);
                }
                holdingObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Input.GetMouseButtonDown(1))
                {
                    holdingParclose = false;
                    Destroy(holdingObject);
                }
            }
        }
        if (holdingBeacon)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (setDirection2)
                {
                    quaternion2 = Quaternion.AngleAxis(
                        Vector2.SignedAngle(Vector2.down, Camera.main.ScreenToWorldPoint(Input.mousePosition) - holdingObject.transform.position), Vector3.forward);
                    // Debug.Log(quaternion2.eulerAngles);
                    holdingObject.transform.Find("Battery").transform.rotation = quaternion2;
                    setDirection2 = false;
                    TowerManager.GetInstance().CreateTower(TowerType.Beacon, dropPos, quaternion1, quaternion2);
                    holdingBeacon = false;
                    Destroy(holdingObject);
                }
                else
                {
                    if (setDirection1)
                    {
                        quaternion1 = Quaternion.AngleAxis(
                                                    Vector2.SignedAngle(Vector2.up, Camera.main.ScreenToWorldPoint(Input.mousePosition) - holdingObject.transform.position), Vector3.forward);
                        holdingObject.transform.rotation = quaternion1;
                        setDirection1 = false;
                        setDirection2 = true;
                    }
                    else
                    {
                        setDirection1 = true;
                        dropPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    }
                }
            }
            else
            {
                if (!holdingObject)
                {
                    holdingObject = Instantiate(beacon);
                }
                if (setDirection1)
                {
                    quaternion1 = Quaternion.AngleAxis(
                                                Vector2.SignedAngle(Vector2.up, Camera.main.ScreenToWorldPoint(Input.mousePosition) - holdingObject.transform.position), Vector3.forward);
                    holdingObject.transform.rotation = quaternion1;
                }
                else
                {
                    if (setDirection2)
                    {
                        quaternion2 = Quaternion.AngleAxis(
                            Vector2.SignedAngle(Vector2.down, Camera.main.ScreenToWorldPoint(Input.mousePosition) - holdingObject.transform.position), Vector3.forward);
                        // Debug.Log(quaternion2.eulerAngles);
                        holdingObject.transform.Find("Battery").transform.rotation = quaternion2;
                    }
                    else
                    {
                        holdingObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    }

                }
                if (Input.GetMouseButtonDown(1))
                {
                    holdingBeacon = false;
                    Destroy(holdingObject);
                }
            }
        }
    }
}
