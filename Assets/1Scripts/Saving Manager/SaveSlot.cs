using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public string SaveFile = "player_saves.json";
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
        if (!IsPlayerSave) return;
        PlayerPrefs.SetString("SaveFile", SaveFile);
        PlayerPrefs.SetString("SaveName", SaveIndex.ToString());
        SceneManager.LoadScene(SceneName);
    }
    public void SaveScene()
    {
        string screenshotPath = GetComponent<Save>().SaveGame(SaveFile, CreateFile, SaveIndex.ToString());
        Image image = GetComponentsInChildren<Image>()[1];

        if (File.Exists(screenshotPath) && image != null)
        {
            byte[] fileData = File.ReadAllBytes(screenshotPath);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            image.sprite = mySprite;
        }
    }
}
