using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFunctions : MonoBehaviour
{
    public GameObject currentlySelected;
    private ElementUi elementUI;

    // Start is called before the first frame update
    public void Start()
    {
        elementUI = currentlySelected.GetComponent<ElementUi>();
    }

    public void setCurrentlySelected(GameObject obj)
    {
        currentlySelected = obj;
    }

    //public void SizeUp()
    //{
    //    Debug.Log("Panel zoom");
    //    elementUI.ZoomBig();
    //}

    //public void SizeDown()
    //{
    //    elementUI.ZoomSmall();
    //}

    // public void BringToFront()
    // {
    //     sortinglayer.Sorted();
    // }

    public void Delete()
    {
        currentlySelected.SetActive(false);
    }

    public void Mirror()
    {
        elementUI.Reflect();
    }
}