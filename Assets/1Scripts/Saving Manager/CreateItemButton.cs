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
    private void Awake()
    {
        SetupSavedItems();

        string elementName = gameObject.GetComponent<Image>().sprite.name.Replace("_thumbnail", "");
        string foundItem = SavedItems.Find(s => s.Split(',')[0].Equals(elementName));
        bool shouldBeActive = foundItem != null;
        ItemNameAndType = foundItem;
        gameObject.SetActive(shouldBeActive);
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
