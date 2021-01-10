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
        if (File.Exists(filePath))
        {
            TextAsset content = (TextAsset)Resources.Load(fileName);
            String[] lines = content.text.Split('\n');

            String[] savedLines = File.ReadAllLines(filePath);
            File.AppendAllText(filePath, "\n");

            foreach (String line in lines)
            {
                string element = line.Split(',')[0];
                string found = Array.Find(savedLines, s => s.Split(',')[0] == element);

                if (found == null) File.AppendAllText(filePath, line);
            }
        }
        else 
        {
            TextAsset content = (TextAsset)Resources.Load(fileName);
            if (content.text != "") File.WriteAllText(filePath, content.text);
        }
    }

    private void CheckSizeInSaul()
    {
        if (!File.Exists(TablouriFilePath())) return;

        string saveString = File.ReadAllText(TablouriFilePath());
        SaveData[] Saves = JsonHelper.FromJson<SaveData>(saveString);
        SaveData saveInstance = Array.Find(Saves, s => s.Name == "Saul1");
        if (saveInstance == null) return;

        ElementData fatGuy = Array.Find(saveInstance.Elements, e => e.Image == "P2.36");
        if (fatGuy == null) return;

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
