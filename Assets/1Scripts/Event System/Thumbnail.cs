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

    protected void OnDestroy()
    {
        EventManager.StopListening("DELETE", OnElementWasDisabled);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.dragging && Math.Abs(eventData.delta.y) < Math.Abs(eventData.delta.x))
        {
            return;
        }

        EventManager.SetData("ENABLE_ELEMENT", id);
        EventManager.EmitEvent("ENABLE_ELEMENT");
        gameObject.SetActive(false);
    }

    protected void OnElementWasDisabled()
    {
        string eventId = EventManager.GetString("DELETE");

        if (eventId == id) gameObject.SetActive(true);
    }

    protected void SetId(string id)
    {
        this.id = id;
    }

    protected void SetId()
    {
        if (!String.IsNullOrWhiteSpace(id)) return;

        id = transform.GetSiblingIndex().ToString("D3");
    }
}
