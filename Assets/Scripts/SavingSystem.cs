using UnityEngine;
using System.IO;

public class SavingSystem : MonoBehaviour
{
   public GameObject masks;
   public CanvasData[] MaskData = new CanvasData[80];
   //public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

//    public static void Init()
//    {
//        // Test if Save folder exists
//        if (!Directory.Exists(SAVE_FOLDER))
//        {
//            // Create save folder
//            Directory.CreateDirectory(SAVE_FOLDER);
//        }
//    }

   public void Looping()
   {
       int childcount = masks.transform.childCount;

       for (int i = 0; i < childcount; i++)
       {
           Transform Mcount = masks.transform.GetChild(i);
           RectTransform pozitii = Mcount.transform.GetComponent<RectTransform>(); // cauti componenta RectTransf

           bool active = Mcount.gameObject.activeSelf;
           string name = Mcount.gameObject.name;

           CanvasData date = new CanvasData(name, pozitii, i, active); // call pe constructorul din clasa respectiva
           MaskData[i] = date;
       } 
   }

//    public static string Load()
//    {
//        if (File.Exists(SAVE_FOLDER + "/save.txt"))
//        {
//            if (File.Exists(SAVE_FOLDER + "/save.txt"))
//            {
//                string saveString = File.ReadAllText(SAVE_FOLDER + "/save.txt");
//                return saveString;
//            }
//            else
//            {
//                {
//                    return null;
//                }
//            }
//        }
//    }
}
