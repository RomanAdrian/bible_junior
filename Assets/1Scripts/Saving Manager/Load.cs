using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class Load : MonoBehaviour
{
    public int SaveIndex = 0;

    public string SaveFolder;
    public string SaveFileName = "player_saves.json";

    public string PathToImages;
    public string PathToThumbs;
    public GameObject ElementPrefab;
    public GameObject ThumbnailPrefab;

    GameObject Canvas;
    GameObject Buttons;

    public static bool changesMade = false;

    [SerializeField]
    private SavedGame[] saveSlots;

    private void Awake()
    {
        Buttons = GameObject.FindGameObjectWithTag("ButtonList");
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void LoadGame()
    {
        if (!File.Exists(GetFilePath())) return;

        string saveString = File.ReadAllText(GetFilePath());
        SaveData save = JsonHelper.FromJson<SaveData>(saveString)[SaveIndex];

        ElementData[] objects = save.Elements;
        ThumbnailData[] thumbs = save.Thumbs;

        foreach (ElementData obj in objects)
        {
            GameObject currentObj = Instantiate(ElementPrefab) as GameObject;
            if (currentObj == null) continue;

            currentObj.transform.SetParent(Canvas.transform);

            currentObj.transform.localPosition = new Vector3(obj.Position[0], obj.Position[1], 0);
            currentObj.transform.localScale = new Vector3(obj.Scale[0], obj.Scale[1], 0);
            currentObj.transform.SetSiblingIndex(obj.Index + 1);

            Sprite img = Resources.Load<Sprite>(PathToImages + obj.Image);

            currentObj.GetComponent<Image>().sprite = img;
            currentObj.GetComponent<RectTransform>().sizeDelta = new Vector2(obj.Size[0], obj.Size[1]);

            currentObj.SetActive(true);
        }

        foreach (ThumbnailData thumb in thumbs)
        {
            GameObject currentObj = Instantiate(ThumbnailPrefab) as GameObject;
            if (currentObj == null) continue;

            currentObj.transform.SetParent(Buttons.transform);

            currentObj.transform.SetSiblingIndex(thumb.Index);

            Sprite img = Resources.Load<Sprite>(PathToThumbs + thumb.Image);

            currentObj.GetComponent<Image>().sprite = img;

            currentObj.transform.localScale = new Vector3(1, 1, 1);
            currentObj.GetComponent<RectTransform>().sizeDelta = new Vector2(thumb.Size[0], thumb.Size[1]);

            currentObj.SetActive(thumb.Active);
        }
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
}
