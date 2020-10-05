using UnityEngine;
using UnityEditor;

namespace TigerForge
{
    [CustomPropertyDrawer(typeof(TFBanner))]
    public class BannerDrawer : DecoratorDrawer
    {
        CLI_Utilities util = new CLI_Utilities();

        TFBanner TF { get { return ((TFBanner)attribute); } }

        Rect area = new Rect(0, 0, 0, 0);
        float subtitleHeight = 1000;

        public override float GetHeight()
        {
            return area.height;
        }

        public override void OnGUI(Rect rect)
        {
            // Inizializzazioni.
            GUIStyle titleStyle = util.GetFontStyle(TF.titleFontStyle, TF.titleColor, false, TF.titleSize);
            var titleHeight = util.CalcTextHeight(TF.title, titleStyle, rect);

            GUIStyle subtitleStyle = util.GetFontStyle(TF.subtitleFontStyle, TF.subtitleColor, true, TF.subtitleSize);
            var _subtitleHeight = util.CalcTextHeight(TF.subtitle, subtitleStyle, rect);
            if (_subtitleHeight < 100) subtitleHeight = _subtitleHeight;

            // Calcolo dell'area da occupare (una striscia intera).
            area.x = 0;
            area.y = rect.y;
            area.width = rect.width + 100;

            // L'altezza da occupare è calcolata a seconda del testo e dello style.
            area.height = titleHeight + subtitleHeight + TF.paddingTop + TF.paddingBottom + TF.marginTop + TF.marginBottom;

            if (TF.moveOnTop)
            {
                ;
                area.y = TF.marginTop;
                area.height = TF.paddingBottom + TF.marginBottom + titleHeight + subtitleHeight + TF.paddingTop + TF.paddingBottom;
            }


            // Calcolo dell'area del banner (intero)
            Rect banner = new Rect();
            banner.x = TF.marginLeft;
            banner.y = area.y + TF.marginTop;
            banner.width = util.ConvertSize(TF.width, area.width);
            banner.height = titleHeight + TF.paddingTop + TF.paddingBottom;

            // Calcolo dell'area icona (posizionata a sinistra del banner).
            Rect iconArea = new Rect();
            iconArea.x = banner.x;
            iconArea.y = banner.y;
            iconArea.width = TF.borderLeftWidth;
            iconArea.height = banner.height;

            // Calcolo posizione della label.
            Rect label = new Rect();
            label.x = banner.x + iconArea.width + TF.paddingLeft;
            label.y = banner.y + TF.paddingTop;
            label.width = banner.width;
            label.height = banner.height;

            // Subtitle
            Rect subTitle = new Rect
            {
                x = banner.x,
                y = banner.y + banner.height,
                width = banner.width,
                height = subtitleHeight + TF.paddingTop + TF.paddingBottom
            };

            Rect subTitleText = new Rect
            {
                x = subTitle.x + TF.paddingLeft,
                y = subTitle.y + TF.paddingTop,
                width = rect.width,
                height = subtitleHeight
            };

            // Posizionamento elementi.
            EditorGUI.DrawRect(banner, TF.bgColor);
            EditorGUI.DrawRect(subTitle, TF.backgroundColor);
            EditorGUI.DrawRect(iconArea, TF.borderLeftColor);
            EditorGUI.LabelField(label, TF.title, titleStyle);
            EditorGUI.LabelField(subTitleText, TF.subtitle, subtitleStyle);

            // Icona (se presente).
            if (TF.iconName != "")
            {
                Rect icon = new Rect();
                icon.x = iconArea.x + TF.iconX;
                icon.y = iconArea.y + TF.iconY;
                icon.width = (TF.iconWidth > 0) ? TF.iconWidth : 128;
                icon.height = (TF.iconHeight > 0) ? TF.iconHeight : 128;

                util.DrawImage(TF.iconName, icon);
            }

            CLI_Static_Manager.Add(TF.UUID, area);

        }

    }
}
