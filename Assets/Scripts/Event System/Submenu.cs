using TigerForge;
using UnityEngine;
using System;

public class Submenu : MonoBehaviour
{
    public string id;
    EventsGroup eventsGroup = new EventsGroup();

    private void Awake()
    {
        SetId();
        eventsGroup.Add("LINK_MENU", OnLinkMenu);
        eventsGroup.Add("OPEN_MENU", OnOpenMenu);
        eventsGroup.StartListening();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        eventsGroup.StopListening();
    }

    private void OnOpenMenu()
    {
        string eventId = EventManager.GetString("OPEN_MENU");
        if (eventId == id) Activate();
    }

    private void OnLinkMenu()
    {
        string eventId = EventManager.GetString("LINK_MENU");
        Link(eventId);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }

    private void Link(string eventId)
    {
        id = eventId;
    }

    private void SetId()
    {
        if (!String.IsNullOrWhiteSpace(id)) return;

        id = (transform.GetSiblingIndex() + 1).ToString("D3");
    }
}
