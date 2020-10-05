using System.Collections.Generic;
using UnityEngine;

namespace TigerForge
{

    /// <summary>
    /// Distribute two or more fields horizontally.
    /// </summary>
    public class TFHorizontal : PropertyAttribute
    {
        public readonly string ID = "";
        public readonly string UID;
        public string newLabelText;
        
        public readonly int n = 0;
        public readonly int offset = 0;
        public readonly int columns = 0;
        
        public readonly bool labelWidthIsPercent = false;
        public readonly bool colWidthIsPercent = false;
        public readonly bool labelAbove = false;
        public readonly bool hideLabels = false;

        public readonly float labelWidth = 0;
        public readonly float fieldWidth = 0;
        public readonly float colWidth = 0;

        public readonly Color labelColor;
        public readonly FontStyle labelFontStyle;

        CLI_Utilities util = new CLI_Utilities();

        public TFHorizontal(string ID, string style, string newLabelText = "", bool labelAbove = false)
        {
            this.ID = ID;
            UID = CLI_Static_Manager.GenerateID("HORIZ");
            this.newLabelText = newLabelText;
            this.labelAbove = labelAbove;

            var defaultStyle = "label-width:50%;offset:0;column-width:0;color:#CCCCCC;text-style:normal";
            CLI_CSSParser css = new CLI_CSSParser(style, defaultStyle);

            var lw = css.stringValue["label-width"];
            labelWidthIsPercent = lw.Contains("%");
            labelWidth = float.Parse(lw.Replace("%", ""));

            var cw = css.stringValue["column-width"];
            colWidthIsPercent = cw.Contains("%");
            colWidth = float.Parse(cw.Replace("%", ""));

            offset = css.intValue["offset"];

            labelColor = css.colorValue["color"];
            labelFontStyle = util.GetFontStyle(css.stringValue["text-style"]);

            CLI_Static_Horizontal.Add(ID, UID);

        }
    }



    /// <summary>
    /// Locate the end of an Horizontal group of fields.
    /// </summary>
    public class TFHorizontalEnd : PropertyAttribute
    {
        public readonly int columns;
        public readonly int offset;

        public TFHorizontalEnd(string ID, int offset = 0)
        {
            columns = CLI_Static_Horizontal.Total(ID);
            this.offset = offset;
        }
    }




    public class CLI_Static_Horizontal
    {
        public static Dictionary<string, string> components = new Dictionary<string, string>();
        public static Dictionary<string, float> widths = new Dictionary<string, float>();
        public static Dictionary<string, Rect> rects = new Dictionary<string, Rect>();

        // Richiamata all'inizializzazione dell'engine (CLI_TF).
        public static void Initialize()
        {

            components = new Dictionary<string, string>();
            widths = new Dictionary<string, float>();

        }

        public static void Add(string groupID, string componentID)
        {
            if (components.ContainsKey(groupID))
            {
                components[groupID] += componentID + "|";
            } else
            {
                components.Add(groupID, componentID + "|");
            }
        }

        public static int Index(string groupID, string componentID)
        {
            if (components.ContainsKey(groupID))
            {
                var data = components[groupID].Split('|');
                for (var i = 0; i < data.Length; i++)
                {
                    if (data[i] == componentID) return i;
                }
            }

            return -1;
        }

        public static int Total(string groupID)
        {
            if (components.ContainsKey(groupID))
            {
                var data = components[groupID].Split('|');
                return data.Length - 1;
            }

            return 0;
        }

        public static void SetWidth(string UID, float value)
        {
            if (widths.ContainsKey(UID)) widths[UID] = value; else widths.Add(UID, value);
        }
        public static float GetWidth(string UID)
        {
            if (widths.ContainsKey(UID)) return widths[UID]; else return 0;
        }

        public static float CalculateTotalWidth(string groupID, int n)
        {
            float totalWidth = 0f;

            if (components.ContainsKey(groupID))
            {
                var data = components[groupID].Split('|');
                for (var i = 0; i < data.Length; i++)
                {
                    if (i < n) totalWidth += GetWidth(data[i]);
                }
            }

            return totalWidth;
        }

        public static void SetRect(string ID, Rect value)
        {
            if (rects.ContainsKey(ID)) rects[ID] = value; else rects.Add(ID, value);
        }
        public static Rect GetRect(string ID)
        {
            if (rects.ContainsKey(ID)) return rects[ID]; else return new Rect(0, 0, 0, 0);
        }
        

    }

}
