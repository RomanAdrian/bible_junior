﻿using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SaveData
{
    public string BackgroundName;
    public ElementData[] Elements;
    public ThumbnailData[] Thumbs;
    public string SaveTime;
    public bool IsPlayerSave;
    public string ScreenShotPath;

    public SaveData(string name, ElementData[] elements, ThumbnailData[] thumbs, DateTime saveTime, string screenShotPath, bool isPlayerSave=true)
    {
        BackgroundName = name;
        Elements = elements;
        Thumbs = thumbs;
        SaveTime = saveTime.ToString();
        IsPlayerSave = isPlayerSave;
        ScreenShotPath = screenShotPath;
    }

    public void ToGameObject(GameObject obj)
    {
        string date = DateTime.Today.ToString();
        string time = "00:00";

        try
        {
            DateTime savetime = DateTime.Parse(SaveTime);
            date = savetime.Date.Day + "." + savetime.Date.Month + "." + savetime.Date.Year;
            time = DateTime.Parse(SaveTime).TimeOfDay.ToString();
        }

        catch (FormatException) { Debug.Log("FormatException"); }

        Image image = obj.GetComponentsInChildren<Image>()[1];
        obj.GetComponent<SaveSlot>().SetData(IsPlayerSave);

        if (File.Exists(ScreenShotPath))
        {
            byte[] fileData = File.ReadAllBytes(ScreenShotPath);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            image.sprite = mySprite;
        }

        obj.GetComponentInChildren<Text>().text = "Data: " + date + "\nOra:  " + time;
    }
}
