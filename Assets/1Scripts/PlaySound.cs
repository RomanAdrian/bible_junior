using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySound : MonoBehaviour, IPointerUpHandler
{
    public AudioSource audioData;
    bool playing = false;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!playing)
        {
            audioData.Play();
            playing = true;
        }
        else
        {
            audioData.Pause();
            playing = false;
        }
    }
}
