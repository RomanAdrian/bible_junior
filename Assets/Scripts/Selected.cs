using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Selected : MonoBehaviour
{
    public Button btn;
    public Color newColor;
    private bool s = false;

    void Start()
    {
        //btn = gameObject.GetComponent<Button> ();
        //btn.onClick.AddListener(TaskOnClick);
    }

    //     if (btn != null || btn.SetActive(true))
    //     {
    //         ColorBlock cb = btn.colors;
    //         cb.selectedColor = newColor;
    //         cb.pressedColor = newColor;
    //         btn.colors = cb;
    //     }
    //  }

    public void TaskOnClick()
    {
        if (s == true || s == false)
        {
            ColorBlock cb = btn.colors;
            cb.selectedColor = newColor;
            cb.normalColor = newColor;
            cb.highlightedColor = newColor;
            cb.pressedColor = newColor;
            btn.colors = cb;
        }
    }
 
    void OnSelect(BaseEventData eventData)
    {
        s = true;
    }

    void OnDeselect(BaseEventData eventData)
    {
        s = false;
    }
}
