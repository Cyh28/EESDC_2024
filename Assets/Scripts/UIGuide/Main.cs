using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
public class Main : MonoBehaviour, IPointerUpHandler
{
    private bool isClicked = false;
    public static Main Self;

    public RectTransform CanvasRectTransform;
    public CanvasScaler CanvasScaler;
    public RectTransform ClickplaceTarget1;
    public RectTransform Target1, target1 = new RectTransform();
    //public RectTransform Target2;
    //public RectTransform Target3;

    public void Awake()
    {
        // target1 = new RectTransform();
        // target1.anchorMax = new Vector2(Target1.anchorMax.x, Target1.anchorMax.y * Target1.transform.parent.GetComponent<RectTransform>().anchorMax.y);
        // target1.anchorMin = new Vector2(Target1.anchorMin.x, Target1.anchorMin.y * Target1.transform.parent.GetComponent<RectTransform>().anchorMax.y);
        // Debug.Log(target1.anchorMax);
        // target1.anchoredPosition = Target1.anchoredPosition;
        // target1.anchoredPosition3D = Target1.anchoredPosition3D;
        // target1.offsetMax = Vector2.zero;
        // target1.offsetMin = Vector2.zero;
        // target1.pivot = Target1.pivot;
        // target1.sizeDelta = Target1.sizeDelta;
        // target1.localPosition = Target1.localPosition;
        // // Target1.up = Target1.
        Self = this;
        var guideMask = FindObjectOfType<GuideMask>();
        guideMask.Init();
        GuideMask.Self.Play(Target1);

    }


    /*public void OnGUI()
    {
        if (GUILayout.Button("=============  Close GuideMask  ============="))
        {
            GuideMask.Self.Close();
        }
        if (GUILayout.Button("=============  Target 1  ============="))
        {
            
        }
        if (GUILayout.Button("=============  Target 2  ============="))
        {
            GuideMask.Self.Play(Target2);
        }
        if (GUILayout.Button("=============  Target 3  ============="))
        {
            GuideMask.Self.Play(Target3);
        }
    }*/
    public void Button1OnClick()
    {
        if (!isClicked)
        {
            GuideMask.Self.Play(ClickplaceTarget1);
            Debug.Log("Button1OnClick");
            isClicked = true;
        }
    }
    //public void Button2OnClick()
    //{

    //}
    public void ClickPlaceOnClick()
    {
        GuideMask.Self.Close();
    }
    public void OnPointerUp(PointerEventData eventData)
    {

    }


    void update()
    {

    }
}
