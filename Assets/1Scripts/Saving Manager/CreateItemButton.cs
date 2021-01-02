using System;
using System.Globalization;
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

    public void Start()
    {
        SetupSavedItems();
        SetupType();

        string elementName = gameObject.GetComponent<Image>().sprite.name.Replace("_thumbnail", "");
        string foundItem = SavedItems.Find(s => s.Split(',')[0].ToLower().Equals(elementName.ToLower())); // split from the free roam list
        SetItemNameAndType(foundItem);
        SetPastElementActive(foundItem);
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
        if (File.Exists(GetFilePath("create.json")))
        {
            SavedItems = File.ReadAllLines(GetFilePath("create.json")).ToList<string>();
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
    private void SetPastElementActive(string item) 
    {
        if (String.IsNullOrWhiteSpace(item)) return;

        activeByDefault = DateTime.ParseExact(item.Split(',')[2], "dd/MM/yyyy", CultureInfo.InvariantCulture) <= DateTime.Today;
    }
}
