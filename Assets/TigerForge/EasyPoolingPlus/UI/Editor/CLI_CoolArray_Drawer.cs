using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TigerForge
{

    [CustomPropertyDrawer(typeof(TFCoolArray))]
    public class ArrayDrawer : PropertyDrawer
    {
        CLI_Utilities util = new CLI_Utilities();

        TFCoolArray TF { get { return ((TFCoolArray)attribute); } }

        private bool isFirst = true;
        private float globalHeight = 0f;
        private Rect area;
        private Color defaultBackgroundColor;
        private string code;

        private CLI_Utilities.SField field;

        GenericMenu dragContextMenu = new GenericMenu();

        private List<string> structure = new List<string>();
        private List<string> structureIDs = new List<string>();
        private Rect addButton;

        private struct SDragDrop
        {
            public List<Rect> buttons;
            public bool isStarted;
            public int itemIndex;
            public int targetIndex;
            public int copiedIndex;
            public string itemID;
            public string copiedItemID;
            public Vector2 mouse;

            public void Initialize()
            {
                buttons = new List<Rect>();
                isStarted = false;
                itemIndex = -1;
                targetIndex = -1;
                copiedIndex = -1;
                itemID = "";
                copiedItemID = "";
                mouse = new Vector2();
            }
        }
        private SDragDrop drag;

        private string hashCodes = "";

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);

            if (isFirst)
            {
                isFirst = false;
                defaultBackgroundColor = util.GetDefaultBackgroundColor();

                field = util.GetFieldStandardValues();

                drag.Initialize();
                dragContextMenu.AddItem(new GUIContent("Take This Element"), false, ArrayTake);
                dragContextMenu.AddItem(new GUIContent("Move Here"), false, ArrayMoveHere);
            }

            code = "ID" + property.propertyPath.GetHashCode();
            CLI_Static_CoolArray.AddProperty(property, code);

            structure = new List<string>();

            bool isOpen = property.isExpanded;
            globalHeight = EditorGUI.GetPropertyHeight(property, label, true);
            bool arrayIsOpen = globalHeight > (field.height + field.fullHeight);
            bool hasElements = false;

            if (arrayIsOpen)
            {
                structure.Add("TOOLBAR_OPEN");

                var singleItem = property.FindPropertyRelative("array");
                var aSize = singleItem.arraySize;
                hasElements = aSize > 0;

                if (hasElements)
                {
                    hashCodes = "";
                    for (var i = 0; i < aSize; i++)
                    {
                        var arrayItem = singleItem.GetArrayElementAtIndex(i);
                        var height = EditorGUI.GetPropertyHeight(arrayItem, new GUIContent(label), true);
                        bool elementIsOpen = arrayItem.isExpanded;

                        CreateItemHashCode(arrayItem);

                        var fieldData = arrayItem.FindPropertyRelative(TF.fieldData);
                        var fieldDataString = "";
                        if (fieldData != null) fieldDataString = fieldData.stringValue;

                        var data = "ELEMENT|" + i + "|";
                        if (elementIsOpen) data += "OPEN|"; else data += "CLOSE|";
                        data += height + "|" + fieldDataString;
                        structure.Add(data);
                    }
                }
            }
            else
            {
                if (isOpen) structure.Add("TOOLBAR_CLOSE");
            }

            area = CalculateArea(rect);

            RenderBackground(rect, property, label);

            EditorGUI.PropertyField(rect, property, true);

            RenderForeground(rect, property, label, isOpen);

            EditorGUI.EndProperty();

            if (Event.current.isMouse) MouseEvents(rect, property, label);


        }

        #region " BACKGROUND "

        private void RenderBackground(Rect rect, SerializedProperty property, GUIContent label)
        {
            float y = 0;

            for (var i = 0; i < structure.Count; i++)
            {

                if (structure[i].StartsWith("TOOLBAR"))
                {

                    bool isOpen = structure[i] == "TOOLBAR_OPEN";
                    y = BG_Toolbar(isOpen);

                }
                else if (structure[i].StartsWith("ELEMENT"))
                {

                    y = BG_Element(structure[i], y);

                }

            }
        }

        private float BG_Toolbar(bool isOpen)
        {
            float height = (isOpen) ? field.fullHeight * 2 : field.fullHeight;

            Rect headerRect = new Rect
            {
                x = area.x,
                y = area.y + field.fullHeight,
                width = area.width,
                height = height
            };

            EditorGUI.DrawRect(headerRect, TF.headerColor);

            Rect headerBorderRect = new Rect
            {
                x = headerRect.x,
                y = headerRect.y,
                width = headerRect.width,
                height = 1
            };

            EditorGUI.DrawRect(headerBorderRect, TF.headerBorderColor);

            headerBorderRect.y = headerRect.y + headerRect.height - 1;

            EditorGUI.DrawRect(headerBorderRect, TF.headerBorderColor);

            return headerBorderRect.y + 1;
        }

        private float BG_Element(string structureData, float y)
        {
            var data = structureData.Split('|');
            int n = int.Parse(data[1]);
            bool isOpen = data[2] == "OPEN";
            float height = float.Parse(data[3]);

            Rect elementRect = new Rect
            {
                x = area.x,
                y = y,
                width = area.width,
                height = height + 1
            };

            EditorGUI.DrawRect(elementRect, TF.backgroundColor);

            Rect elementTitleRect = new Rect
            {
                x = elementRect.x,
                y = y,
                width = elementRect.width,
                height = field.height
            };

            DrawDarkerRect(elementTitleRect, TF.backgroundColor, 0.1f);

            return y + height + field.vSpace;
        }

        #endregion


        #region " FOREGROUND "

        private void RenderForeground(Rect rect, SerializedProperty property, GUIContent label, bool mainIsOpen)
        {
            FG_VariableName();

            float y = 0;
            drag.buttons = new List<Rect>();

            for (var i = 0; i < structure.Count; i++)
            {

                if (structure[i].StartsWith("TOOLBAR"))
                {

                    bool isOpen = structure[i] == "TOOLBAR_OPEN";
                    y = FG_Toolbar(isOpen);

                }
                else if (structure[i].StartsWith("ELEMENT"))
                {

                    y = FG_Element(structure[i], y);

                }

            }

            if (mainIsOpen && !TF.hideFooter) FG_Footer(rect);

        }

        private void FG_VariableName()
        {
            Rect variableRect = new Rect
            {
                x = area.x,
                y = area.y,
                width = area.width,
                height = field.height
            };
            if (field.level <= 0)
            {
                EditorGUI.DrawRect(variableRect, defaultBackgroundColor);
            }
            else
            {
                EditorGUI.DrawRect(variableRect, TF.titleBackgroundColor);
            }

            EditorGUI.LabelField(CalculateTextArea(variableRect), TF.title, util.GetFontStyle(TF.titleStyle, TF.titleColor));
        }

        private float FG_Toolbar(bool isOpen)
        {
            float height = (isOpen) ? field.fullHeight * 2 : field.fullHeight;

            Rect coverRect = new Rect
            {
                x = area.x + field.indentSpace,
                y = area.y + field.fullHeight + 1,
                width = area.width - field.indentSpace,
                height = height - 2
            };

            EditorGUI.DrawRect(coverRect, TF.headerColor);

            EditorGUI.LabelField(CalculateTextArea(coverRect), TF.listName, util.GetFontStyle(FontStyle.Normal, TF.subTitleColor));

            if (isOpen)
            {
                Rect toolbarRect = new Rect
                {
                    x = area.x,
                    y = area.y + (field.fullHeight * 2),
                    width = area.width,
                    height = field.fullHeight - 1
                };

                DrawDarkerRect(toolbarRect, TF.headerColor, 0.3f);

                addButton = new Rect
                {
                    x = toolbarRect.x,
                    y = toolbarRect.y,
                    width = 1000,
                    height = 1000
                };

                Rect iconPosition = CalculateTextArea(addButton);

                util.DrawImage(TF.iconAdd, iconPosition);

                addButton.width = 24;
                addButton.height = 18;

                if (TF.icon != "")
                {
                    Rect iconRect = new Rect
                    {
                        x = area.x + area.width - 32 - 2,
                        y = coverRect.y,
                        width = 1000,
                        height = 1000
                    };

                    iconRect = CalculateTextArea(iconRect);

                    util.DrawImage(TF.icon, iconRect);
                }
            }

            Rect headerBorderRect = new Rect
            {
                x = coverRect.x,
                y = coverRect.y,
                width = coverRect.width,
                height = 1
            };

            headerBorderRect.y = coverRect.y + coverRect.height - 1;

            return headerBorderRect.y + 1;
        }

        private float FG_Element(string structureData, float y)
        {
            var data = structureData.Split('|');
            int n = int.Parse(data[1]);
            bool isOpen = data[2] == "OPEN";
            float height = float.Parse(data[3]);
            string fieldData = data[4];

            Rect elementTitleRect = new Rect
            {
                x = area.x + (field.indentSpace * 2),
                y = y + 1,
                width = area.width - (field.indentSpace * 2),
                height = field.height - 2
            };

            DrawDarkerRect(elementTitleRect, TF.backgroundColor, 0.1f);

            var text = GetItemTitle(n);
            EditorGUI.LabelField(CalculateTextArea(elementTitleRect), text);

            if (fieldData != "")
            {
                var fieldDataStyle = util.GetFontStyle(TF.itemDataStyle, TF.itemDataColor, false, TF.itemDataSize);
                fieldDataStyle.alignment = TextAnchor.MiddleRight;
                EditorGUI.LabelField(CalculateTextArea(elementTitleRect), fieldData + "  ", fieldDataStyle);
            }

            if (isOpen && area.x == field.indentX)
            {
                Rect anchorRect = new Rect
                {
                    x = area.x,
                    y = y + 1,
                    width = field.indentSpace,
                    height = height
                };

                EditorGUI.DrawRect(anchorRect, TF.dragDropColor);

                CLI_Static_CoolArray.AddRect(anchorRect, code, n);

                Rect dragButtonRect = new Rect
                {
                    x = anchorRect.x,
                    y = anchorRect.y,
                    width = 15,
                    height = 16
                };

                drag.buttons.Add(dragButtonRect);

                EditorGUI.DrawRect(dragButtonRect, util.GetBlack(0.2f));

                Rect iconRect = CalculateTextArea(anchorRect);
                iconRect.x -= 1;
                iconRect.y -= 1;
                iconRect.width = 1000;
                iconRect.height = 1000;
                util.DrawImage(TF.iconDrag, iconRect);
            }

            return y + height + field.vSpace;
        }

        private string GetItemTitle(int n)
        {
            var text = TF.itemName;
            text = text.Replace("{#}", n.ToString());

            return text;
        }

        private void FG_Footer(Rect rect)
        {
            Rect footerRect = new Rect
            {
                x = rect.x,
                y = rect.y + rect.height + 2,
                width = rect.width,
                height = 4
            };

            EditorGUI.DrawRect(footerRect, TF.footerColor);
        }

        #endregion


        #region " MOUSE EVENTS "

        private void MouseEvents(Rect rect, SerializedProperty property, GUIContent label)
        {

            if (util.MouseClickedLeftOn(addButton)) ListAddNew(property);

            //if (util.IsMouseLeftDown())
            //{

            //    for (var i = 0; i < drag.buttons.Count; i++)
            //    {
            //        if (drag.buttons[i].Contains(Event.current.mousePosition))
            //        {
            //            drag.itemIndex = i;
            //            drag.itemID = CLI_Static_CoolArray.GetClickedID(Event.current.mousePosition);
            //            drag.isStarted = true;
            //            Debug.Log("Left on " + drag.itemID);
            //            break;
            //        }
            //    }
            //}

            //if (util.IsMouseLeftUp() && dragStart)
            //{
            //    if (dragItemIndex >= 0 && dragTargetIndex >= 0 && dragItemIndex != dragTargetIndex)
            //    {
            //        var array = property.FindPropertyRelative("array");
            //        if (array != null) array.MoveArrayElement(dragItemIndex, dragTargetIndex);
            //    }

            //    dragStart = false;
            //    dragItemIndex = -1;
            //    dragTargetIndex = -1;
            //}

            //if (dragStart && dragItemIndex >= 0)
            //{
            //    dragTargetIndex = -1;
            //    for (var i = 0; i < dragButtons.Count; i++)
            //    {
            //        if (dragButtons[i].Contains(Event.current.mousePosition))
            //        {
            //            dragTargetIndex = i;
            //            break;
            //        }
            //    }
            //}


            if (util.IsMouseRightUp())
            {
                for (var i = 0; i < drag.buttons.Count; i++)
                {
                    if (drag.buttons[i].Contains(Event.current.mousePosition))
                    {
                        drag.itemIndex = i;
                        drag.mouse = Event.current.mousePosition;
                        drag.itemID = CLI_Static_CoolArray.GetClickedID(drag.mouse);
                        dragContextMenu.ShowAsContext();
                        break;
                    }
                }
            }

        }

        private void ListAddNew(SerializedProperty property)
        {
            CLI_Static_CoolArray.Initialize();

            property.serializedObject.Update();
            var array = property.FindPropertyRelative("array");
            array.serializedObject.Update();
            if (array != null) array.arraySize++;
            array.serializedObject.ApplyModifiedProperties();
            property.serializedObject.ApplyModifiedProperties();
        }



        private void ArrayTake()
        {
            drag.copiedIndex = drag.itemIndex;
            drag.copiedItemID = drag.itemID;
            //Debug.Log("Take: " + drag.copiedIndex + " - " + drag.copiedItemID + " mouse: " + drag.mouse);
        }

        private void ArrayMoveHere()
        {
            if (drag.copiedIndex < 0 || drag.itemIndex < 0 || drag.copiedIndex == drag.itemIndex)
            {
                Debug.LogError("Index: From " + drag.copiedIndex + " ! To " + drag.itemIndex);
                return;
            }

            if (drag.itemID != drag.copiedItemID)
            {
                Debug.LogError("IDs: " + drag.itemID + " ! " + drag.copiedItemID + " mouse: " + drag.mouse);
                return;
            }

            SerializedProperty prop = CLI_Static_CoolArray.GetPropertyByID(drag.copiedItemID);

            //Debug.Log("Move: " + drag.copiedIndex + " to " + drag.itemIndex + " - " + drag.copiedItemID + " = " + prop.propertyPath.GetHashCode());

            prop.serializedObject.Update();
            var array = prop.FindPropertyRelative("array");
            if (array != null) array.MoveArrayElement(drag.copiedIndex, drag.itemIndex);
            prop.serializedObject.ApplyModifiedProperties();

            drag.Initialize();
            CLI_Static_CoolArray.Initialize();
        }

        #endregion


        #region " Helpers "

        private Rect CalculateArea(Rect rect)
        {
            if (field.level > 0)
            {
                rect.x += field.indent;
                rect.width -= field.indent;
            }
            return rect;
        }

        private Rect CalculateTextArea(Rect rect)
        {
            if (field.level > 0)
            {
                rect.x -= field.indent;
            }
            return rect;
        }

        private void DrawDarkerRect(Rect rect, Color color, float alpha)
        {
            EditorGUI.DrawRect(rect, color);
            EditorGUI.DrawRect(rect, new Color(0, 0, 0, alpha));
        }

        #endregion


        #region " HASH CODE "

        private void CreateItemHashCode(SerializedProperty item)
        {
            var hashcode = item.FindPropertyRelative("_hashCode");

            if (hashcode != null)
            {

                string code = "ID" + item.GetHashCode() + Random.Range(10000, 99999) + Random.Range(10000, 99999);
                code = code.Replace("-", "X");

                if (hashcode.stringValue == "")
                {
                    hashcode.stringValue = code;
                }
                else
                {

                    if (!hashCodes.Contains(hashcode.stringValue))
                    {
                        hashCodes += " " + hashcode.stringValue;
                    }
                    else
                    {
                        hashcode.stringValue = code;

                        hashCodes += " " + hashcode.stringValue;
                    }

                }

            }



        }

        #endregion


    } // ====



}