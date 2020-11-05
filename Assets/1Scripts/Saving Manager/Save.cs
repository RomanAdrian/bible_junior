﻿using UnityEngine;
using System.IO;
using EasyMobile;
using System.Linq;
using System;

public class Save : MonoBehaviour
{
    public GameObject[] Thumbnails;
    public GameObject[] StoryElements;
    public ElementData[] SerializedElements;
    public ThumbnailData[] SerializedThumbs;
    public SaveData[] Saves;

    public string SaveFolder;

    public static bool changesMade = false;

    [SerializeField]
    private SavedGame[] saveSlots;

    public void SaveGame(string saveFile, string createFile, int index)
    {
        //SetUpSaveFolder();
        CreateSaveFile(index, saveFile);
        AddObjectsToCreate(createFile);

        changesMade = false;
    }

    private void CreateSaveFile(int index, string saveFile)
    {
        if (File.Exists(GetFilePath(saveFile)))
        {
            string saveString = File.ReadAllText(GetFilePath(saveFile));
            Saves = JsonHelper.FromJson<SaveData>(saveString);
        }  else
        {
            Saves = new SaveData[6];
        }

        SerializeThumbs();
        SerializeElements();

        Saves[index] = CreateSaveObject();
        File.WriteAllText(GetFilePath(saveFile), JsonHelper.ToJson(Saves, true));
    }

    private SaveData CreateSaveObject()
    {
        string ScreenShotPath = new ScreenShot().TakeHiResShot();

        return new SaveData("name", SerializedElements, SerializedThumbs, DateTime.Now, ScreenShotPath);
    }

    private void SerializeElements()
    {
        Transform ElementsContainer = GameObject.FindGameObjectWithTag("Canvas").transform;
        int count = ElementsContainer.childCount;
        SerializedElements = new ElementData[count - 1];

        for (int i = 1; i < count; i++)
        {
            SerializedElements[i -1] = new ElementData(ElementsContainer.GetChild(i).gameObject);
        }
    }

    private void SerializeThumbs()
    {
        Transform ThumbContainer = GameObject.FindGameObjectWithTag("ButtonList").transform;
        int count = ThumbContainer.childCount;
        SerializedThumbs = new ThumbnailData[count];

        for (int i = 0; i < count; i++)
        {
            SerializedThumbs[i] = new ThumbnailData(ThumbContainer.GetChild(i).gameObject);
        }
    }

    private string GetFilePath(string saveFile)
    {
        return GetSaveFolder() + saveFile;
    }

    private string GetSaveFolder()
    {
        if (SaveFolder != "") return SaveFolder;
        return Application.persistentDataPath + "/Saves/";
    }

    private void AddObjectsToCreate(string createFile)
    {
        string[] objList = new string[100];
        if (File.Exists(GetFilePath(createFile)))
        {
            objList = File.ReadAllLines(GetFilePath(createFile));

            foreach (ElementData element in SerializedElements)
            {
                string[] matches = Array.FindAll(objList, s => s.Split(',')[0].Equals(element.Image));

                if (matches.Length == 0) File.AppendAllText(GetFilePath(createFile), element.Image + "," + element.SubmenuType + Environment.NewLine);
            }
        }
        else
        {
            objList = File.ReadAllLines(GetFilePath(createFile));

            foreach (ElementData element in SerializedElements)
            {
                File.AppendAllText(GetFilePath(createFile), element.Image + "," + element.SubmenuType + Environment.NewLine);
            }
        }
    }

    private void SetUpSaveFolder()
    {
        // Test if Save folder exists
        if (!Directory.Exists(GetSaveFolder()))
        {
            // Create save folder
            Directory.CreateDirectory(GetSaveFolder());

        }
    }
}
