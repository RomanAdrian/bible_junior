using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TigerForge
{
    [CustomPropertyDrawer(typeof(TFHorizontal))]
    public class HorizontalDrawer : PropertyDrawer
    {

        CLI_Utilities util = new CLI_Utilities();

        Rect area = new Rect(0, 0, 0, 0);

        TFHorizontal TF { get { return ((TFHorizontal)attribute); } }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            // Inizializzazioni.
            var n = CLI_Static_Horizontal.Index(TF.ID, TF.UID);
            var total = CLI_Static_Horizontal.Total(TF.ID);
            bool isPrimo = n == 0;
            var text = "";
            if (TF.newLabelText != "") text = TF.newLabelText; else text = label.text;

            float colWidth = 0;
            // Calcolo la larghezza delle colonne. Solo dal primo elemento.
            if (TF.colWidth == 0)
            {
                // Divido l'intera larghezza per il numero totale di componenti.
                colWidth = (rect.width / total);
            }
            else
            {
                // Calcolo secondo quanto richiesto.
                if (TF.colWidthIsPercent) colWidth = rect.width * (TF.colWidth / 100); else colWidth = TF.colWidth;
            }
            // Registro la larghezza di questo componente
            CLI_Static_Horizontal.SetWidth(TF.UID, colWidth);

            //Debug.Log(TF.ID + " : " + TF.UID + " = " + n + " di " + total);

            EditorGUI.BeginProperty(rect, label, property);

            if (isPrimo)
            {
                // Calcolo dell'area da occupare (equivalente all'intero PRIMO componente).
                area.x = rect.x;
                area.y = rect.y;
                area.width = rect.width;
                area.height = rect.height;

                CLI_Static_Horizontal.SetRect(TF.ID, area);
            }
            else
            {
                // Imposto l'area uguale alla prima rilevata.
                area = CLI_Static_Horizontal.GetRect(TF.ID);
            }

            // Calcolo della porzione occupata da questo componente in base al suo indice e totale componenti.
            Rect column = new Rect();
            column.x = area.x + CLI_Static_Horizontal.CalculateTotalWidth(TF.ID, n);
            column.y = area.y;
            column.width = colWidth;
            column.height = area.height;
            //Debug.Log(TF.UID + " " + column.x + " , " + column.y + " " + column.width + " x " + column.height);

            // All'interno della 'column' ricostruisco il componente.

            // Posizione e dimensioni Label.
            Rect newLabel = new Rect();
            newLabel.x = column.x + TF.offset;
            newLabel.y = column.y;
            newLabel.width = (TF.labelWidthIsPercent) ? column.width * (TF.labelWidth / 100) : TF.labelWidth;
            newLabel.width -= TF.offset;
            newLabel.height = column.height;

            // Posizione e dimensioni Field (in proporzione con la Label).
            Rect newField = new Rect();
            newField.x = newLabel.x + newLabel.width;
            newField.y = newLabel.y;
            newField.width = column.width - newLabel.width - TF.offset;
            newField.height = newLabel.height;

            //EditorGUI.DrawRect(newLabel, util.RandomColor());
            //EditorGUI.DrawRect(newField, util.RandomColor());

            // Se ho annullato la Label (o posizione la Label sopra al Field), riposiziono il Field sull'intera colonna.
            if (TF.newLabelText == "<none>" || TF.labelAbove)
            {
                newField.x = column.x + TF.offset;
                newField.y = column.y;
                newField.width = column.width - TF.offset;
                newField.height = column.height;
            }
            if (TF.newLabelText == "<none>") text = "";
            if (TF.labelAbove)
            {
                newLabel.y -= newLabel.height;
            }

            // Ridiscegno i componenti.
            EditorGUI.LabelField(newLabel, text, util.GetFontStyle(TF.labelFontStyle, TF.labelColor));
            EditorGUI.PropertyField(newField, property, new GUIContent(""));

            EditorGUI.EndProperty();

        }

    }



    [CustomPropertyDrawer(typeof(TFHorizontalEnd))]
    public class HorizontalEndDrawer : DecoratorDrawer
    {

        TFHorizontalEnd tFHorizontalEnd { get { return ((TFHorizontalEnd)attribute); } }

        public override float GetHeight()
        {
            return (-EditorGUIUtility.singleLineHeight - EditorGUIUtility.standardVerticalSpacing - tFHorizontalEnd.offset) * (tFHorizontalEnd.columns - 1);
        }

        public override void OnGUI(Rect rect) { }

    }


}
