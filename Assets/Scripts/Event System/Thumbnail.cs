﻿using UnityEngine;
using TigerForge;
using UnityEngine.EventSystems;

public class Thumbnail : MonoBehaviour, IPointerDownHandler
{
    public string id;

    public void Start()
    {
        SetId();
        EventManager.StartListening("DELETE", OnElementWasDisabled);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("DELETE", OnElementWasDisabled);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventManager.SetData("ENABLE_ELEMENT", id);
        EventManager.EmitEvent("ENABLE_ELEMENT");
        gameObject.SetActive(false);
    }

    private void OnElementWasDisabled()
    {
        string eventId = EventManager.GetString("DELETE");

        if (eventId == id) gameObject.SetActive(true);
    }

    private void SetId()
    {
        if (!System.String.IsNullOrWhiteSpace(id)) return;

        id = (transform.GetSiblingIndex() + 1).ToString("D3");
    }
}
