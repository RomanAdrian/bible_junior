using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Selected : MonoBehaviour, IPointerUpHandler
{
    public Button btn;
    public Color newColor;
    private bool selected = false;

    void Start()
    {
        btn = GetComponent<Button> ();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            return;
        }

        selected = !selected;

        if (selected)
        {
            ColorBlock cb = btn.colors;
            cb.selectedColor = newColor;
            cb.highlightedColor = newColor;
            cb.pressedColor = newColor;
            cb.disabledColor = newColor;
            cb.normalColor = newColor;
            btn.colors = cb;
        }
        else
        {
            ColorBlock cb = btn.colors;
            cb.normalColor = Color.white;
            cb.highlightedColor = Color.white;
            cb.selectedColor = Color.white;
            cb.selectedColor = Color.white;
            cb.pressedColor = Color.white;
            cb.disabledColor = Color.white;
            btn.colors = cb;
        }

    }
}
