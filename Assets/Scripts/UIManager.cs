using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public RectTransform listMenu;
    private PanelOpener toggle_mover;
    public GameObject scrollable;

    // private void Awake()
    // {
    //     toggle_mover = GetComponent<PanelOpener>();
    // }
    // Start is called before the first frame update
    public void listMenuButton()
    {
        
        if (scrollable.activeSelf) 
        {
            listMenu.DOAnchorPos(new Vector2(-3f, -(Screen.currentResolution.height * 0.4f)), 0.25f);
        }
        else
        {
            listMenu.DOAnchorPos(new Vector2(-3, -(Screen.currentResolution.height * 0.15f)), 0.25f);
        }
    }
}
