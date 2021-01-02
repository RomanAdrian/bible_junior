using UnityEngine;
using System.IO;
using System;


public class CreateDefaultTablouriFile : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        SetUpSaveFolder();
        CreateDefaultFile(CreateFilePath(), "create");
        CheckSizeInSaul();
    }

    public void CreateDefaultFile(string filePath, string fileName)
    {
        if (File.Exists(filePath)) return;


        TextAsset content = (TextAsset)Resources.Load(fileName);
        Debug.Log(content.text);
        if (content.text != "") File.WriteAllText(filePath, content.text);
    }

    private void CheckSizeInSaul()
    {
        if (!File.Exists(TablouriFilePath())) return;

        string saveString = File.ReadAllText(TablouriFilePath());
        SaveData[] Saves = JsonHelper.FromJson<SaveData>(saveString);
        SaveData saveInstance = Array.Find(Saves, s => s.Name == "Saul1");
        ElementData fatGuy = Array.Find(saveInstance.Elements, e => e.Image == "P2.36");
        fatGuy.Transform.SizeDelta = new Vector2(344, 717);
        File.WriteAllText(TablouriFilePath(), JsonHelper.ToJson(Saves, true));
    }

    public string SaveFilePath()
    {
        return Application.persistentDataPath + "/Saves/tablouri.json";
    }

    public string CreateFilePath()
    {
        return Application.persistentDataPath + "/Saves/create.json";
    }

    private void SetUpSaveFolder()
    {
        // Test if Save folder exists
        if (!Directory.Exists(Application.persistentDataPath + "/Saves/"))
        {
            // Create save folder
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves/");

        }
    }

    private string TablouriFilePath()
    {
        return Application.persistentDataPath + "/Saves/tablouri.json";
    }
}
