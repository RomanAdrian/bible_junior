using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class Load : MonoBehaviour
{
    public int SaveIndex = 0;

    public string SaveFolder;
    public string SaveFileName = "player_saves.json";

    public string PathToImages;
    public string PathToThumbs;
    public GameObject ElementPrefab;
    public GameObject ThumbnailPrefab;

    GameObject Canvas;
    GameObject Buttons;

    public static bool changesMade = false;

    public void Awake()
    {
        Buttons = GameObject.FindGameObjectWithTag("ButtonList");
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        SaveFileName = PlayerPrefs.GetString("SaveFile");
        SaveIndex = PlayerPrefs.GetInt("Index");
        LoadGame();
    }

    public void LoadGame()
    {
        if (!File.Exists(GetFilePath())) return;

        string saveString = File.ReadAllText(GetFilePath());
        SaveData save = JsonHelper.FromJson<SaveData>(saveString)[SaveIndex];

        ElementData[] objects = save.Elements;
        ThumbnailData[] thumbs = save.Thumbs;

        foreach (ElementData obj in objects)
        {
            GameObject currentObj = Instantiate(ElementPrefab);
            if (currentObj == null) continue;

            currentObj.transform.SetParent(Canvas.transform);
            obj.ToGameObject(currentObj, PathToImages);
        }

        foreach (ThumbnailData thumb in thumbs)
        {
            GameObject currentObj = Instantiate(ThumbnailPrefab);
            if (currentObj == null) continue;

            currentObj.transform.SetParent(Buttons.transform);
            thumb.ToGameObject(currentObj, PathToThumbs);
        }
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
