using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CreateDefaultTablouriFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(SaveFilePath())) return;

        TextAsset content = (TextAsset)Resources.Load("tablouri");
        if (content.text != "") File.WriteAllText(SaveFilePath(), content.text);
    }

    public string SaveFilePath()
    {
        return Application.persistentDataPath + "/Saves/tablouri.json";
    }
}
