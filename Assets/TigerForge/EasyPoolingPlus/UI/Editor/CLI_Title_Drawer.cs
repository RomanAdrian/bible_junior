using UnityEngine;
using UnityEditor;

namespace TigerForge
{
    [CustomPropertyDrawer(typeof(TFHeader))]
    public class TitleDrawer : DecoratorDrawer
    {
        CLI_Utilities util = new CLI_Utilities();

        Rect area = new Rect(0, 0, 0, 0);

        float subtitleHeight;

        TFHeader TF
        {
            get { return ((TFHeader)attribute); }
        }

        public override float GetHeight()
        {
            return area.height;
        }

        public override void OnGUI(Rect rect)
        {
            // Inizializzazioni.
            GUIStyle titleStyle = util.GetFontStyle(TF.titleFontStyle, TF.titleColor);
            float titleHeight = util.CalcTextHeight(TF.title, titleStyle, rect);

            GUIStyle subStyle = util.GetFontStyle(TF.subTitleFontStyle, TF.subTitleColor, true);
            float tmpH = util.CalcTextHeight(TF.subTitle, subStyle, rect);
            if (tmpH < 100) subtitleHeight = tmpH;

            // Calcolo dell'area da occupare (una zona intera).
            area.x = rect.x;
            area.y = rect.y;
            area.width = rect.width;

            // L'altezza da occupare è calcolata a seconda del testo e dello style.
            area.height = titleHeight + subtitleHeight + TF.marginBottom + TF.lineHeight + TF.lineSpace;

            // Calcolo posizione e dimensioni per il Titolo.
            Rect title = new Rect();
            title.x = area.x;
            title.y = area.y + TF.marginTop;
            title.width = area.width;
            title.height = titleHeight;

            // Calcolo posizione e dimensioni per il Sottotitolo.
            Rect subTitle = new Rect();
            subTitle.x = title.x;
            subTitle.y = title.y + title.height + 4;
            subTitle.width = area.width;
            subTitle.height = subtitleHeight;

            // Calcolo posizione e dimensioni della Linea.
            Rect line = new Rect();
            line.x = subTitle.x;
            line.y = subTitle.y + subTitle.height + TF.lineSpace;
            line.width = area.width;
            line.height = TF.lineHeight;

            // Disegno dei componenti.
            EditorGUI.LabelField(title, TF.title, titleStyle);
            EditorGUI.LabelField(subTitle, TF.subTitle, subStyle);
            EditorGUI.DrawRect(line, TF.lineColor);
        }


    }
}
