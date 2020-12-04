using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreateItemButton : MonoBehaviour
{
    public static List<string> SavedItems = new List<string>();
    public string ItemNameAndType;
    // Start is called before the first frame update
    private void Start()
    {
        SetupSavedItems();

        string elementName = gameObject.GetComponent<Image>().sprite.name.Replace("_thumbnail", "");
        string foundItem = SavedItems.Find(s => s.Split(',')[0].Equals(elementName)); // split from the free roam list
        ItemNameAndType = foundItem;
        SetOpacity(foundItem != null);
    }

    private void SetOpacity(bool active)
    {
        if (active) return;
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, .5f);
        gameObject.GetComponent<Button>().interactable = false;
    }

    private void OnEnable()
    {
        SetupSavedItems();

        string elementName = gameObject.GetComponent<Image>().sprite.name.Replace("_thumbnail", "");
        string foundItem = SavedItems.Find(s => s.Split(',')[0].Equals(elementName)); // split from the free roam list
        ItemNameAndType = foundItem;
        SetOpacity(foundItem != null);
    }

    private void SetupSavedItems()
    {
        if (SavedItems.Count > 0) return;
        SavedItems = File.ReadAllLines(GetFilePath("creeaza.json")).ToList<string>();
    }

    private string GetFilePath(string saveFile)
    {
        return Application.persistentDataPath + "/Saves/" + saveFile;
    }
}
