using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    public GameObject[] Thumbnails;
    public GameObject[] StoryElements;
    public ElementData[] SerializedElements;
    public ThumbnailData[] SerializedThumbs;
    public string[] NarrationElements;
    public string BackgroundName;
    public SaveData[] Saves;

    public string SaveFolder;

    public static bool changesMade = false;

    [SerializeField]
    private SavedGame[] saveSlots;

    public void SaveGame(string saveFile, string createFile, string SaveName)
    {
        SetUpSaveFolder();
        CreateSaveFile(SaveName, saveFile);
        AddObjectsToCreate(createFile);

        changesMade = false;
    }

    private void CreateSaveFile(string SaveName, string saveFile)
    {

        SetBackground();
        SerializeThumbs();
        SetNarrationElements();
        SerializeElements();

        if (File.Exists(GetFilePath(saveFile)))
        {
            string saveString = File.ReadAllText(GetFilePath(saveFile));
            Saves = JsonHelper.FromJson<SaveData>(saveString);
            int index = Array.FindIndex(Saves, s => s.Name == SaveName);
            Saves.SetValue(CreateSaveObject(SaveName), index);
        }
        else
        {
            Saves = new SaveData[250];
            Saves.SetValue(CreateSaveObject(SaveName), 0);
        }

        File.WriteAllText(GetFilePath(saveFile), JsonHelper.ToJson(Saves, true));
    }

    private void SetNarrationElements()
    {
        Transform n = GameObject.FindWithTag("Canvas").transform.Find("Naratiune").GetChild(0);
        Image[] images = n.gameObject.GetComponentsInChildren<Image>();

        NarrationElements = new string[images.Length - 1];

        for (int i = 1; i < images.Length; i++)
        {
            NarrationElements[i - 1] = images[i].sprite.name;
        }
    }


    private void SetBackground()
    {
        BackgroundName = GameObject.Find("Panel").GetComponent<Image>().sprite.name;
    }

    private SaveData CreateSaveObject(string saveName)
    {
        string ScreenShotPath = new ScreenShot().TakeHiResShot();

        return new SaveData(BackgroundName, SerializedElements, SerializedThumbs, ScreenShotPath, NarrationElements, saveName);
    }

    private void SerializeElements()
    {
        Transform ElementsContainer = GameObject.FindGameObjectWithTag("Painting").transform;
        int count = ElementsContainer.childCount;
        SerializedElements = new ElementData[count - 1];

        for (int i = 1; i < count; i++)
        {
            StoryElement s = ElementsContainer.GetChild(i).gameObject.GetComponent<StoryElement>();
            if (s == null) continue;
            SerializedElements[i - 1] = new ElementData(ElementsContainer.GetChild(i).gameObject);
        }
    }

    private void SerializeThumbs()
    {
        Transform ThumbContainer = GameObject.FindGameObjectWithTag("ButtonList").transform;
        int count = ThumbContainer.childCount;
        SerializedThumbs = new ThumbnailData[count];

        for (int i = 0; i < count; i++)
        {
            Thumbnail thumb = ThumbContainer.GetChild(i).gameObject.GetComponent<Thumbnail>();
            if (thumb == null) continue;
            SerializedThumbs[i] = new ThumbnailData(ThumbContainer.GetChild(i).gameObject);
        }
    }

    private string GetFilePath(string saveFile)
    {
        return GetSaveFolder() + saveFile;
    }

    private string GetSaveFolder()
    {
        if (SaveFolder != "") return SaveFolder;
        return Application.persistentDataPath + "/Saves/";
    }

    private void AddObjectsToCreate(string createFile)
    {
        string[] objList = new string[100];
        if (File.Exists(GetFilePath(createFile)))
        {
            objList = File.ReadAllLines(GetFilePath(createFile));

            foreach (ElementData element in SerializedElements)
            {
                string[] matches = Array.FindAll(objList, s => s.Split(',')[0].Equals(element.Image));

                if (matches.Length == 0) File.AppendAllText(GetFilePath(createFile), element.Image + "," + element.SubmenuType + Environment.NewLine);
            }

            string bg = GameObject.Find("Panel").GetComponent<Image>().sprite.name;
            string[] bgMatches = Array.FindAll(objList, s => s.Split(',')[0].Equals(bg));
            if (bgMatches.Length == 0) File.AppendAllText(GetFilePath(createFile), bg + ",Background" + Environment.NewLine);
        }
        else
        {
            String fileContent = "";
            foreach (ElementData element in SerializedElements)
            {
               fileContent = fileContent + element.Image + "," + element.SubmenuType + Environment.NewLine;
            }
            fileContent = fileContent + GameObject.Find("Panel").GetComponent<Image>().sprite.name + ",Background" + Environment.NewLine;

            File.WriteAllText(GetFilePath(createFile), fileContent);
        }
    }

    private void SetUpSaveFolder()
    {
        // Test if Save folder exists
        if (!Directory.Exists(GetSaveFolder()))
        {
            // Create save folder
            Directory.CreateDirectory(GetSaveFolder());

        }
    }
}
