using UnityEngine.EventSystems;
using UnityEngine;

public class CreateThumbIndex : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        Transform guiParent = GameObject.Find("SavePanelCanvas").transform;
        if (guiParent) guiParent.GetChild(guiParent.childCount - 1).gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    public void SetLast()
    {
        gameObject.transform.SetAsLastSibling();
    }
}
