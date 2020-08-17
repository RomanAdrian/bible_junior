using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ToggleMover : MonoBehaviour
{
    public GameObject ButtonToggle;
    //public bool bt = false;
    
    
    public void ToggleButton()
    {
        if (ButtonToggle != null)
        {
            bool isActive = ButtonToggle.activeSelf;
            ButtonToggle.SetActive(!isActive);
            //bt = true;
        }
     }
}
