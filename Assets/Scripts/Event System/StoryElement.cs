using UnityEngine.EventSystems;
using TigerForge;
using UnityEngine;
using System;

public class StoryElement : MonoBehaviour, IPointerUpHandler, ISelectHandler
{
    public string id;

    private void Awake()
    {
        SetId();
        EventManager.StartListening("ENABLE_ELEMENT", OnReceivedEnable);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("ENABLE_ELEMENT", OnReceivedEnable);
    }

    private void OnDisable()
    {
        EventManager.SetData("DISABLED_ELEMENT", id);
        EventManager.EmitEvent("DISABLED_ELEMENT");
    }

    public void OnSelect(BaseEventData eventData)
    {
        EventManager.SetData("LINK_MENU", id);
        EventManager.EmitEvent("LINK_MENU");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.dragging) return;

        EventManager.SetData("OPEN_MENU", id);
        EventManager.EmitEvent("OPEN_MENU");
    }

    private void OnReceivedEnable()
    {
        string eventId = EventManager.GetString("ENABLE_ELEMENT");
        if (eventId == id) Activate();
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }


    private void SetId()
    {
        if (!String.IsNullOrWhiteSpace(id)) return;

        id = (transform.GetSiblingIndex()).ToString("D3");
    }
}
