using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Android;

public class ElementUi : MonoBehaviour, ISelectHandler, IDeselectHandler, IDragHandler, IPointerUpHandler
{
    public GameObject SubMenu;

    private PanelOpener po; // initializat
    private Zoom zoom;
    private bool TouchingCurrent;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private int count = 0;
    public GameObject ScrollArea;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        po = GetComponent<PanelOpener>(); 
        zoom = GetComponent<Zoom>();
        ScrollArea = GameObject.Find("ScrollableArea");
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
            if (Input.touchCount == 1 || count == 2)
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
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
        count = 2;
        Debug.Log("unselected");
    }
}
