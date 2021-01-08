using UnityEngine.EventSystems;
using TigerForge;
using UnityEngine;
using System;

public class StoryElement : MonoBehaviour, IPointerUpHandler, ISelectHandler
{
    public string id;
    public string SubmenuType;
    private bool alreadySetUp = false;
    private int index = 0;
    EventsGroup eventsGroup = new EventsGroup();

    // SUBSCRIBING TO EVENTS
    public void Awake()
    {
        SetId(); // Sets this object's id, so we can identify if the events are addressing it or another StoryElement

        if (!alreadySetUp) 
        {
            // We set StoryElements to listen for these events ("ENABLE_ELEMENT", "DELETE", etc) and respond with their respective callback methods (OnReceivedEnable, OnDelete, etc) 
            eventsGroup.Add("ENABLE_ELEMENT", OnReceivedEnable); // Triggered when you click the thumbnail and enable the StoryElement
            eventsGroup.Add("DELETE", OnDelete);                 // Triggered when you click the delete button in the submenu
            eventsGroup.Add("BRING_FORWARD", OnBringForward);    // Triggered when you click the bring forward button in the submenu
            eventsGroup.Add("ZOOM_IN", OnZoomIn);                // Triggered when you zoom in in the submenu
            eventsGroup.Add("ZOOM_OUT", OnZoomOut);              // Triggered when you zoom out in the submenu
            eventsGroup.Add("REFLECT", OnReflect);               // Triggered when you click reflect in the submenu
            eventsGroup.StartListening();
            index = transform.GetSiblingIndex();
            alreadySetUp = true;
        }

        gameObject.SetActive(false);
    }

    public void Setup(string id, string submenuType="Submenu")
    {
        SetId(id); // Sets this object's id, so we can identify if the events are addressing it or another StoryElement
        SubmenuType = submenuType;

        if (!alreadySetUp)
        {
            // We set StoryElements to listen for these events ("ENABLE_ELEMENT", "DELETE", etc) and respond with their respective callback methods (OnReceivedEnable, OnDelete, etc) 
            eventsGroup.Add("ENABLE_ELEMENT", OnReceivedEnable); // Triggered when you click the thumbnail and enable the StoryElement
            eventsGroup.Add("DELETE", OnDelete);                 // Triggered when you click the delete button in the submenu
            eventsGroup.Add("BRING_FORWARD", OnBringForward);    // Triggered when you click the bring forward button in the submenu
            eventsGroup.Add("ZOOM_IN", OnZoomIn);                // Triggered when you zoom in in the submenu
            eventsGroup.Add("ZOOM_OUT", OnZoomOut);              // Triggered when you zoom out in the submenu
            eventsGroup.Add("REFLECT", OnReflect);               // Triggered when you click reflect in the submenu
            eventsGroup.StartListening();
            index = transform.GetSiblingIndex();
            alreadySetUp = true;
        }
    }

    // UNSUBSCRIBING TO EVENTS
    private void OnDestroy()
    {
        eventsGroup.StopListening();
    }

    // SENDING EVENTS

    // When this object is selected, we want to tell the Submenu that it should address all of its actions to this object
    // We emit the event and object with the Submenu script receives and deals with it
    public void OnSelect(BaseEventData eventData)
    {
        EventManager.SetData("LINK_MENU", id);
        EventManager.EmitEvent("LINK_MENU", $"tag:{SubmenuType}"); // tag:SubmenuType means we only send the event to objects with that particular tag. In this case, either 
                                                                   // the SmallSubmenu or the regular Submenu
    }

    // On click, send the event to open the Submenu. The Submenu script handles it.
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.dragging) return;

        EventManager.SetData("OPEN_MENU", id);
        EventManager.EmitEvent("OPEN_MENU");
    }

    // RECEIVING EVENTS
    // All of these use the IsAddressingDifferentObject method, which is implemented and explained in the "HELPERS" section
    // But basically it checks if the event's id is the same as this StoryElement's id and if it is the event is talking to *this* StoryElement

    // Deactivate the object if received a DELETE event (which happens when the delete button was clicked in the submenu)
    private void OnDelete()
    {
        if (IsAddressingDifferentObject("DELETE")) return;
        SetActive(false);
    }

    // Set the object to active and also put on top of other StoryElements if received ENABLE_ELEMENT event (which means when the thumbnail was clicked) 
    private void OnReceivedEnable()
    {
        if (IsAddressingDifferentObject("ENABLE_ELEMENT")) return;

        transform.SetSiblingIndex(index);
        SetActive(true);
    }

    // Bring forward if received a BRING_FORWARD event (which happens when the BRING_FORWARD button was clicked in the submenu)
    private void OnBringForward()
    {
        if (IsAddressingDifferentObject("BRING_FORWARD")) return;

        BringForward();
    }

    // Flip horizontally if received a REFLECT event (which happens when the REFLECT button was clicked in the submenu)
    private void OnReflect()
    {
        if (IsAddressingDifferentObject("REFLECT")) return;

        Vector3 scale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }

    // Call the ZoomIn function from a Zoom script attached to this same gameobject if received a "ZOOM_IN" event (aka the Zoom + button was clicked in the submenu)
    private void OnZoomIn()
    {
        if (IsAddressingDifferentObject("ZOOM_IN")) return;

        Zoom zoom = GetComponent<Zoom>();
        if (zoom != null) zoom.ZoomIn();
    }

    // Call the ZoomOut function from a Zoom script attached to this same gameobject if received a "ZOOM_OUT" event (aka the Zoom - button was clicked in the submenu)
    private void OnZoomOut()
    {
        if (IsAddressingDifferentObject("ZOOM_OUT")) return;

        Zoom zoom = GetComponent<Zoom>();
        if (zoom != null) zoom.ZoomOut();
    }

    // HELPER METHODS

    // Check if the event is addressing *this* StoryElement or another by comparing the id received with the Event with this object's id
    // This works for Thumbnails because they're coupled together: each Thumbnail has a StoryElement and viceversa, their ids are set when the objects are first initialized (the SetId method)
    // And it works with the Submenu because when a StoryElement is selected we send the LINK_MENU event and the Submenu sets its own id to equal the id of the selected StoryElement
    private bool IsAddressingDifferentObject(string EventName)
    {
        string eventId = EventManager.GetString(EventName);
        return eventId != id;
    }

    private void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }

    private void BringForward()
    {
        transform.transform.SetAsLastSibling();
    }

    private void SetId(string id)
    {
       this.id = id;
    }

    private void SetId()
    {
        if (!String.IsNullOrWhiteSpace(id)) return;

        id = (transform.GetSiblingIndex() - 1).ToString("D3");
    }
}
