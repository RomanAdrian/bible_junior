using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SaveData
{
    public string Name;
    public string BackgroundName;
    public ElementData[] Elements;
    public ThumbnailData[] Thumbs;
    public string SaveTime;
    public string AudioSource;
    public bool IsPlayerSave;
    public string ScreenShotPath;
    public string[] NarrationElements;

    public SaveData(string backgroundName, ElementData[] elements, ThumbnailData[] thumbs, string screenShotPath, string[] narration, string SaveName, string audioSource = "", bool isPlayerSave=true)
    {
        BackgroundName = backgroundName;
        Elements = elements;
        Thumbs = thumbs;
        SaveTime = DateTime.Now.ToString();
        IsPlayerSave = isPlayerSave;
        NarrationElements = narration;
        ScreenShotPath = screenShotPath;
        Name = SaveName;
        AudioSource = audioSource;
    }

    public void ToSaveSlot(GameObject obj)
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

        Image[] images = obj.GetComponentsInChildren<Image>();
        obj.GetComponent<SaveSlot>().SetData(IsPlayerSave);

        if (File.Exists(ScreenShotPath))
        {
            byte[] fileData = File.ReadAllBytes(ScreenShotPath);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            images[3].sprite = mySprite;

            images[1].gameObject.SetActive(false);
            images[2].gameObject.SetActive(false);
            images[3].color = new Color(images[3].color.r, images[3].color.g, images[3].color.b, 1f);
        }

        obj.GetComponentInChildren<Text>().text = "Data: " + date + "\nOra:  " + time;
    }


    public void ToScroll(GameObject obj)
    {

        Image image = obj.GetComponent<Image>();
        obj.GetComponent<StorySlot>().SetData(IsPlayerSave);

        if (File.Exists(ScreenShotPath))
        {
            byte[] fileData = File.ReadAllBytes(ScreenShotPath);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            image.sprite = mySprite;
        }
    }
}
