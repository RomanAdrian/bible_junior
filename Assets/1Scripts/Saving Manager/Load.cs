using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class Load : MonoBehaviour
{
    public string SaveName = "";

    public string SaveFolder;
    public string SaveFileName = "player_saves.json";

    public string PathToImages;
    public string PathToThumbs;
    public GameObject ElementPrefab;
    public GameObject ThumbnailPrefab;
    public GameObject NarrationPrefab;

    GameObject Canvas;
    GameObject Buttons;

    public static bool changesMade = false;

    public void Awake()
    {
        Buttons = GameObject.FindGameObjectWithTag("ButtonList");
        Canvas = GameObject.FindGameObjectWithTag("Painting");
        SaveName = PlayerPrefs.GetString("SaveName");
        LoadGame();
    }

    public void LoadGame()
    {
        if (!File.Exists(GetFilePath())) return;

        SaveData[] saveString = JsonHelper.FromJson<SaveData>(File.ReadAllText(GetFilePath()));

        SaveData save = Array.Find(saveString, s => s.Name == SaveName );

        ElementData[] objects = save.Elements;
        ThumbnailData[] thumbs = save.Thumbs;

        SetBackground(save.BackgroundName);
        SetNarrationElements(save.NarrationElements, save.AudioSource);

        foreach (ElementData obj in objects)
        {
            GameObject currentObj = Instantiate(ElementPrefab);
            if (currentObj == null) continue;

            currentObj.transform.SetParent(Canvas.transform);
            obj.ToGameObject(currentObj, PathToImages);
        }

        foreach (ThumbnailData thumb in thumbs)
        {
            if (thumb.Name == "" || thumb.Image == "") continue;

            GameObject currentObj = Instantiate(ThumbnailPrefab);
            if (currentObj == null) continue;

            currentObj.transform.SetParent(Buttons.transform);
            thumb.ToGameObject(currentObj, PathToThumbs);
        }
    }

    public void SetNarrationElements(string[] narrationElements, string audioSource)
    {
        Transform naratiune = GameObject.FindWithTag("Canvas").transform.Find("Naratiune");
        Transform content = naratiune.GetChild(0).GetChild(0).GetChild(0);
        Transform pagination = naratiune.GetChild(1);

        for (int i = 0; i < narrationElements.Length; i++)
        {
            GameObject currentObj = Instantiate(NarrationPrefab);
            currentObj.transform.SetParent(content);
            Sprite img = Resources.Load<Sprite>(PathToImages + narrationElements[i]);

            currentObj.GetComponent<Image>().sprite = img;
            currentObj.transform.localScale = new Vector3(0.75f, 0.79f, 0.75f);
        }

        if (pagination.childCount < narrationElements.Length)
        {
            Transform el = pagination.GetChild(0);
            for (int i = 0; i <= narrationElements.Length - pagination.childCount; i++)
            {
                GameObject clone = Instantiate(el.gameObject);
                clone.transform.SetParent(pagination);
                clone.transform.localScale = new Vector3(1, 1, 1);
                clone.transform.localPosition = new Vector3(40 * (i + 1), el.transform.localPosition.y, el.transform.localPosition.z);
            }
        }

        if (audioSource == "" || audioSource == null) return;
        AudioSource audio = naratiune.GetComponentInChildren<AudioSource>();
        audio.clip = Resources.Load<AudioClip>(audioSource);
    }

    public string GetFilePath()
    {
        return GetSaveFolder() + SaveFileName;
    }

    public string GetSaveFolder()
    {
        if (SaveFolder != "") return SaveFolder;
        return Application.persistentDataPath + "/Saves/";
    }

    public string LocalizedText(string filename)
    {
        if (filename.EndsWith("_ro") && PlayerPrefs.GetString("Language") == "en")
        {
            return filename.Replace("_ro", "_en");
        }
        else if (filename.EndsWith("_en") && PlayerPrefs.GetString("Language") == "ro")
        {
            return filename.Replace("_en", "_ro");
        }
        else return filename;
    }

    public void SetBackground(string backgroundName)
    {
        Image panel = GameObject.Find("Panel").GetComponent<Image>();
        Sprite img = Resources.Load<Sprite>(PathToImages + backgroundName);
        panel.GetComponent<Image>().sprite = img;
    }

}
