using UnityEngine;

namespace TigerForge
{

    /// <summary>
    /// Change the text and style of an Inspector field's label.
    /// </summary>
    public class TFLabelStyle : PropertyAttribute
    {

        public string newLabelText;

        public readonly string width;
        public readonly int marginLeft;
        public readonly int offset = 0;

        public readonly Color labelColor;
        public readonly FontStyle labelFontStyle;

        CLI_Utilities util = new CLI_Utilities();

        public TFLabelStyle(string newLabelText, string style)
        {
            var defaultStyle = "width:0;margin-left:1;color:#CCCCCC;text-style:normal;offset:0";

            CLI_CSSParser css = new CLI_CSSParser(style, defaultStyle);

            this.newLabelText = newLabelText;

            width = css.stringValue["width"];
            marginLeft = css.intValue["margin-left"];
            labelColor = css.colorValue["color"];
            labelFontStyle = util.GetFontStyle(css.stringValue["text-style"]);
            offset = css.intValue["offset"];

        }
    }




}
