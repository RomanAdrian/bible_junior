using UnityEngine;

namespace TigerForge
{
    /// <summary>
    /// Draw a coloured banner with a title.
    /// </summary>
    public class TFBanner : PropertyAttribute
    {
        public readonly string title;
        public readonly string subtitle;

        public readonly Color titleColor;
        public readonly FontStyle titleFontStyle;
        public readonly int titleSize;

        public readonly Color subtitleColor;
        public readonly FontStyle subtitleFontStyle;
        public readonly int subtitleSize;

        public readonly int paddingTop;
        public readonly int paddingBottom;
        public readonly int paddingLeft;
        public readonly Color bgColor;
        public readonly Color backgroundColor;
        public readonly string width;
        public readonly string iconName;
        public readonly int marginLeft;
        public readonly int borderLeftWidth;
        public readonly Color borderLeftColor;
        public readonly float iconX;
        public readonly float iconY;
        public readonly float iconWidth;
        public readonly float iconHeight;

        public readonly float marginTop;
        public readonly float marginBottom;

        public readonly bool moveOnTop = false;
        public readonly string UUID;

        CLI_Utilities util = new CLI_Utilities();

        public TFBanner(string title, string subtitle, float marginTop, float marginBottom, string style, string iconName = "", bool moveOnTop = false)
        {
            var defaultStyle = "width:100%;margin-left:0;title-color:#FFF;title-style:bold;title-size:12;subtitle-color:#FFF;subtitle-style:normal;subtitle-size:10;color:#696969;background-color:#696969;padding-top:4;padding-bottom:4;padding-left:4;border-left-width:4;border-left-color:#CCC;icon-x:0;icon-y:0;icon-width:0;icon-height:0";

            CLI_CSSParser css = new CLI_CSSParser(style, defaultStyle);

            this.title = title;
            this.subtitle = subtitle;

            titleColor = css.colorValue["title-color"];
            titleFontStyle = util.GetFontStyle(css.stringValue["title-style"]);
            titleSize = css.intValue["title-size"];

            subtitleColor = css.colorValue["subtitle-color"];
            subtitleFontStyle = util.GetFontStyle(css.stringValue["subtitle-style"]);
            subtitleSize = css.intValue["subtitle-size"];

            paddingTop = css.intValue["padding-top"];
            paddingBottom = css.intValue["padding-bottom"];
            paddingLeft = css.intValue["padding-left"];

            bgColor = css.colorValue["color"];
            backgroundColor = css.colorValue["background-color"];
            width = css.stringValue["width"];
            marginLeft = css.intValue["margin-left"];

            borderLeftWidth = css.intValue["border-left-width"];
            borderLeftColor = css.colorValue["border-left-color"];

            this.iconName = iconName;
            iconX = css.floatValue["icon-x"];
            iconY = css.floatValue["icon-y"];
            iconWidth = css.floatValue["icon-width"];
            iconHeight = css.floatValue["icon-height"];

            this.marginTop = marginTop;
            this.marginBottom = marginBottom;

            UUID = CLI_Static_Manager.GenerateID("BANNER");
            this.moveOnTop = moveOnTop;

        }
    }


}
