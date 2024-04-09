using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Lean.Transition.Method;
[DefaultExecutionOrder(-5)]
public class ButtonArrow : MonoBehaviour
{
    [HideInInspector]
    public bool thisFrame = false;
    [HideInInspector]
    public bool isPressed = false;
    private Image image;
    private bool trigger = false;
    private bool trigger2 = false;
    public bool isUp = false;
    private float t;

    public LeanGraphicColor colorEnter, colorExit;
   /* public void OnPointerDown(PointerEventData eventData)
    {
        thisFrame = isPressed = trigger = true;
        t = Time.time;
        image.CrossFadeColor(Color.grey, 0.1f, true, false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        thisFrame = isPressed = trigger = true;
        image.CrossFadeColor(Color.grey, 0.1f, true, false);
    }*/
    public void OnEnter()
    {
        thisFrame = isPressed = trigger = true;
    }
    public void OnExit()
    {
        thisFrame = isPressed = trigger = false;
        isUp = trigger2 = true;
    }

    /*public void OnPointerUp(PointerEventData eventData)
    {
        thisFrame = isPressed = trigger = false;
        isUp = trigger2 = true;
        Debug.Log((Time.time - t) * 1000);
        image.CrossFadeColor(Color.white, 0.1f, true, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        thisFrame = isPressed = trigger = false;
        isUp = trigger2 = true;
        image.CrossFadeColor(Color.white, 0.1f, true, false);
    }*/

    private void Start()
    {
        image = GetComponent<Image>();
        // method to change color
    }
    private void Update()
    {
        if (trigger)
        {
            trigger = false;
        }
        else
        {
            thisFrame = false;
        }

        if (trigger2)
        {
            trigger2 = false;
        }
        else
        {
            isUp = false;
        }
    }

    
}
