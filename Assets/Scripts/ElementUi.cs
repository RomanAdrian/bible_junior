﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Android;

public class ElementUi : MonoBehaviour, ISelectHandler, IDeselectHandler, IDragHandler, IPointerUpHandler
{
    public GameObject SubMenu;
    public bool draggable = true;
    public bool movesInBothDirections = true;

    private PanelOpener po; // variabila initializata
    private Zoom zoom;
    private bool TouchingCurrent;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private int count = 0;
    public GameObject ScrollArea;
    public Selectable btn;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        po = GetComponent<PanelOpener>(); 
        zoom = GetComponent<Zoom>();
        ScrollArea = GameObject.Find("ScrollableArea");
        btn = GetComponent<Selectable> ();
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            return;
        }

        SubMenu.SetActive(!SubMenu.activeSelf);
    }

     public void OnDrag(PointerEventData eventData)
     {    
            if (draggable && (Input.touchCount == 1 || count == 2))
            {
                if (movesInBothDirections)
                    rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
                else
                    rectTransform.anchoredPosition += new Vector2(0, eventData.delta.y) / canvas.scaleFactor;
                ColorBlock cb = btn.colors;
                cb.pressedColor = Color.cyan;
                btn.colors = cb;
            }
    }
    public void ZoomBig()
    {
        zoom.ZoomIn();
    }

    public void ZoomSmall()
    {
        zoom.ZoomOut();
    }

    public void Reflect()
    {
        //rectTransform.Rotate(new Vector3(0, 180, 0));
        Vector3 reference = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3( -reference.x , reference.y , reference.z );
    }

    public void OnSelect(BaseEventData eventData)
    {
        ScrollArea.SetActive(false);
        count = 1;
        Debug.Log("selected");
        GameObject[] subMenus = GameObject.FindGameObjectsWithTag("Submenu");

        foreach(GameObject subMenu in subMenus)
        {
            if(subMenu != this.SubMenu)
                subMenu.SetActive(false);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        ColorBlock cb = btn.colors;
        cb.pressedColor = Color.white;
        btn.colors = cb;
        count = 2;
        Debug.Log("unselected");
    }
}
