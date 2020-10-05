using TigerForge;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubmenuButton : MonoBehaviour, IPointerClickHandler
{
    // primeste event link - menu
    // trimite actiunea care corespunde butonului respectiv

    public string Action;
    public string id;

    public void OnPointerClick(PointerEventData eventData)
    {
        id = GetComponentInParent<Submenu>().id;

        EventManager.SetData(Action, id);
        EventManager.EmitEvent(Action);
    }
}
