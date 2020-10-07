using UnityEngine.EventSystems;
using TigerForge;
using UnityEngine;
using System;

public class StoryElement : MonoBehaviour, IPointerUpHandler, ISelectHandler
{
    public string id;
    public string SubmenuType = "Submenu";
    EventsGroup eventsGroup = new EventsGroup();

    private void Awake()
    {
        SetId();

        // submenu buttons
        eventsGroup.Add("ENABLE_ELEMENT", OnReceivedEnable);
        eventsGroup.Add("DELETE", OnDelete);
        eventsGroup.Add("BRING_FORWARD", OnBringForward);
        eventsGroup.Add("ZOOM_IN", OnZoomIn);
        eventsGroup.Add("ZOOM_OUT", OnZoomOut);
        eventsGroup.Add("REFLECT", OnReflect);
        eventsGroup.StartListening();

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        eventsGroup.StopListening();
    }

    private void OnDelete()
    {
        if (IsAddressingDifferentObject("DELETE")) return;
        SetActive(false);
    }

    // link-menu event sent
    public void OnSelect(BaseEventData eventData)
    {
        EventManager.SetData("LINK_MENU", id);
        EventManager.EmitEvent("LINK_MENU", $"tag:{SubmenuType}");
    }

    // open menu event sent
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.dragging) return;

        EventManager.SetData("OPEN_MENU", id);
        EventManager.EmitEvent("OPEN_MENU");
    }

    private bool IsAddressingDifferentObject(string EventName)
    {
        string eventId = EventManager.GetString(EventName);
        return eventId != id;
    }

    // when element is activated from the thumbnail it's positioned in front of the other elements
    private void OnReceivedEnable()
    {
        if (IsAddressingDifferentObject("ENABLE_ELEMENT")) return;

        SetActive(true);
        BringForward();
    }

    // button bring front
    private void OnBringForward()
    {
        if (IsAddressingDifferentObject("BRING_FORWARD")) return;

        BringForward();
    }

    private void OnReflect()
    {
        if (IsAddressingDifferentObject("REFLECT")) return;

        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }

    private void OnZoomIn()
    {
        if (IsAddressingDifferentObject("ZOOM_IN")) return;

        Zoom zoom = GetComponent<Zoom>();
        if (zoom != null) zoom.ZoomIn();
    }

    private void OnZoomOut()
    {
        if (IsAddressingDifferentObject("ZOOM_OUT")) return;

        Zoom zoom = GetComponent<Zoom>();
        if (zoom != null) zoom.ZoomOut();
    }

    private void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }

    private void BringForward()
    {
       transform.SetAsLastSibling();
    }

    // seteaza id-ul dinamic
    private void SetId()
    {
        if (!String.IsNullOrWhiteSpace(id)) return;

        id = transform.GetSiblingIndex().ToString("D3");
    }
}
