using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ToggleMover : MonoBehaviour
{
    public GameObject ButtonToggle;
    //public RectTransform button;
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
//      void onEnable()
//      {
//         button.DOAnchorPos(new Vector2(-3, -(Screen.currentResolution.height * 0.15f)), 0.25f);
//      }

//      void onDisable()
//      {   
//         button.DOAnchorPos(new Vector2(-3, -(Screen.currentResolution.height * 0.4f)), 0.25f);
//      }
// }
