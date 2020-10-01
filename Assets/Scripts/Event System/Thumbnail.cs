using UnityEngine;
using TigerForge;
using UnityEngine.EventSystems;

public class Thumbnail : MonoBehaviour, IPointerClickHandler
{
    public string id;

    public void Start()
    {
        SetId();
        EventManager.StartListening("DISABLED_ELEMENT", OnElementWasDisabled);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("DISABLED_ELEMENT", OnElementWasDisabled);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventManager.SetData("ENABLE_ELEMENT", id);
        EventManager.EmitEvent("ENABLE_ELEMENT");
        gameObject.SetActive(false);
    }

    private void OnElementWasDisabled()
    {
        string eventId = EventManager.GetString("DISABLED_ELEMENT");

        if (eventId == id) gameObject.SetActive(true);
    }

    private void SetId()
    {
        if (!System.String.IsNullOrWhiteSpace(id)) return;

        id = (transform.GetSiblingIndex() + 1).ToString("D3");
    }
}
