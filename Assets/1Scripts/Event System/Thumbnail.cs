using UnityEngine;
using TigerForge;
using UnityEngine.EventSystems;
using System;

public class Thumbnail : MonoBehaviour, IPointerUpHandler
{
    public string id;

    public void Awake()
    {
        SetId();
        EventManager.StartListening("DELETE", OnElementWasDisabled);
    }
    public void Setup(string id)
    {
        SetId(id);
        EventManager.StartListening("DELETE", OnElementWasDisabled);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("DELETE", OnElementWasDisabled);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.dragging && Math.Abs(eventData.delta.y) < Math.Abs(eventData.delta.x))
        {
            return;
        }

        EventManager.SetData("ENABLE_ELEMENT", id);
        EventManager.EmitEvent("ENABLE_ELEMENT");
        gameObject.SetActive(false);
    }

    private void OnElementWasDisabled()
    {
        string eventId = EventManager.GetString("DELETE");

        if (eventId == id) gameObject.SetActive(true);
    }

    private void SetId(string id)
    {
        this.id = id;
    }

    private void SetId()
    {
        if (!String.IsNullOrWhiteSpace(id)) return;

        id = transform.GetSiblingIndex().ToString("D3");
    }
}
