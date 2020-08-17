using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    private Vector3 scaleChange;

    public void ZoomMinus()
    {
        scaleChange = transform.localScale;
        scaleChange -= new Vector3(.25f, .25f, 0); // creeaza un obiect nou si il initializeaza cu valorile respective ^

        if (scaleChange.x >= .15f)
        {
            transform.localScale = scaleChange;
        }
    }
}
