using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StorySlot : MonoBehaviour
{
    public string SaveFile = "tablouri.json";
    public string CreateFile = "creeaza.json";
    public string SceneName = "Create_FromSave";
    public int SaveIndex;
    public SceneLoader sceneLoader;
    public bool IsPlayerSave { get; private set; }

    // Update is called once per frame
    public void SetData(bool isPlayerSave)
    {
        IsPlayerSave = isPlayerSave;
    }

    public void LoadScene()
    {
        PlayerPrefs.SetString("SaveFile", SaveFile);
        PlayerPrefs.SetString("SaveName", SceneName);
        sceneLoader.enter(SceneName);
    }

    public void LoadSceneDynamic()
    {
        PlayerPrefs.SetString("SaveFile", SaveFile);
        PlayerPrefs.SetString("SaveName", gameObject.name);
        sceneLoader.enter(SceneName);
    }

    public void SaveScene()
    {
        GetComponent<Save>().SaveGame(SaveFile, CreateFile, SceneManager.GetActiveScene().name);
    }

    public void SaveSceneDynamic()
    {
        GetComponent<Save>().SaveGame(SaveFile, CreateFile, PlayerPrefs.GetString("NumeTablou"));
    }
}
