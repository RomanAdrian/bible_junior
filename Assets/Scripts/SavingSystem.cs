using UnityEngine;
using System.IO;
using EasyMobile;

public class SavingSystem : MonoBehaviour
{
   public GameObject masks;
   public CanvasData[] MaskData = new CanvasData[80];
   public string SAVE_FOLDER;
   public static bool changesMade = false;

    [SerializeField]
    private SavedGame[] saveSlots;

   private void Awake()
   {
       SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
       // Test if Save folder exists
       if (!Directory.Exists(SAVE_FOLDER))
       {
           // Create save folder
           Directory.CreateDirectory(SAVE_FOLDER);
       }

       foreach (SavedGame saved in saveSlots)
        {
            // ShowSavedFiles(saved);
        }
   }

   public void Save()
   {
        int childcount = masks.transform.childCount;
        int saveNumber = 1;

       for (int i = 0; i < childcount; i++)
       {
           Transform Mcount = masks.transform.GetChild(i);
           RectTransform pozitii = Mcount.transform.GetComponent<RectTransform>(); 

           bool active = Mcount.gameObject.activeSelf;
           string name = Mcount.gameObject.name;

           CanvasData date = new CanvasData(name, pozitii, i, active); // call pe constructorul din clasa respectiva
           MaskData[i] = date;
       }
       
       string json = JsonHelper.ToJson(MaskData, true);
       while(File.Exists(SAVE_FOLDER + "save_" + saveNumber + ".json"))
        {
            saveNumber++;
        }
       
        File.WriteAllText(SAVE_FOLDER + "save_" + saveNumber + ".json", json);
       
        changesMade = false;
   }

   public string Load()
   {

       if (File.Exists(SAVE_FOLDER + "/save.json"))
       {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/save.json");
            return saveString;
       }
       else
       {
            {
                return null;
            }
        }
   }
}
