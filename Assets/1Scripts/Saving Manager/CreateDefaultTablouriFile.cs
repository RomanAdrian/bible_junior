using UnityEngine;
using System.IO;


public class CreateDefaultTablouriFile : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        CreateDefaultFile(SaveFilePath(), "tablouri");
        CreateDefaultFile(CreateFilePath(), "create");
    }

    public void CreateDefaultFile(string filePath, string fileName)
    {

        Debug.Log("de ke " + fileName + filePath);
        if (File.Exists(filePath)) return;

        TextAsset content = (TextAsset)Resources.Load(fileName);
        if (content.text != "") File.WriteAllText(filePath, content.text);
    }

    
    public string SaveFilePath()
    {
        return Application.persistentDataPath + "/Saves/tablouri.json";
    }

    public string CreateFilePath()
    {
        return Application.persistentDataPath + "/Saves/create.json";
    }
}
