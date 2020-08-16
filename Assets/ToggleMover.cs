using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ToggleMover : MonoBehaviour
{
    public GameObject ButtonToggle;
    public bool bt = false;
    public Vector3 upPosition;
    public Vector3 downPosition;
    
    
    public void ToggleButton()
    {
        if (ButtonToggle != null)
        {
            bool isActive = ButtonToggle.activeSelf;
            ButtonToggle.SetActive(!isActive);
            bt = true;
        }
        
        if (bt == true)
        {
            transform.DOMoveY(-1, 1);
        }
    }

    public void moveToggle()
    {
        Toggle toggle = ButtonToggle.GetComponent<Toggle>();
        RectTransform rt = GetComponent<RectTransform>();
        if(toggle.isOn)
        {
            rt.anchoredPosition = upPosition;
        } else 
        {
            rt.anchoredPosition = downPosition;
        }
    }
}
