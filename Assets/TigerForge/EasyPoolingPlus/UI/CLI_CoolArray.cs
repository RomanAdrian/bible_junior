using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TigerForge
{
    public class TFCoolArray : PropertyAttribute
    {

        public readonly string iconAdd = "d_Toolbar Plus";
        public readonly string iconRemove = "d_LookDevClose";
        public readonly string iconDrag = "align_vertically_center_active";

        public readonly Color headerColor;
        public readonly Color headerBorderColor;
        public readonly Color headerTextColor;
        public readonly FontStyle headerTextStyle;

        public readonly Color footerColor;
        public readonly bool hideFooter;

        public readonly Color backgroundColor;
        public readonly string backgroundStyle = "";

        public readonly Color titleColor;
        public readonly Color titleBackgroundColor;
        public readonly FontStyle titleStyle;

        public readonly Color itemDataColor;
        public readonly FontStyle itemDataStyle;
        public readonly int itemDataSize;

        public readonly Color subTitleColor;
        
        public readonly Color dragDropColor;
        public readonly Color selectionColor;

        public readonly string title = "";
        public readonly string listName = "";
        public readonly string itemName = "";
        public readonly string fieldData = "";
        public readonly string icon = "";

        CLI_Utilities util = new CLI_Utilities();

        public TFCoolArray(string style, string title, string subTitle, string itemName, bool hideFooter = false, string fieldData = "", string icon = "")
        {

            var defaultStyle = "title-background-color:#383838;subtitle-color:#FFF;background-color:#505050;background-style:level-based;drag-color:#303030;header-color:#696969;header-border-color:#000000;header-text-color:#CCC;header-text-style:normal;footer-color:#303030;selection-color:none;pagination-color:#303030;pagination-text-color:#FFF;icon-x:0;icon-y:0;icon-width:48;icon-height:48;title-style:normal;title-color:#FFF;item-data-style:normal;item-data-fontsize:10;item-data-color:#CCC;";

            CLI_CSSParser css = new CLI_CSSParser(style, defaultStyle);

            headerBorderColor = css.colorValue["header-border-color"];
            headerColor = css.colorValue["header-color"];
            headerTextColor = css.colorValue["header-text-color"];
            headerTextStyle = util.GetFontStyle(css.stringValue["header-text-style"]);

            footerColor = css.colorValue["footer-color"];
            this.hideFooter = hideFooter;

            backgroundColor = css.colorValue["background-color"];
            backgroundStyle = css.stringValue["background-style"];

            titleColor = css.colorValue["title-color"];
            titleBackgroundColor = css.colorValue["title-background-color"];
            titleStyle = util.GetFontStyle(css.stringValue["title-style"]);

            subTitleColor = css.colorValue["subtitle-color"];

            itemDataColor = css.colorValue["item-data-color"];
            itemDataStyle = util.GetFontStyle(css.stringValue["item-data-style"]);
            itemDataSize = css.intValue["item-data-fontsize"];
            
            dragDropColor = css.colorValue["drag-color"];
            selectionColor = css.colorValue["selection-color"];

            this.title = title;
            this.listName = subTitle;
            this.itemName = itemName;
            this.fieldData = fieldData;
            this.icon = icon;

        }

    }

#if UNITY_EDITOR
    public class CLI_Static_CoolArray
    {

        private static Dictionary<string, List<Rect>> positions = new Dictionary<string, List<Rect>>();

        private static Dictionary<string, SerializedProperty> properties = new Dictionary<string, SerializedProperty>();

        public static void Initialize()
        {
            positions = new Dictionary<string, List<Rect>>();
            properties = new Dictionary<string, SerializedProperty>();
        }

        public static void AddRect(Rect rect, string ID, int n)
        {
            if (!positions.ContainsKey(ID))
            {
                positions.Add(ID, new List<Rect>());
            }

            int count = positions[ID].Count;

            if (n < count)
            {
                positions[ID][n] = rect;
                //Debug.Log("Update " + ID + " [" + n + "] = " + rect);
            }
            else
            {
                positions[ID].Add(rect);
            }
        }

        public static void AddProperty(SerializedProperty prop, string ID)
        {
            if (!properties.ContainsKey(ID))
            {
                properties.Add(ID, prop);
            }
        }

        public static string GetClickedID(Vector2 mousePosition)
        {
            var ID = "";

            foreach (KeyValuePair<string, List<Rect>> item in positions)
            {
                ID = item.Key;
                foreach (Rect rect in item.Value)
                {
                    if (rect.Contains(mousePosition)) return ID;
                }
            }

            return ID;

        }



        public static SerializedProperty GetPropertyByID(string ID)
        {
            return properties[ID];
        }

        public static void DebugPositions()
        {
            for (var i = 0; i < positions.Count; i++)
            {
                //Debug.Log(i + ") " + IDs[i] + " >> " + positions[i]);
            }
        }

    }
#endif

}


