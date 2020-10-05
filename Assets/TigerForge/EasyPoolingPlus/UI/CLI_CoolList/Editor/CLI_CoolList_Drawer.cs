using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TigerForge.Editor {

	[CustomPropertyDrawer(typeof(TFCoolList))]
	public class ReorderableDrawer : PropertyDrawer {

		private static Dictionary<int, CoolList> lists = new Dictionary<int, CoolList>();

        TFCoolList TF { get { return ((TFCoolList)attribute); } }

        static TFCoolList TFCL;

        public override bool CanCacheInspectorGUI(SerializedProperty property) {
			return false;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

			CoolList list = GetArray(property, attribute as TFCoolList, "array");
			return list != null ? list.GetHeight() : EditorGUIUtility.singleLineHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            TFCL = TF;

            if (TFCL != null)
            {
                CoolList list = GetArray(property, attribute as TFCoolList, "array");

                CLI_Static_CoolList.RegisterMainName(property.serializedObject.targetObject.GetHashCode(), property.name, TFCL.listName);
                CLI_Static_CoolList.RegisterItemName(property.serializedObject.targetObject.GetHashCode(), property.name, TFCL.itemName);
                CLI_Static_CoolList.RegisterConfiguration(property.serializedObject.targetObject.GetHashCode(), property.name, TFCL);

                if (list != null)
                {
                    list.DoList(EditorGUI.IndentedRect(position), label);
                }
                else
                {
                    GUI.Label(position, "You have to create a valid array using TFCoolListCreate<YourClass> { }", EditorStyles.label);
                }
            }

            
		}

		public static int GetID(SerializedProperty property) {

			if (property != null) {

				int h1 = property.serializedObject.targetObject.GetHashCode();
				int h2 = property.propertyPath.GetHashCode();

				return (((h1 << 5) + h1) ^ h2);
			}

			return 0;
		}

		public static CoolList GetArray(SerializedProperty property, TFCoolList attrib, string arrayPropertyName) {

			return GetArray(property, attrib, GetID(property), arrayPropertyName);
		}

		public static CoolList GetArray(SerializedProperty property, TFCoolList attrib, int id, string arrayPropertyName) {

			if (property == null) return null;

            CoolList coolList = null;
            
            if (TFCL != null)
            {
                SerializedProperty array = property.FindPropertyRelative(arrayPropertyName);
                
                if (array != null && array.isArray)
                {

                    if (!lists.TryGetValue(id, out coolList))
                    {

                        CoolList.ElementDisplayType displayType = attrib.singleLine ? CoolList.ElementDisplayType.SingleLine : CoolList.ElementDisplayType.Auto;
                        coolList = new CoolList(array, TFCL, displayType);

                        lists.Add(id, coolList);
                    }
                    else
                    {

                        coolList.List = array;
                    }
                }
            }

			return coolList;
		}

	}

}

