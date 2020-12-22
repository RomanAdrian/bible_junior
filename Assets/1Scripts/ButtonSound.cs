using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerDownHandler
{
    public AudioSource audioData;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (audioData == null) return;
        {
            audioData.Play();
        }
    }
}
