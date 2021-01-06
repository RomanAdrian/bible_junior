using UnityEngine.EventSystems;
using UnityEngine;

public class ForwardButton : BackButton
{
    // Start is called before the first frame update
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        LoadElements();
        RepositionThumbnail();
    }

    private void LoadElements()
    {
        LoadHandler loadHandler = gameObject.GetComponent<LoadHandler>();

        Debug.Log(loadHandler);
        if (loadHandler) loadHandler.LoadItems();
    }

    private void RepositionThumbnail()
    {
        GameObject buttonList = GameObject.FindWithTag("ButtonList");
        if (!buttonList) return;

        CreateThumbIndex script = buttonList.GetComponentInChildren<CreateThumbIndex>();
        if (script) script.SetLast();
    }
}
