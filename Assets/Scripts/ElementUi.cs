using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Android;
using UnityEngine.iOS;

public class ElementUi : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private GameObject SubMenu;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private PanelOpener po; // initializat
    private ZoomIn zoom_plus;
    private ZoomOut zoom_minus;
    private float TouchTime;
    private bool DragAndDrop = false;
    private bool TouchingCurrent;



    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        po = GetComponent<PanelOpener>(); 
        zoom_plus = GetComponent<ZoomIn>();
        zoom_minus = GetComponent<ZoomOut>();
        SubMenu = transform.parent.transform.Find("Sub menu").gameObject;
    }

    void Update() // updates at each frame
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Began)
            {
                TouchTime = Time.time;
                DragAndDrop = false;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                DragAndDrop = true;
            }
            // Check if finger is over a UI element
            if (touch.phase == TouchPhase.Ended && Time.time - TouchTime >= 0.3f && !DragAndDrop && TouchingCurrent)
            {
                SubMenu.SetActive(!SubMenu.activeSelf);
            }
        }
    }

    public void ZoomBig()
    {
        zoom_plus.Zoomplus();
    }

    public void ZoomSmall()
    {
        zoom_minus.ZoomMinus();
    }

    public void Reflect()
    {
        rectTransform.Rotate(new Vector3(0, 180, 0));
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("selected");
        TouchingCurrent = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("unselected");
        TouchingCurrent = false;
    }
}
