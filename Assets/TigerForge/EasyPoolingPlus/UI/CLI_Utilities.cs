using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace TigerForge
{

    public class CLI_CSSParser
    {

        public Dictionary<string, string> stringValue;
        public Dictionary<string, int> intValue;
        public Dictionary<string, float> floatValue;
        public Dictionary<string, bool> boolValue;
        public Dictionary<string, Color> colorValue;

        private string strings = "|item-data-style|width|position|title-style|subtitle-style|text-style|label-width|field-width|column-width|x|y|background-style|header-text-style|";
        private string ints = "|item-data-fontsize|height|margin-top|margin-bottom|margin-left|margin-right|line-height|line-margin-top|offset|text-size|title-size|subtitle-size|padding-top|padding-bottom|padding-left|border-left-width|border-top-height|";
        private string floats = "|icon-x|icon-y|icon-width|icon-height|icon-resize|";
        private string bools = "||";
        private string colors = "|item-data-color|color|title-color|title-background-color|subtitle-color|line-color|text-color|border-left-color|border-top-color|border-color|placeholder-color|background-color|back-color|drag-color|header-text-color|footer-color|header-color|header-border-color|selection-color|pagination-color|pagination-text-color|";


        public CLI_CSSParser(string cssStyle, string cssDefault)
        {
            stringValue = new Dictionary<string, string>();
            intValue = new Dictionary<string, int>();
            floatValue = new Dictionary<string, float>();
            boolValue = new Dictionary<string, bool>();
            colorValue = new Dictionary<string, Color>();

            if (cssStyle == "") cssStyle = cssDefault;

            var data = cssStyle.Split(';');
            foreach (string d in data)
            {

                var tmp = d.Split(':');
                if (tmp.Length == 2)
                {

                    var key = tmp[0].ToLower().Trim();
                    var value = tmp[1].ToLower().Trim();

                    if (strings.Contains("|" + key + "|"))
                    {
                        stringValue.Add(key, value);
                    }
                    else if (ints.Contains("|" + key + "|"))
                    {
                        intValue.Add(key, toInt(value));
                    }
                    else if (floats.Contains("|" + key + "|"))
                    {
                        floatValue.Add(key, toFloat(value));
                    }
                    else if (bools.Contains("|" + key + "|"))
                    {
                        boolValue.Add(key, toBool(value));
                    }
                    else if (colors.Contains("|" + key + "|"))
                    {
                        colorValue.Add(key, toColor(value));
                    }

                }

            }

            data = cssDefault.Split(';');
            foreach (string d in data)
            {

                var tmp = d.Split(':');
                if (tmp.Length == 2)
                {

                    var key = tmp[0].ToLower().Trim();
                    var value = tmp[1].ToLower().Trim();

                    if (strings.Contains("|" + key + "|"))
                    {
                        if (!stringValue.ContainsKey(key)) stringValue.Add(key, value);
                    }
                    else if (ints.Contains("|" + key + "|"))
                    {
                        if (!intValue.ContainsKey(key)) intValue.Add(key, toInt(value));
                    }
                    else if (floats.Contains("|" + key + "|"))
                    {
                        if (!floatValue.ContainsKey(key)) floatValue.Add(key, toFloat(value));
                    }
                    else if (bools.Contains("|" + key + "|"))
                    {
                        if (!boolValue.ContainsKey(key)) boolValue.Add(key, toBool(value));
                    }
                    else if (colors.Contains("|" + key + "|"))
                    {
                        if (!colorValue.ContainsKey(key)) colorValue.Add(key, toColor(value));
                    }

                }

            }

        }

        private int toInt(string value)
        {
            int number;
            if (int.TryParse(value, out number))
            {
                return number;
            } else
            {
                return 0;
            }
        }

        private float toFloat(string value)
        {
            float number;
            if (float.TryParse(value, out number))
            {
                return number;
            }
            else
            {
                return 0f;
            }
        }

        private bool toBool(string value)
        {
            bool number;
            if (bool.TryParse(value, out number))
            {
                return number;
            }
            else
            {
                return false;
            }
        }

        private Color toColor(string value)
        {
            if (value == "none") return new Color(0, 0, 0, 0);

            Color newCol;
            if (ColorUtility.TryParseHtmlString(value, out newCol))
            {
                return newCol;
            } else
            {
                return new Color(1, 1, 1);
            }
        }

    }

    public class CLI_Utilities
    {

        private string mouseEvent = "";
        private Vector2 mousePosition = new Vector2(0, 0);
        private Vector2 mouseDownPosition = new Vector2(0, 0);
        private Vector2 mouseUpPosition = new Vector2(0, 0);

        public struct SField
        {
            public float height;
            public float vSpace;
            public float fullHeight;
            public float leftSpace;
            public float indent;
            public int level;
            public float indentSpace;
            public float indentX;
        }

        public SField GetFieldStandardValues()
        {
            SField field = new SField();
#if UNITY_EDITOR
            field.height = EditorGUIUtility.singleLineHeight;
            field.vSpace = EditorGUIUtility.standardVerticalSpacing;
            field.fullHeight = field.height + field.vSpace;
            field.leftSpace = 14;
            field.indentSpace = 15;
            field.level = EditorGUI.indentLevel;
            field.indent = (field.level <= 0) ? 0 : ((field.level) * field.indentSpace);
            field.indentX = field.indent + field.leftSpace;
#endif
            return field;
        }

        /// <summary>
        /// Convert a string size in a float.
        /// </summary>
        public float GetSize(string size, float realSize, float OnErrorSize, float ifNotSize)
        {
            float value = 0f;
            float defaultValue = OnErrorSize;

            if (size == "") return ifNotSize;

            if (size.Contains("%"))
            {
                size = size.Replace("%", "");

                try
                {
                    float percent = float.Parse(size);
                    if (percent > 100) percent = 100;
                    value = realSize * (percent / 100);
                }
                catch (System.Exception)
                {

                    value = defaultValue;
                }

            } else
            {
                try
                {
                    value = float.Parse(size);
                }
                catch (System.Exception)
                {

                    value = defaultValue;
                }
                
            }

            return value;
        }

        public float ConvertSize(string size, float realSize)
        {
            float value = 0f;

            try
            {
                if (size == "") return realSize;

                bool isPercentual = size.Contains("%");
                size = size.Replace("%", "");

                if (isPercentual)
                {
                    float percent = float.Parse(size);
                    if (percent > 100) percent = 100;
                    value = realSize * (percent / 100);
                }
                else
                {
                    value = float.Parse(size);
                }
            }
            catch (System.Exception)
            {

                value = realSize;
            }
            
            return value;

        }

        /// <summary>
        /// Convert an html color in a Color object.
        /// </summary>
        public Color GetColor(string color, Color onErrorColor)
        {
            if (color == "") return onErrorColor;

            Color newCol = onErrorColor;
            if (ColorUtility.TryParseHtmlString(color, out newCol)) return newCol;

            return newCol;
        }

        /// <summary>
        /// Calculate the height of the text area with the provided informations.
        /// </summary>
        public float CalcTextHeight(string text, GUIStyle style, float width)
        {
            GUIContent textContent = new GUIContent();
            textContent.text = text;
            float textH = style.CalcHeight(textContent, width);
            
            return textH;
        }
        public float CalcTextHeight(string text, GUIStyle style, Rect rect)
        {
            GUIContent textContent = new GUIContent();
            textContent.text = text;
            float textH = style.CalcHeight(textContent, rect.width);

            return textH;
        }

        public float CalcTextWidth(string text, GUIStyle style)
        {
            GUIContent textContent = new GUIContent();
            textContent.text = text;

            var size = style.CalcSize(textContent);

            return size.x;
            
        }

        /// <summary>
        /// Split a string in a proper way.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<string> SplitString(string text, int expectedQuantity, char separator = ',')
        {
            List<string> myList = new List<string>();

            var data = text.Split(separator);
            foreach (string chunk in data)
            {
                myList.Add(chunk.Trim());
            }

            if (myList.Count == expectedQuantity) return myList; else return null;
        }

        /// <summary>
        /// Return the FontStyle type from the styleName.
        /// </summary>
        public FontStyle GetFontStyle(string styleName)
        {
            if (styleName == "bold")
            {
                return FontStyle.Bold;
            } else if (styleName == "bolditalic")
            {
                return FontStyle.BoldAndItalic;
            } else if (styleName == "italic")
            {
                return FontStyle.Italic;
            } else
            {
                return FontStyle.Normal;
            }
        }

        /// <summary>
        /// Return a GUIStyle for texts.
        /// </summary>
        public GUIStyle GetFontStyle(FontStyle fontStyle, Color textColor, bool wordWrap = false, int fontSize = 0)
        {
            GUIStyle newStyle = new GUIStyle();
            newStyle.fontStyle = fontStyle;
            newStyle.normal.textColor = textColor;
            newStyle.wordWrap = wordWrap;

            if (fontSize > 0) newStyle.fontSize = fontSize;

            return newStyle;
        }

        public Color RandomColor()
        {
            return new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );
        }

        public string Mouse()
        {
            var isLeftButton = Event.current.button == 0;
            var isRightButton = Event.current.button == 1;
            var isMouseDown = Event.current.type == EventType.MouseDown;
            var isMouseUp = Event.current.type == EventType.MouseUp;
            mousePosition = Event.current.mousePosition;
            
            if (mouseEvent == "" && isMouseDown) mouseEvent = "IS_DOWN";

            if (isMouseUp) mouseEvent = "";

            if (mouseEvent == "IS_DOWN" && isLeftButton)
            {
                mouseEvent = "";
                return "LEFT_CLICK";
            }

            if (mouseEvent == "IS_DOWN" && isRightButton)
            {
                mouseEvent = "";
                return "RIGHT_CLICK";
            }

            return "";
        }

        public string MouseIsScrolling()
        {
            if (!Event.current.isScrollWheel) return "";

            if (Event.current.delta.y > 0) return "SCROLL_DOWN"; else return "SCROLL_UP";

        }

        public bool IsMouseLeftDown()
        {
            var isLeftButton = Event.current.button == 0;
            var isRightButton = Event.current.button == 1;
            var isMouseDown = Event.current.type == EventType.MouseDown;
            var isMouseUp = Event.current.type == EventType.MouseUp;
            mouseDownPosition = Event.current.mousePosition;

            return isLeftButton && isMouseDown;
        }

        public bool IsMouseLeftUp()
        {
            var isLeftButton = Event.current.button == 0;
            var isRightButton = Event.current.button == 1;
            var isMouseDown = Event.current.type == EventType.MouseDown;
            var isMouseUp = Event.current.type == EventType.MouseUp;
            mouseUpPosition = Event.current.mousePosition;
            
            return isLeftButton && isMouseUp;
        }

        public bool IsMouseRightUp()
        {
            var isLeftButton = Event.current.button == 0;
            var isRightButton = Event.current.button == 1;
            var isMouseDown = Event.current.type == EventType.MouseDown;
            var isMouseUp = Event.current.type == EventType.MouseUp;
            mouseUpPosition = Event.current.mousePosition;

            return isRightButton && isMouseUp;
        }

        public Vector2 MouseLeftDownPosition()
        {
            var isLeftButton = Event.current.button == 0;
            var isRightButton = Event.current.button == 1;
            var isMouseDown = Event.current.type == EventType.MouseDown;
            var isMouseUp = Event.current.type == EventType.MouseUp;

            if (isLeftButton && isMouseDown) return Event.current.mousePosition; else return new Vector2(0, 0);
        }

        public bool MouseClickedLeftOn(Rect area)
        {
            var thisMouseEvent = Mouse();
            if (thisMouseEvent == "LEFT_CLICK")
            {
                return area.Contains(mousePosition);
            }

            return false;
        }

        public bool MouseClickedRightOn(Rect area)
        {
            var thisMouseEvent = Mouse();
            if (thisMouseEvent == "RIGHT_CLICK")
            {
                return area.Contains(mousePosition);
            }

            return false;
        }

        public GUIContent GetImage(string iconName)
        {

            GUIContent image = new GUIContent();
#if UNITY_EDITOR
            if (iconName.StartsWith("/")) iconName = "Assets" + iconName;
            image = EditorGUIUtility.IconContent(iconName);
#endif
            return image;
        }

        public void DrawCenteredImage(string iconName, Rect rect, Vector2 size)
        {
            GUIContent image = new GUIContent();
            bool isFile = iconName.StartsWith("/");

            if (isFile) iconName = "Assets" + iconName;
#if UNITY_EDITOR
            image = EditorGUIUtility.IconContent(iconName);
#endif
            GUIStyle iconStyle = new GUIStyle();
            iconStyle.alignment = TextAnchor.MiddleCenter;

            if (isFile && size.x > 0)
            {
                rect = new Rect
                {
                    x = rect.x + ((rect.width - size.x) / 2) - 1,
                    y = rect.y + ((rect.height - size.y) / 2) - 1,
                    width = size.x,
                    height = size.y
                };
            }
#if UNITY_EDITOR
            EditorGUI.LabelField(rect, image, iconStyle);
#endif
        }

        public void DrawImage(string iconName, Rect rect, float newWidth = 0, float newHeight = 0)
        {
            GUIContent image = new GUIContent();
            bool isFile = iconName.StartsWith("/");

            if (isFile) iconName = "Assets" + iconName;
#if UNITY_EDITOR
            image = EditorGUIUtility.IconContent(iconName);
#endif
            if (newWidth > 0)
            {
                rect.width = newWidth;
                rect.height = newHeight;
            }
#if UNITY_EDITOR
            EditorGUI.LabelField(rect, image);
#endif
        }

        public Color GetBlack(float fade)
        {
            return new Color(0, 0, 0, fade);
        }

        public Color GetDefaultBackgroundColor()
        {
#if UNITY_EDITOR
            if (EditorGUIUtility.isProSkin) return new Color32(56, 56, 56, 255); else return new Color32(186, 186, 186, 255);
#else
            return new Color32(56, 56, 56, 255);
#endif

        }

        public string ExtractFromTag(string data, string tag)
        {
            string fromTag = "<" + tag + ">";
            string toTag = "</" + tag + ">";
            int pFrom = data.IndexOf(fromTag) + fromTag.Length;
            int pTo = data.LastIndexOf(toTag);

            if (pTo <= 0 || pFrom <= 0 || (pTo - pFrom) <= 0)
            {
                Debug.LogWarning("CoolComboBox: " + tag + " tag returned an error!");
                Debug.LogWarning("CoolComboBox: " + data);
                Debug.LogWarning("CoolComboBox: " + fromTag + " in " + pFrom + " - " + toTag + " in " + pTo);
                return "";
            }

            return data.Substring(pFrom, pTo - pFrom);
        }


    }

   

}


