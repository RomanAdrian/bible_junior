using UnityEngine;
using UnityEditor;

namespace TigerForge
{

    [CustomPropertyDrawer(typeof(TFLabelStyle))]
    public class LabelLookDrawer : PropertyDrawer
    {

        CLI_Utilities util = new CLI_Utilities();

        TFLabelStyle TF { get { return ((TFLabelStyle)attribute); } }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginProperty(rect, label, property);

            var labelText = (TF.newLabelText == "") ? label.text : TF.newLabelText;
            var labelStyle = util.GetFontStyle(TF.labelFontStyle, TF.labelColor);

            EditorGUI.LabelField(rect, labelText, labelStyle);

            if (TF.offset == 0)
            {

                EditorGUI.PropertyField(rect, property, new GUIContent(" "));

            }
            else
            {

                Rect newPosition = new Rect
                {
                    x = rect.x + TF.offset,
                    y = rect.y,
                    width = rect.width - TF.offset,
                    height = rect.height
                };

                EditorGUI.PropertyField(newPosition, property, new GUIContent());

            }

            EditorGUI.EndProperty();

        }

    }
}