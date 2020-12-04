using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class LoadScrolls : MonoBehaviour
{
    public string SaveFileName = "tablouri.json";
    public string SaveFolder;
    public string isSaveSlot;

    public void OnEnable()
    {
        if (!File.Exists(GetFilePath())) return;

        string saveString = File.ReadAllText(GetFilePath());
        SaveData[] saves = JsonHelper.FromJson<SaveData>(saveString);

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject currentObj = transform.GetChild(i).gameObject;
            string name = currentObj.GetComponent<StorySlot>() != null ? currentObj.GetComponent<StorySlot>().SceneName : currentObj.name;
            SaveData save = Array.Find(saves, s => s.Name == name);
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
