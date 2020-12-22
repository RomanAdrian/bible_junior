using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class StorySlot : MonoBehaviour, IPointerUpHandler
{
    public string SaveFile = "tablouri.json";
    public string CreateFile = "creeaza.json";
    public string SceneName = "Create_FromSave";
    public bool forLoading = false;
    public bool IsPlayerSave = false;
    public string date;
    public int dayOfWeek;


    public void Start()
    {
        if (!File.Exists(GetFilePath()) || forLoading == false) return;

        string saveString = File.ReadAllText(GetFilePath());
        SaveData[] saves = JsonHelper.FromJson<SaveData>(saveString);

        SaveData save = Array.Find(saves, s => s.Name == SceneName);
        if (save != null && date == DateTime.Today.ToString("dd/MM/yyyy")) save.ToScroll(gameObject);
        else DefaultSetup();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (forLoading == true) LoadScene();
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

    public void SaveScene()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        string SceneName = activeScene == "Tablou" ? PlayerPrefs.GetString("SaveName") : activeScene;
        GetComponent<Save>().SaveGame(SaveFile, CreateFile, SceneName);
    }

    public string GetFilePath()
    {
        return Application.persistentDataPath + "/Saves/" + SaveFile;
    }

    private void DefaultSetup()
    {
        Sprite sprite = Resources.Load<Sprite>("Images/Sprites/" + SceneName.ToLower());
        gameObject.GetComponent<Image>().sprite = sprite;
    }
}
