using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;


public class StorySlot : MonoBehaviour
{
    public string SaveFile = "tablouri.json";
    public string CreateFile = "creeaza.json";
    public string SceneName = "Create_FromSave";
    public int SaveIndex;
    public bool forLoading = false;
    public SceneLoader sceneLoader;
    public bool IsPlayerSave = false;

    public void OnEnable()
    {
        if (!File.Exists(GetFilePath()) || forLoading == false) return;

        string saveString = File.ReadAllText(GetFilePath());
        SaveData[] saves = JsonHelper.FromJson<SaveData>(saveString);

        SaveData save = Array.Find(saves, s => s.Name == SceneName);
        if (save != null) save.ToScroll(gameObject);
    }

    // Update is called once per frame
    public void SetData(bool isPlayerSave)
    {
        IsPlayerSave = isPlayerSave;
    }

    public void LoadScene()
    {
        PlayerPrefs.SetString("SaveFile", SaveFile);
        PlayerPrefs.SetString("SaveName", SceneName);
        if (IsPlayerSave) SceneManager.LoadScene("Tablou");
        else SceneManager.LoadScene(SceneName);
    }

    public void LoadSceneDynamic()
    {
        PlayerPrefs.SetString("SaveFile", SaveFile);
        PlayerPrefs.SetString("SaveName", gameObject.name);
        sceneLoader.enter(SceneName);
    }

    public void SaveScene()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        string SceneName = activeScene == "Tablou" ? PlayerPrefs.GetString("SaveName") : activeScene;
        GetComponent<Save>().SaveGame(SaveFile, CreateFile, SceneName);
    }

    public void SaveSceneDynamic()
    {
        GetComponent<Save>().SaveGame(SaveFile, CreateFile, PlayerPrefs.GetString("NumeTablou"));
    }

    public string GetFilePath()
    {
        return Application.persistentDataPath + "/Saves/" + SaveFile;
    }
}
