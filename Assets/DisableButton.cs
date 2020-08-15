using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    public Button startButton;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startButton.interactable = !startButton.interactable;
        }
        
    }
}
