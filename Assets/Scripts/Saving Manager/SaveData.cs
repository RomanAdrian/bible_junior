using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string Name;
    public ElementData[] Elements;
    public ThumbnailData[] Thumbs;
    public string SaveTime;
    public bool IsPlayerSave;

    public SaveData(string name, ElementData[] elements, ThumbnailData[] thumbs, DateTime saveTime, bool isPlayerSave=true)
    {
        Name = name;
        Elements = elements;
        Thumbs = thumbs;
        SaveTime = saveTime.ToString();
        IsPlayerSave = isPlayerSave;
    }
}
