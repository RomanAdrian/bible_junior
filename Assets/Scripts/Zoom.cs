using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float zoomOutAmount = .5f;
    public float zoomInAmount = 2f;

    private Vector3 scaleChange;
    private Vector3 initialScale;
    private Vector3 initialPos;

    void Start(){
        initialScale = transform.localScale;
        initialPos = transform.position;
    }

    void OnDisable()
    {
        transform.localScale = initialScale;
        transform.position = initialPos;
    }

    public void ZoomIn()
    {
        Vector3 scaleChange = transform.localScale * zoomInAmount;
        if (Mathf.Abs(transform.localScale.x) <= Mathf.Abs(initialScale.x))
        {
            transform.localScale = scaleChange;
        }
    }


    public void ZoomOut()
    {
        Vector3 scaleChange = transform.localScale * zoomOutAmount;
        if (Mathf.Abs(transform.localScale.x) >= Mathf.Abs(initialScale.x))
        {
            transform.localScale = transform.localScale * zoomOutAmount;
        }
    }
}
