using UnityEngine;
using TigerForge;
using UnityEngine.EventSystems;
using System;

public class CreateThumbnail : Thumbnail
{
    public GameObject elementPrefab;
    public DynamicElementData data;

    private bool elementExists = false;

    public void Setup(string id, DynamicElementData d)
    {
        SetId(id);
        data = d;

        EventManager.StartListening("DELETE", OnElementWasDisabled);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.dragging && Math.Abs(eventData.delta.y) < Math.Abs(eventData.delta.x)) return;
 
        if (!elementExists) InstantiateElement();

        EventManager.SetData("ENABLE_ELEMENT", id);
        EventManager.EmitEvent("ENABLE_ELEMENT");
        gameObject.SetActive(false);
    }

    private void InstantiateElement()
    {
        if (data.type.ToLower() == "background")
            data.ToBackground();
        else
        {
            GameObject obj = Instantiate(elementPrefab);
            Transform painting = GameObject.FindGameObjectWithTag("Painting").transform;
            data.ToGameObject(obj, painting, true);
        }

        elementExists = true;
    }
}
