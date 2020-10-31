using UnityEngine;
using System.IO;
using EasyMobile;
using System.Linq;
using System;

public class Save : MonoBehaviour
{
    public GameObject[] Thumbnails;
    public GameObject[] StoryElements;
    public ElementData[] SerializedElements;
    public ThumbnailData[] SerializedThumbs;
    public SaveData[] Saves;
    public int SaveIndex = 0;

    public string SaveFolder;
    public string SaveFileName = "player_saves.json";

    public static bool changesMade = false;

    [SerializeField]
    private SavedGame[] saveSlots;
    
    public void SaveGame(string SaveFile, int index)
    {
        SaveFileName = SaveFile;
        SaveIndex = index; 

        // Test if Save folder exists
        if (!Directory.Exists(GetSaveFolder()))
        {
            // Create save folder
            Directory.CreateDirectory(GetSaveFolder());
        }

        string saveString = File.ReadAllText(GetFilePath());
        Saves = JsonHelper.FromJson<SaveData>(saveString);


        Transform ThumbContainer = GameObject.FindGameObjectWithTag("ButtonList").transform;
        Transform ElementsContainer = GameObject.FindGameObjectWithTag("Canvas").transform;

        Thumbnails = new GameObject[ThumbContainer.childCount];
        StoryElements = new GameObject[ElementsContainer.childCount];

        for (int i = 0; i < ThumbContainer.childCount; i++)
        {
            Thumbnails[i] = ThumbContainer.GetChild(i).gameObject;
            StoryElements[i] = ElementsContainer.GetChild(i + 1).gameObject;
        }

        SerializedElements = new ElementData[Thumbnails.Count()];
        SerializedThumbs = new ThumbnailData[Thumbnails.Count()];
 
        Saves[SaveIndex] = CreateSaveObject();
        string SavesString = JsonHelper.ToJson(Saves, true);

        File.WriteAllText(GetFilePath(), SavesString);

        changesMade = false;
    }

    public SaveData CreateSaveObject()
    {
        for (int i = 0; i < Thumbnails.Count(); i++)
        {
            SerializedElements[i] = new ElementData(StoryElements[i]);
            SerializedThumbs[i] = new ThumbnailData(Thumbnails[i]);
        }

        string ScreenShotPath = new ScreenShot().TakeHiResShot();

        return new SaveData("name", SerializedElements, SerializedThumbs, DateTime.Now, ScreenShotPath);
    }

    public string GetFilePath()
    {
        return GetSaveFolder() + SaveFileName;
    }

    public string GetSaveFolder()
    {
        if (SaveFolder != "") return SaveFolder;
        return Application.persistentDataPath + "/Saves/";
    }
}
