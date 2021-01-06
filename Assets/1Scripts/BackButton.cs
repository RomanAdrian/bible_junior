using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerUpHandler
{
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        StartSound();
        HideElementList();
    }

    protected void StartSound()
    {
        AudioSource click = GameObject.Find("Audio Source - Click").GetComponent<AudioSource>();
        if (click) click.Play();
    }

    protected void HideElementList()
    {
        GameObject panel = transform.parent.gameObject;

        if (panel && panel.name == "GUI Creeaza") panel.SetActive(false);
    }
}
