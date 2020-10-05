using UnityEngine;

namespace TigerForge
{
    /// <summary>
    /// Create a Header with a title and a subtitle, with the given style.
    /// </summary>
    public class TFHeader : PropertyAttribute
    {

        public string title;
        public string subTitle;

        public readonly int marginTop;
        public readonly int marginBottom;

        public readonly Color lineColor = new Color(255, 255, 255);
        public readonly int lineHeight = 0;
        public readonly int lineSpace = 0;

        public readonly Color titleColor = new Color(255, 255, 255);
        public readonly FontStyle titleFontStyle = FontStyle.Normal;

        public readonly Color subTitleColor = new Color(255, 255, 255);
        public readonly FontStyle subTitleFontStyle = FontStyle.Normal;

        CLI_Utilities util = new CLI_Utilities();

        public readonly string UUID;

        public TFHeader(string title, string subTitle, string style)
        {
            var defaultStyle = "margin-top:10;margin-bottom:10;title-color:#FFF;title-style:bold;subtitle-color:#CCC;subtitle-style:italic;line-color:#595959;line-height:1;line-margin-top:4";

            CLI_CSSParser css = new CLI_CSSParser(style, defaultStyle);

            this.title = title;
            this.subTitle = subTitle;

            marginTop = css.intValue["margin-top"];
            marginBottom = css.intValue["margin-bottom"];

            lineColor = css.colorValue["line-color"];
            lineHeight = css.intValue["line-height"];
            lineSpace = css.intValue["line-margin-top"];

            titleColor = css.colorValue["title-color"];
            titleFontStyle = util.GetFontStyle(css.stringValue["title-style"]);

            subTitleColor = css.colorValue["subtitle-color"];
            subTitleFontStyle = util.GetFontStyle(css.stringValue["subtitle-style"]);

            UUID = CLI_Static_Manager.GenerateID("BANNER");
        }
    }


}


