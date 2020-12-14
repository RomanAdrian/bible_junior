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
    public bool activeByDefault = false;
    public string ElementType;
    // Start is called before the first frame update

    private void Start()
    {
        SetupSavedItems();
        SetupType();

        string elementName = gameObject.GetComponent<Image>().sprite.name.Replace("_thumbnail", "");
        string foundItem = SavedItems.Find(s => s.Split(',')[0].Equals(elementName)); // split from the free roam list
        SetItemNameAndType(foundItem);
        SetOpacity(foundItem != null);
    }

    private void SetOpacity(bool active)
    {
        if (active || activeByDefault) return;
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, .5f);
        gameObject.GetComponent<Button>().interactable = false;
    }

    private void SetItemNameAndType(string foundItem)
    {
        if (String.IsNullOrWhiteSpace(foundItem))
        {
            ItemNameAndType = gameObject.GetComponent<Image>().sprite.name.Replace("_thumbnail", "") + "," + ElementType;
        }
        else ItemNameAndType = foundItem;
    }

    private void SetupSavedItems()
    {
        if (SavedItems.Count > 0) return;
        if (File.Exists(GetFilePath("creeaza.json")))
        {
            SavedItems = File.ReadAllLines(GetFilePath("creeaza.json")).ToList<string>();
        }
    }

    private void SetupType() 
    {
        if (!String.IsNullOrWhiteSpace(ElementType)) return;
        ElementType = transform.parent.parent.parent.name == "1ScrollFundal" ? "SmallSubmenu" : "Submenu";
    }

    private string GetFilePath(string saveFile)
    {
        return Application.persistentDataPath + "/Saves/" + saveFile;
    }
}
