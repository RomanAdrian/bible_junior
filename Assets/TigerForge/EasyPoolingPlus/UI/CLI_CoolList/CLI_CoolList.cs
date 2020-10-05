using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerForge {

    /// <summary>
    /// Convert a List in a full styled component.
    /// </summary>
	public class TFCoolList : PropertyAttribute {

        public bool singleLine;

        public readonly string iconAdd = "d_Toolbar Plus";
        public readonly string iconRemove = "d_LookDevClose";
        public readonly string iconDrag = "align_vertically_center_active";

        public readonly Color headerColor;
        public readonly Color headerBorderColor;
        public readonly Color headerTextColor;
        public readonly FontStyle headerTextStyle;

        public readonly Color footerColor;

        public readonly Color backgroundColor;
        public readonly string backgroundStyle = "";

        public readonly Color titleColor;
        public readonly FontStyle titleStyle;

        public readonly Color dragDropColor;
        public readonly Color selectionColor;

        public readonly int pagination = 0;
        public readonly Color paginationColor;
        public readonly Color paginationTextColor;

        public readonly string listName = "";
        public readonly string itemName = "";
        public readonly string variableName = "";

        public readonly string iconName;
        public readonly float iconX;
        public readonly float iconY;
        public readonly float iconWidth;
        public readonly float iconHeight;

        CLI_Utilities util = new CLI_Utilities();

        public TFCoolList(string style, int pagination = 0, string listName = "", string itemName = "", string variableName = "", string iconName = "")
        {

            var defaultStyle = "background-color:#505050;background-style:level-based;drag-color:#303030;header-color:#696969;header-border-color:#000000;header-text-color:#CCC;header-text-style:normal;footer-color:#303030;selection-color:none;pagination-color:#303030;pagination-text-color:#FFF;icon-x:0;icon-y:0;icon-width:48;icon-height:48;title-style:normal;title-color:#FFF;";

            CLI_CSSParser css = new CLI_CSSParser(style, defaultStyle);

            headerBorderColor = css.colorValue["header-border-color"];
            headerColor = css.colorValue["header-color"];
            headerTextColor = css.colorValue["header-text-color"];
            headerTextStyle = util.GetFontStyle(css.stringValue["header-text-style"]);

            footerColor = css.colorValue["footer-color"];

            backgroundColor = css.colorValue["background-color"];
            backgroundStyle = css.stringValue["background-style"];

            titleColor = css.colorValue["title-color"];
            titleStyle = util.GetFontStyle(css.stringValue["title-style"]);

            dragDropColor = css.colorValue["drag-color"];
            selectionColor = css.colorValue["selection-color"];
            paginationColor = css.colorValue["pagination-color"];
            paginationTextColor = css.colorValue["pagination-text-color"];

            this.listName = listName;
            this.itemName = itemName;
            this.pagination = pagination;
            this.variableName = variableName;

            this.iconName = iconName;
            iconX = css.floatValue["icon-x"];
            iconY = css.floatValue["icon-y"];
            iconWidth = css.floatValue["icon-width"];
            iconHeight = css.floatValue["icon-height"];

        }
    }

    public class CLI_Static_CoolList
    {
        private static Dictionary<string, string> itemNames = new Dictionary<string, string>();
        private static Dictionary<string, string> mainNames = new Dictionary<string, string>();
        private static Dictionary<string, TFCoolList> config = new Dictionary<string, TFCoolList>();

        public static void Initialize()
        {
            itemNames = new Dictionary<string, string>();
            mainNames = new Dictionary<string, string>();
            config = new Dictionary<string, TFCoolList>();
        }

        public static void RegisterItemName(int ID, string name, string newName)
        {
            var key = name + "_" + ID;
            if (itemNames.ContainsKey(key))
            {
                itemNames[key] = newName;
            } else
            {
                itemNames.Add(key, newName);
            }
        }

        public static string GetRegisteredItemName(int ID, string name)
        {
            var key = name + "_" + ID;
            if (itemNames.ContainsKey(key))
            {
                return itemNames[key];
            }
            else
            {
                return "";
            }
        }

        public static void RegisterMainName(int ID, string name, string newName)
        {
            var key = name + "_" + ID;
            if (mainNames.ContainsKey(key))
            {
                mainNames[key] = newName;
            }
            else
            {
                mainNames.Add(key, newName);
            }
        }

        public static string GetRegisteredMainName(int ID, string name)
        {
            var key = name + "_" + ID;
            if (mainNames.ContainsKey(key))
            {
                return mainNames[key];
            }
            else
            {
                return "";
            }
        }

        public static void RegisterConfiguration(int ID, string name, TFCoolList configuration)
        {
            var key = name + "_" + ID;
            if (config.ContainsKey(key))
            {
                config[key] = configuration;
            }
            else
            {
                config.Add(key, configuration);
            }
        }

        public static TFCoolList GetRegisteredConfigurarion(int ID, string name)
        {
            var key = name + "_" + ID;
            if (config.ContainsKey(key))
            {
                return config[key];
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Generate a List of objects compatible with CoolList.
    /// </summary>
    [Serializable]
    public abstract class TFCoolListCreate<T> : ICloneable, IList<T>, ICollection<T>, IEnumerable<T>
    {

        [SerializeField]
        private List<T> array = new List<T>();

        public TFCoolListCreate()
            : this(0)
        {
        }

        public TFCoolListCreate(int length)
        {

            array = new List<T>(length);
        }

        public T this[int index]
        {

            get { return array[index]; }
            set { array[index] = value; }
        }

        public int Length
        {

            get { return array.Count; }
        }

        public bool IsReadOnly
        {

            get { return false; }
        }

        public int Count
        {

            get { return array.Count; }
        }

        public object Clone()
        {

            return new List<T>(array);
        }

        public void CopyFrom(IEnumerable<T> value)
        {

            array.Clear();
            array.AddRange(value);
        }

        public bool Contains(T value)
        {

            return array.Contains(value);
        }

        public int IndexOf(T value)
        {

            return array.IndexOf(value);
        }

        public void Insert(int index, T item)
        {

            array.Insert(index, item);
        }

        public void RemoveAt(int index)
        {

            array.RemoveAt(index);
        }

        public void Add(T item)
        {

            array.Add(item);
        }

        public void Clear()
        {

            array.Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {

            this.array.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {

            return array.Remove(item);
        }

        public T[] ToArray()
        {

            return array.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {

            return array.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {

            return array.GetEnumerator();
        }
    }
}
