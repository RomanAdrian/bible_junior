using System.Collections;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadItems : MonoBehaviour
{
    public const string url = "https://run.mocky.io/v3/4ac972b5-39ee-4720-b829-ba3fec9b27f9";
    public const string createFileDir = "/Saves/";
    public const string createFile = "create.txt";
    private const string resourcePath = "Images/Sprites/";

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
            if (spritesDontExist(e)) continue;

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

    private bool spritesDontExist(DynamicElementData e)
    {
       return Resources.Load<Sprite>(resourcePath + e.name) == null || Resources.Load<Sprite>(resourcePath + e.name + "_thumbnail") == null;
    }
}
