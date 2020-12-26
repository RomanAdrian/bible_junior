 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Android;

public class ElementUi : MonoBehaviour, ISelectHandler, IDeselectHandler, IDragHandler
{

    public bool draggable = true;
    public bool movesInBothDirections = true;

    private PanelOpener po; // variabila initializata
    private Zoom zoom;
    private bool TouchingCurrent;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    public int count = 0;
    public GameObject ScrollArea;
    public Selectable btn;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        po = GetComponent<PanelOpener>();
        zoom = GetComponent<Zoom>();
        ScrollArea = GameObject.Find("Chat Side-Menu");
        btn = GetComponent<Selectable>();

        if (movesInBothDirections == true) movesInBothDirections = GetComponent<StoryElement>().SubmenuType == "Submenu";
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            return;
        }
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
    //public void ZoomBig()
    //{
    //    zoom.ZoomIn();
    //}

    //public void ZoomSmall()
    //{
    //    zoom.ZoomOut();
    //}

    public void Reflect()
    {
        //rectTransform.Rotate(new Vector3(0, 180, 0));
        Vector3 reference = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3( -reference.x , reference.y , reference.z );
    }

    public void OnSelect(BaseEventData eventData)
    {
        ScrollArea.GetComponent<DanielLochner.Assets.SimpleSideMenu.SimpleSideMenu>().Close();
        count = 1;
        
       // SavingSystem.changesMade = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        ColorBlock cb = btn.colors;
        cb.pressedColor = Color.white;
        btn.colors = cb;
        count = 2;
    }
}
