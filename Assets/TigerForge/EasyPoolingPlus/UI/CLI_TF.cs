using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerForge
{
    /// <summary>
    /// Initialize the Cool Look Inspector engine.
    /// </summary>
    public class TFCoolLookInspector : PropertyAttribute
    {
        public TFCoolLookInspector()
        {

            CLI_Static_Manager.Initialize();

            CLI_Static_Horizontal.Initialize();

#if UNITY_EDITOR
            CLI_Static_CoolArray.Initialize();
#endif
        }
    }

    public class CLI_Static_Manager
    {

        public static Dictionary<string, Rect> components = new Dictionary<string, Rect>();
        public static List<string> ids = new List<string>();

        public static void Initialize()
        {
            components = new Dictionary<string, Rect>();
            ids = new List<string>();

            Add("__First_CoolLookInspector_Item__", new Rect(0, 0, 0, 0));
        }

        public static string GenerateID(string prefix)
        {
            var ID = prefix + Random.Range(1, 1000000) + "_" + Random.Range(1, 1000000) + "_" + Random.Range(1, 1000000);
            Add(ID, new Rect(0, 0, 0, 0));
            return ID;
        }

        public static void Add(string ID, Rect componentArea)
        {
            if (components.ContainsKey(ID))
            {
                components[ID] = componentArea;
            }
            else
            {
                components.Add(ID, componentArea);
                ids.Add(ID);
            }
        }

        public static Rect GetArea(string ID)
        {
            if (components.ContainsKey(ID)) return components[ID]; else return new Rect(0, 0, 0, 0);
        }

        public static float GetHeight(string ID)
        {
            if (components.ContainsKey(ID)) return components[ID].height; else return 0;
        }

        public static float GetWidth(string ID)
        {
            if (components.ContainsKey(ID)) return components[ID].width; else return 0;
        }

        public static float GetX(string ID)
        {
            if (components.ContainsKey(ID)) return components[ID].x; else return 0;
        }

        public static float GetY(string ID)
        {
            if (components.ContainsKey(ID)) return components[ID].y; else return 0;
        }

        public static float GetNextY(string ID)
        {
            if (components.ContainsKey(ID)) return components[ID].y + components[ID].height; else return 0;
        }

        public static string GetPreviousID()
        {
            if (ids.Count == 0)
            {
                return "";
            } else
            {
                return ids[ids.Count - 1];
            }
        }

        public static Rect GetPreviousArea()
        {
            var ID = GetPreviousID();
            if (ID == "") return new Rect(0, 0, 0, 0);

            if (components.ContainsKey(ID)) return components[ID]; else return new Rect(0, 0, 0, 0);
        }

        public static Rect AdjustCurrentY(string ID, Rect currentArea, float offset = 0)
        {
            var previousID = "";
 
            for (var i = 0; i < ids.Count; i++)
            {
                
                if (ids[i] == ID)
                {
                    previousID = ids[i - 1];
                    break;
                }
                
            }

            if (previousID != "")
            {
                var previousArea = components[previousID];
                currentArea.y = previousArea.y + previousArea.height + offset;
            }

            return currentArea;
        }

    }

}
