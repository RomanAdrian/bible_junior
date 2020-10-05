using TigerForge;
using UnityEngine;
using System;

public class Submenu : MonoBehaviour
{
    public string id = "000";
    EventsGroup eventsGroup = new EventsGroup();

    private void Awake()
    {
        EventManager.StartListening("LINK_MENU", gameObject, OnLinkMenu);
        EventManager.StartListening("OPEN_MENU", OnOpenMenu);
        EventManager.StartListening("DELETE", OnDisableElement);


        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        eventsGroup.StopListening();
    }

    private void OnOpenMenu()
    {
        string eventId = EventManager.GetString("OPEN_MENU");
        if (eventId == id) 
            SetActive(!gameObject.activeSelf);
        else
            SetActive(false);
    }

    private void OnDisableElement()
    {
        string eventId = EventManager.GetString("DELETE");
        if (eventId == id) SetActive(false);
    }

    private void OnLinkMenu()
    {
        string eventId = EventManager.GetString("LINK_MENU");

        if (eventId != id) SetActive(false);
        Link(eventId);
    }

    private void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }

    private void Link(string eventId)
    {
        id = eventId;
    }
}
