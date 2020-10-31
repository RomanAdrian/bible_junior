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
    public int MaxSaves = 6;

    public string SaveFolder;
    public string SaveFileName = "player_saves.json";

    public static bool changesMade = false;

    [SerializeField]
    private SavedGame[] saveSlots;

    private void Awake()
    {

        // Test if Save folder exists
        if (!Directory.Exists(GetSaveFolder()))
        {
            // Create save folder
            Directory.CreateDirectory(GetSaveFolder());
        }

        Thumbnails = GameObject.FindGameObjectsWithTag("Thumbnail");
        StoryElements = GameObject.FindGameObjectsWithTag("StoryElement");
        SerializedElements = new ElementData[StoryElements.Count()];
        SerializedThumbs = new ThumbnailData[Thumbnails.Count()];
        Saves = new SaveData[MaxSaves];
    }

    public void SaveGame()
    {
        Saves[SaveIndex] = CreateSaveObject();
        string SavesString = JsonHelper.ToJson(Saves, true);

        File.WriteAllText(GetFilePath(), SavesString);

        changesMade = false;
    }

    public SaveData CreateSaveObject()
    {
        for (int i = 0; i < StoryElements.Count(); i++)
        {
            SerializedElements[i] = new ElementData(StoryElements[i]); ;
            
            SerializedThumbs[i] = new ThumbnailData(Thumbnails[i]);
        }

        return new SaveData("name", SerializedElements, SerializedThumbs, DateTime.Now);
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
