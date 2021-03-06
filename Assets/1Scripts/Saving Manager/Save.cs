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
    public string AudioSource;
    public SaveData[] Saves;
    private string ScreenShotPath;

    public string SaveFolder;

    public static bool changesMade = false;

    [SerializeField]
    private SavedGame[] saveSlots;

    public string SaveGame(string saveFile, string createFile, string SaveName)
    {
        SetUpSaveFolder();
        CreateSaveFile(SaveName, saveFile);
        AddObjectsToCreate(createFile);

        changesMade = false;
        return ScreenShotPath;
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
            if (index == -1)
            { 
                int emptyIndex = Array.FindIndex(Saves, s => s.Name == "");
                if (emptyIndex == -1) emptyIndex = 0;
                Saves.SetValue(CreateSaveObject(SaveName), emptyIndex);
            }
            else Saves.SetValue(CreateSaveObject(SaveName), index);
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
        Transform saveCanvas = GameObject.Find("SavePanelCanvas").transform;
        if (saveCanvas == null) return;

        Transform naratiune = saveCanvas.Find("GUI/Naratiune");
        if (naratiune == null) return;

        Transform n = naratiune.Find("Naratiunea Scroll/Viewport/Content");
        Image[] images = n.gameObject.GetComponentsInChildren<Image>();

        NarrationElements = new string[images.Length];

        for (int i = 0; i < images.Length; i++)
        {
            if (images[i] != null && images[i].sprite != null)
            {
                NarrationElements[i] = images[i].sprite.name;
            }
        }

        AudioSource sunet = naratiune.GetComponentInChildren<AudioSource>();
        if (sunet != null && sunet.clip != null) AudioSource = sunet.clip.name;
    }


    private void SetBackground()
    {
        Image panel = GameObject.Find("Panel").GetComponent<Image>();
        if (panel == null || panel.sprite == null) return;

        BackgroundName = GameObject.Find("Panel").GetComponent<Image>().sprite.name;
    }

    private SaveData CreateSaveObject(string saveName)
    {
        ScreenShotPath = new ScreenShot().TakeHiResShot();

        return new SaveData(BackgroundName, SerializedElements, SerializedThumbs, ScreenShotPath, NarrationElements, saveName, AudioSource);
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

                if (matches.Length == 0) File.AppendAllText(GetFilePath(createFile), element.Image + ","
                                                                                     + element.SubmenuType + ","
                                                                                     + DateTime.Today.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                                                                                     + element.Transform.SizeDelta[0] + ","
                                                                                     + element.Transform.SizeDelta[1] + ","
                                                                                     + Environment.NewLine);
            }

            Image img = GameObject.Find("Panel").GetComponent<Image>();
            if (img != null && img.sprite != null)
            {
                string bg = img.sprite.name;
                string[] bgMatches = Array.FindAll(objList, s => s.Split(',')[0].Equals(bg));
                if (bgMatches.Length == 0) File.AppendAllText(GetFilePath(createFile), bg + ",Background" + Environment.NewLine);
            }
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
