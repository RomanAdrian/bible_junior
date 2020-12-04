using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class LoadScrolls : MonoBehaviour
{
    public string SaveFileName = "player_saves.json";
    public string SaveFolder;
    public string isSaveSlot;

    public void Awake()
    {
        if (!File.Exists(GetFilePath())) return;

        string saveString = File.ReadAllText(GetFilePath());
        SaveData[] saves = JsonHelper.FromJson<SaveData>(saveString);



        for (int i = 0; i < transform.childCount; i++)
        { 
            GameObject currentObj = transform.GetChild(i).gameObject;
            SaveData save = Array.Find(saves, s => s.Name == currentObj.name);
            if (save != null) save.ToScroll(currentObj);
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
