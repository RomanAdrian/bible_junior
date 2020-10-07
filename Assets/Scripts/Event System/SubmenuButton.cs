using TigerForge;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubmenuButton : MonoBehaviour, IPointerClickHandler
{
    // sets the action for every button

    public string Action;
    public string id;

    public void OnPointerClick(PointerEventData eventData)
    {
        id = GetComponentInParent<Submenu>().id;

        EventManager.SetData(Action, id);
        EventManager.EmitEvent(Action);
    }
}
