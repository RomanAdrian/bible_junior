using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    public GameObject ButtonToggle;
    

    public void ToggleButton()
    {
        if (ButtonToggle != null)
        {
            bool isActive = ButtonToggle.activeSelf;
            ButtonToggle.SetActive(!isActive);
        }
    }
}
