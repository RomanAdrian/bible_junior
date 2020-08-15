using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementUi : MonoBehaviour
{
    private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private PanelOpener po; // initializat
    private LongClick my_long_click;
    private ZoomIn zoom_plus;
    private ZoomOut zoom_minus;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        po = GetComponent<PanelOpener>(); // asignare
        my_long_click = GetComponent<LongClick>();
        zoom_plus = GetComponent<ZoomIn>();
        zoom_minus = GetComponent<ZoomOut>();
    }

    void Update() // updates at each frame
    {
        Debug.Log("OnPointerDown");
        if (my_long_click.lk == true)
        {
            po.OpenPanel();
            my_long_click.lk = false;
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
}
