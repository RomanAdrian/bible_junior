using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStories : MonoBehaviour
{
    public int completedStoriesCount = 0;
    // Start is called before the first frame update
    void Awake()
    {
        StorySlot[] slots = gameObject.GetComponentsInChildren<StorySlot>();

        foreach (StorySlot slot in slots)
        {
            slot.Setup();
            if (slot.IsPlayerSave == true) completedStoriesCount++;
        }
    }
}
