using UnityEngine;
using System.IO;
using EasyMobile;
using System.Linq;
using System;

public class SavingSystem : MonoBehaviour
{
    public GameObject[] Thumbnails;
    public GameObject[] StoryElements;
    public static CanvasData[] MaskData;
    public string SAVE_FOLDER;
    public static bool changesMade = false;
    public static int saveNumber = 1;

    [SerializeField]
    private SavedGame[] saveSlots;

    private void Awake()
    {
        SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
        // Test if Save folder exists
        if (!Directory.Exists(SAVE_FOLDER))
        {
            // Create save folder
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        Thumbnails = GameObject.FindGameObjectsWithTag("Thumbnail");
        StoryElements = GameObject.FindGameObjectsWithTag("StoryElement");
        MaskData = new CanvasData[StoryElements.Count()];

    }

    public void Save()
    {
        int i = 0;
        foreach (GameObject StoryElement in StoryElements)
        {
            CanvasData date = new CanvasData(StoryElement); // call pe constructorul din clasa respectiva
            MaskData[i] = date;
            i++;
        }

        string json = JsonHelper.ToJson(MaskData, true);
        while (File.Exists(FilePath()))
        {
            saveNumber++;
        }

        File.WriteAllText(FilePath(), json);
        Debug.Log(FilePath());

        changesMade = false;
    }

    public void Load(string num)
    {
        if (!File.Exists(FilePath(num))) return;
        Debug.Log(FilePath(num));

        string saveString = File.ReadAllText(FilePath(num));
        CanvasData[] objects = JsonHelper.FromJson<CanvasData>(saveString);

        foreach (CanvasData obj in objects)
        {
            if (obj.name == "") continue;
            GameObject currentObj = Array.Find(StoryElements, e => e.name == obj.name);
            if (currentObj == null) continue;

            Debug.Log(obj.name + "  " + obj.active);
            Debug.Log(currentObj.name + " " + currentObj.activeSelf);

            currentObj.transform.localPosition = new Vector3(obj.position[0], obj.position[1], 0);
            currentObj.transform.localScale = new Vector3(obj.scale[0], obj.scale[1], 0);
            currentObj.transform.SetSiblingIndex(obj.childcount + 1);

            currentObj.SetActive(obj.active);

            char index = obj.name.Split('(')[1][0];

            Debug.Log(thumbName(index));
            GameObject currentThumb = Array.Find(Thumbnails, t => t.name == thumbName(index));

            currentThumb.SetActive(!obj.active);
        }
    }

    public string FilePath()
    {
        return SAVE_FOLDER + "save_" + saveNumber + ".json";
    }


    public string FilePath(string num)
    {
        return SAVE_FOLDER + "save_" + num + ".json";
    }

    private string thumbName(char index)
    {
        return "Button (" + index + ")";
    }
}
