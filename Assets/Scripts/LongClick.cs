using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class LongClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool lk = false;
    public bool inObjectBoundary;
    private float startTime, endTime;
    Vector3 startPosition, endPosition; // coordinates are Vector3 data type
    // Start is called before the first frame update
    void Start()
    {
        startTime = 0f;
        endTime = 0f;
    }
    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0)) // mark when left mouse butt is pressed
        {
            startPosition = Input.mousePosition; // reads position
            startTime = Time.time;
         }
         if (Input.GetMouseButtonUp(0)) // mark when it is released
         {
             endPosition = Input.mousePosition;
             endTime = Time.time;
         }
         if (endTime - startTime > 0.5f & endPosition == startPosition && inObjectBoundary)
         {
             Debug.Log("Long Click");
             lk = true;
             startTime = 0f;
             endTime = 0f;
         }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        inObjectBoundary = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        inObjectBoundary = false;
    }
}