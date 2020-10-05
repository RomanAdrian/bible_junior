using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zoom : MonoBehaviour
{
    private Vector3 scaleChange;
    private Vector3 initialScale;
    private Vector3 initialPos;

    public float[] scales;

    void Start()
    {
        initialScale = transform.localScale;
        initialPos = transform.position;
    }

    void OnDisable()
    {
        //transform.localScale = initialScale || new Vector3(0, 0, 0);
        //transform.position = initialPos;
    }

    public void ZoomIn()
    {
        int currentSize = System.Array.IndexOf(scales, transform.localScale.y / initialScale.y);
        Debug.Log(transform.localScale.y / initialScale.y);
        if (currentSize < 6)
        {
            float sign = transform.localScale.x >= 0 ? 1 : -1;
            Vector3 modifiedScale = initialScale * scales[currentSize + 1];
            transform.localScale = new Vector3(modifiedScale.x * sign, modifiedScale.y, modifiedScale.z);
        }
    }

    public void ZoomOut()
    {
        int currentSize = System.Array.IndexOf(scales, transform.localScale.y / initialScale.y);
        if (currentSize > 0)
        {
            float sign = transform.localScale.x >= 0 ? 1 : -1;
            Vector3 modifiedScale = initialScale * scales[currentSize - 1];
            transform.localScale = new Vector3(modifiedScale.x * sign, modifiedScale.y, modifiedScale.z);
        }
    }
}
