using System.Collections;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadItems : MonoBehaviour
{
    public const string url = "https://run.mocky.io/v3/aaf2702e-3f40-4554-ae0a-036e0ccb4539";
    public const string createFileDir = "/Saves/";
    public const string createFile = "create.txt";

    private DynamicElementData[] elements;
    public GameObject itemPrefab;
    public GameObject thumbnailPrefab;

    void Start()
    {
        if (false) //(RecentSaveFileExists())
        {
            GetDataFromFile();
        }
        else
        {
            StartCoroutine(GetApiData());
            SetUpThumbnails();
        }
    }

    private IEnumerator GetApiData()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("No internet:(");
        }
        else
        {
            elements = JsonHelper.FromJson<DynamicElementData>(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);
            SaveDataLocally(request.downloadHandler.text);
            SetUpThumbnails();
        }

        // Clean up any resources it is using.
        request.Dispose();
    }

    private void GetDataFromFile()
    { 
    }

    private void SetUpThumbnails()
    {
        foreach (DynamicElementData e in elements)
        {
            Debug.Log(e);
            GameObject obj = Instantiate(thumbnailPrefab);
            e.ToThumbnail(obj, transform);
        }
    }

    private bool RecentSaveFileExists()
    {
        return File.Exists(FilePath()) && (File.GetLastWriteTimeUtc(FilePath()) >= DateTime.Today.AddDays(-15));
    }

    private void SaveDataLocally(string data)
    {
        if (!Directory.Exists(Application.persistentDataPath + createFileDir))
        {
            // Create save file
            Directory.CreateDirectory(Application.persistentDataPath + createFileDir);
        }

        File.WriteAllText(FilePath(), data);
    }

    private string FilePath()
    {
        return Application.persistentDataPath + createFileDir + createFile;
    }
}
